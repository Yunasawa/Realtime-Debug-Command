using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using YNL.Extension.Method;
using YNL.Utilities;

public class DebugCommandExecuter : MonoBehaviour
{
    private DebugCommandEditor _editor;
    private DebugCommandManager _manager;

    #region ▶ Properties 1
    public GameObject CommandPanel => _manager.CommandPanel;
    public RectTransform SuggestionPanel => _manager.SuggestionPanel;
    public TMP_InputField CommandInput => _manager.CommandInput;
    public TextMeshProUGUI CommandPrompt => _manager.CommandPrompt;
    public TextMeshProUGUI CommandText => _manager.CommandText;
    public RectTransform CommandLog => _manager.CommandLog;

    public List<TextMeshProUGUI> SuggestionPrompts;
    public List<TextMeshProUGUI> CommandLogs;

    public int CurrentNode = 0;

    public DebugCommand CurrentCommand;
    #endregion
    #region ▶ Properties 2
    public string[] InputCommandSet = new string[0]; // Set of command words seperated by 'space'
    public string[] SuggestionCommandSet = new string[0]; // Set of suggestion command for current inputing word
    public List<string> CommandHistory = new(0);

    int _oldCaretPosition = 0;
    public int _currentHistory = -1;

    [SerializeField] private SerializableDictionary<GameObject, Coroutine> _activateCommandLog = new();
    #endregion

    #region ▶ MonoBehaviour
    private void Awake()
    {
        _editor = GetComponentInParent<DebugCommandEditor>();
        _manager = GetComponent<DebugCommandManager>();
        CommandInput.onValueChanged.AddListener(OnChangeValue);

        DebugCommandAction.OnFinishNode += AutoFixWrongCommand;
        DebugCommandAction.OnCaretPositionChanged += UpdateCurrentNodeOnCaretPositionChanged;
        //DebugCommandAction.OnChangeNode += UpdateSuggestionPanelPosition;
        DebugCommandAction.OnShowLog += ShowLog;
        DebugCommandAction.ActivateDebugCommand += ActivateDebugCommand;

        UpdateSuggestionPanelChildrenAmount();
        UpdateLogPanelChildrenAmount();

        ActivateExtensionPanels(false);
        ActivateDebugCommand(false);
    }

    private void OnDestroy()
    {
        DebugCommandAction.OnFinishNode -= AutoFixWrongCommand;
        DebugCommandAction.OnCaretPositionChanged -= UpdateCurrentNodeOnCaretPositionChanged;
        //DebugCommandAction.OnChangeNode -= UpdateSuggestionPanelPosition;
        DebugCommandAction.OnShowLog -= ShowLog;
        DebugCommandAction.ActivateDebugCommand -= ActivateDebugCommand;
    }

    private void Start()
    {
        CurrentCommand = _manager.Commands.GetDetector();

        DebugCommandAction.OnStartInputCommand?.Invoke();

    }

    private void Update()
    {
        if (CommandInput.isFocused)
        {
            if (_editor.EnableRemoveDuplicated) RemoveDuplicatedChar(_editor.RemovableCharacters.ToCharArray());
            if (_editor.EnableRemoveUselessSpace) RemoveDuplicatedUselessSpace();
        }
        else
        {

        }

        if (Input.GetKeyDown(KeyCode.Return)) StartExecuteCommand();
        if (Input.GetKeyDown(KeyCode.Tab) && _editor.EnableFinishCommand) CompleteCurrentWord();

        RewriteCommandFromHistory();

        if (_oldCaretPosition != CommandInput.caretPosition) DebugCommandAction.OnCaretPositionChanged?.Invoke();
        _oldCaretPosition = CommandInput.caretPosition;

        CommandPrompt.gameObject.SetActive(_editor.EnableSuggestionPrompt);

        //CurrentCommand.Update();
    }

    public void OnChangeValue(string text)
    {
        SeparateInputIntoWords(text);
        UpdateCurrentNodeOnPressingSpace(); 
        IdentifyRootCommand();
        SuggestionCommandSet = GetSuggestionList(CurrentCommand.CommandNodes[CurrentNode].Suggestions, CurrentCommand.CommandNodes[CurrentNode].StartWith);
        if (_editor.EnableSuggestionPanel)
        {
            ShowCommandSuggestion();
            UpdateSuggestionPanelPosition();
        }
        if (_editor.EnableSuggestionPrompt) UpdatePromptText();
    }
    #endregion

    #region ▶ Executer Methods
    public void ActivateDebugCommand(bool enable)
    {
        CommandPanel.gameObject.SetActive(enable);
        if (enable)
        {
            CommandInput.Select();
            CommandInput.text = "/";
            CommandInput.caretPosition = 1;
            CommandInput.onValueChanged?.Invoke(CommandInput.text);

            CommandLog.SetActive(true);
            CommandLog.SetActiveAllChildren(true);
        }
    } // ✔✔
    public void ActivateExtensionPanels(bool enable)
    {
        CommandLog.SetActive(enable);
        SuggestionPanel.SetActive(enable);
    } // ✔
    public void SeparateInputIntoWords(string text)
    {
        if (!text.IsNullOrEmpty() && text[0] == '/') InputCommandSet = text.Remove(0, 1).SplitIntoArray(' ');
        else InputCommandSet = text.SplitIntoArray(' ');
    } // ✔
    public void RemoveDuplicatedChar(params char[] characters)
    {
        CommandInput.text = CommandInput.text.RemoveDuplicatedChars(characters);
    } // ✔
    public void RemoveDuplicatedUselessSpace()
    {
        if (CommandInput.text.IsNullOrEmpty()) return;

        if (CommandInput.caretPosition == CommandInput.text.Length)
        {
            if (CommandInput.text[CommandInput.caretPosition - 1] == ' ' && CommandInput.text[CommandInput.caretPosition - 2] == ' ')
            {
                CommandInput.text = CommandInput.text.Remove(CommandInput.caretPosition - 1, 1);
            }
        }
    } // ✔
    public void UpdateCurrentNodeOnPressingSpace()
    {
        int oldNode = CurrentNode;

        if (CommandInput.text.Replace("/", "").IsNullOrEmpty())
        {
            //Debug.Log("Changed On Update 1");
            CurrentNode = 0;
            if (oldNode != CurrentNode) DebugCommandAction.OnChangeNode?.Invoke();
            return;
        }
        if (!CommandInput.text.IsNullOrEmpty() && !(CommandInput.caretPosition - 1 < 0))
        {
            if (CommandInput.text[CommandInput.caretPosition - 1] == ' ')
            {
                //Debug.Log("Changed On Update 2");
                if (CommandInput.caretPosition == CommandInput.text.Length) CurrentNode = InputCommandSet.Length;
                else if (CommandInput.text[CommandInput.caretPosition] != ' ') CurrentNode = InputCommandSet.Length;
            }
            else
            {
                //Debug.Log("Changed On Update 3");
                CurrentNode = InputCommandSet.IsEmpty() ? 0 : InputCommandSet.Length - 1;
            }
        }

        if (CurrentNode >= CurrentCommand.CommandNodes.Count)
        {
            //Debug.Log("Changed On Update 4");
            CurrentNode = CurrentCommand.CommandNodes.Count - 1;
        }

        if (oldNode != CurrentNode) DebugCommandAction.OnChangeNode?.Invoke();
    } // ✔✔✔✔
    public void UpdateCurrentNodeOnCaretPositionChanged()
    {
        string word = CommandInput.text.RemoveAll("/").GetWordAtIndex(CommandInput.caretPosition - 1);
        if (!word.IsNullOrEmpty())
        {
            //Debug.Log("Changed On Position 1");
            CurrentNode = Array.IndexOf(InputCommandSet, word);
            if (CurrentNode >= CurrentCommand.CommandNodes.Count)
            {
                //Debug.Log("Changed On Position 2");
                CurrentNode = CurrentCommand.CommandNodes.Count - 1;
            }
        }
    } // ✔✔
    public void IdentifyRootCommand()
    {
        if (InputCommandSet.IsEmpty())
        {
            CurrentCommand = _manager.Commands.GetDetector();
            return;
        }
        else if (_manager.Commands.Keys.Contains(InputCommandSet[0]))
        {
            CurrentCommand = _manager.Commands.CreateCommand(InputCommandSet[0]);
        }
        else
        {
            CurrentCommand = _manager.Commands.GetDetector();
        }
    } // ✔
    public string[] GetSuggestionList(List<string> originList, bool startWith)
    {
        if (CommandInput.text.IsNullOrEmpty()) return new string[0];

        // Detect if CurrentNode has any input
        if (CurrentNode < InputCommandSet.Length)
        {
            if (startWith)
            {
                string[] getWordsContains = originList.GetWordsStartWith(InputCommandSet[CurrentNode], true);
                if (!getWordsContains.IsNullOrEmpty()) return getWordsContains;
            }
            else
            {
                string[] getWordsContains = originList.GetWordsContain(InputCommandSet[CurrentNode], true);
                if (!getWordsContains.IsNullOrEmpty()) return getWordsContains;
            }
            return originList.FuzzySearch(InputCommandSet[CurrentNode]);
        }
        else
        {
            // Add all suggestion into list
            return originList.ToArray();
        }

    } // ✔✔✔
    public void InvokeOnFinishWordAction()
    {
        if (CommandInput.text.Length >= 2 && InputCommandSet.Length > 0)
        {
            if (CommandInput.text[CommandInput.caretPosition - 2] != ' ') DebugCommandAction.OnFinishNode?.Invoke();
        }
    } // ✔
    public string MergeStringArrayToText(string[] array)
    {
        return "/" + string.Join(" ", array) + " ";
    } // ✔
    public void AutoFixWrongCommand()
    {
        Debug.Log(CurrentNode);

#if true
        if (SuggestionCommandSet.IsEmpty())
        {
            MDebug.Notify("Case 1");
            return;
        }
        if (!SuggestionCommandSet.IsEmpty())
        {
            if (InputCommandSet.Length > CurrentNode)
            {
                MDebug.Notify("Pre-Case 2");
                if (InputCommandSet[CurrentNode] == SuggestionCommandSet[0])
                {
                    MDebug.Notify("Case 2");
                    return;
                }
            }
            MDebug.Notify("Case 3");
            if (CurrentCommand == _manager.Commands.GetDetector()) InputCommandSet[^1] = SuggestionCommandSet[0];
            else
            {
                if (CurrentNode == CurrentCommand.CommandNodes.Count - 1) InputCommandSet[^1] = GetSuggestionList(CurrentCommand.CommandNodes[CurrentNode].Suggestions, CurrentCommand.CommandNodes[CurrentNode].StartWith)[0];
                else InputCommandSet[^1] = GetSuggestionList(CurrentCommand.CommandNodes[CurrentNode - 1].Suggestions, CurrentCommand.CommandNodes[CurrentNode - 1].StartWith)[0];
            }
            CommandInput.text = MergeStringArrayToText(InputCommandSet);
            CommandInput.MoveToEndOfLine(false, false);
        }
#endif
    } // ✘
    public void CompleteCurrentWord()
    {
        if (SuggestionCommandSet.Length > 0)
        {
            if (InputCommandSet.Length <= CurrentNode)
            {
                List<string> getList = InputCommandSet.ToList();
                getList.Add(SuggestionCommandSet[0]);
                InputCommandSet = getList.ToArray();
            }
            else
            {
                InputCommandSet[^1] = SuggestionCommandSet[0];
            }
            CommandInput.text = "/" + string.Join(' ', InputCommandSet);
            CommandInput.MoveToEndOfLine(false, false);
        }

        CommandInput.onValueChanged?.Invoke(CommandInput.text);
    } // ✔
    public void ShowCommandSuggestion()
    {
        if (!SuggestionCommandSet.IsEmpty())
        {
            SuggestionPanel.SetActive(true);
            SuggestionPanel.SetActiveAllChildren(false);
            for (int i = 0; i < SuggestionCommandSet.Length; i++)
            {
                if (i >= SuggestionPrompts.Count) return;
                SuggestionPrompts[i].gameObject.SetActive(true);
                SuggestionPrompts[i].text = SuggestionCommandSet[i];
            }

            if (InputCommandSet.IsEmpty() || CurrentNode >= InputCommandSet.Length) return;
            if (SuggestionCommandSet[0] == InputCommandSet[CurrentNode])
            {
                SuggestionPanel.SetActive(false);
            }
        }
        else SuggestionPanel.SetActive(false);
    } // ✔✔
    public void StartExecuteCommand()
    {
        if (!CommandInput.text.IsNullOrEmpty())
        {
            if (InputCommandSet.Length <= 0)
            {
                DebugCommandAction.OnShowLog?.Invoke(LogType.Failed, $"<color=#FF7070><b>Failed:</b></color> Command is empty.");
                Debug.Log($"<color=#FF7070><b>Failed:</b></color> Command is empty.");
                return;
            }
            else if (CommandInput.text[0] == '/')
            {
                CurrentCommand.Execute(InputCommandSet);
            }
            else Debug.Log("This is not a command");
        }

        CommandPanel.SetActive(false);
        //CommandInput.onDeselect?.Invoke("");
    } // ✔✔✔
    public void UpdateSuggestionPanelPosition()
    {
        SuggestionPanel.anchoredPosition = new((int)Input.compositionCursorPos.x - 10, SuggestionPanel.anchoredPosition.y);
    } // ✔
    public void UpdateSuggestionPanelChildrenAmount()
    {
        SuggestionPrompts.Clear();
        SuggestionPanel.gameObject.DestroyAllChildren();
        for (int i = 0; i < _editor.MaximumSuggestion; i++)
        {
            var tmp = Instantiate(CommandText, SuggestionPanel);
            SuggestionPrompts.Add(tmp);
        }
        SuggestionPanel.SetActiveAllChildren(false);
    } // ✔
    public void UpdatePromptText()
    {
        List<string> getList = new();
        DebugCommand command = _manager.Commands.GetDetector();

        if (CurrentNode == 0) command = _manager.Commands.GetDetector();
        else command = _manager.Commands.CreateCommand(InputCommandSet[0]);

        if (InputCommandSet.IsEmpty())
        {
            getList.Clear();
            getList.Add("command");
        }
        else
        {
            getList.Clear();
            for (int i = 0; i <= CurrentNode; i++)
            {
                if (InputCommandSet.Length > i)
                {
                    string[] suggestionArray = command.CommandNodes[i].Suggestions.GetWordsContain(InputCommandSet[i], true);
                    if (!suggestionArray.IsEmpty()) getList.Add(suggestionArray[0]);
                }
                else getList.Add(command.CommandNodes[i].Nodes);
            }
        }

        CommandPrompt.text = MergeStringArrayToText(getList.ToArray());
    } // ✔
    public void ShowLog(LogType type, string message)
    {
        // Disable all empty logs
        foreach (var child in CommandLogs)
        {
            if (!_activateCommandLog.ContainsKey(child.gameObject)) child.gameObject.SetActive(false);
        }

        // Object-Pooling for logs
        CommandLog.MoveChildToLast(0);
        CommandLogs = CommandLogs.Skip(1).Concat(CommandLogs.Take(1)).ToList();

        TextMeshProUGUI tmp = CommandLogs[^1];
        tmp.text = message;
        tmp.color = Color.white;

        // Coroutine for enable/disable logs
        Coroutine coroutine = StartCoroutine(ShowCommandMessage(tmp.gameObject));
        _activateCommandLog.Add(tmp.gameObject, coroutine);

        WriteCommandToHistory(type);

        CommandInput.text = "";

    } // ✔✔✔✔
    public void UpdateLogPanelChildrenAmount()
    {
        CommandLogs.Clear();
        CommandLog.gameObject.DestroyAllChildren();
        for (int i = 0; i < _editor.MaximumCommandLog; i++)
        {
            var tmp = Instantiate(CommandText, CommandLog);
            tmp.color = Color.clear;
            tmp.gameObject.SetActive(false);
            CommandLogs.Add(tmp);
        }
    } // ✔
    public void WriteCommandToHistory(LogType type)
    {
        if ((!_editor.SaveWrongCommandToHistory && type == LogType.Executed) || _editor.SaveWrongCommandToHistory)
        {
            StartWriting();
        }

        void StartWriting()
        {
            string command = CommandInput.text;
            if (_editor.MaximumCommandHistory != -1)
            {
                if (CommandHistory.Count < _editor.MaximumCommandHistory)
                {
                    if (_editor.RemoveDuplicatedCommandHistory && CommandHistory.Contains(command)) CommandHistory.Remove(command);
                    CommandHistory.Insert(0, command);
                }
            }
        }
    } // ✔
    public void RewriteCommandFromHistory()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentHistory++;
            if (_currentHistory >= CommandHistory.Count) _currentHistory = 0;
            CommandInput.text = CommandHistory[_currentHistory];
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_currentHistory <= 0) _currentHistory = CommandHistory.Count - 1;
            else _currentHistory--;
            CommandInput.text = CommandHistory[_currentHistory];
        } // ✔
    } // ✔
    public IEnumerator ShowCommandMessage(GameObject obj)
    {
        CommandLog.SetActive(true);
        obj.SetActive(true);
        yield return new WaitForSeconds(_editor.TimeToHideCommandMessage);
        obj.SetActive(false);
        if (_activateCommandLog.ContainsKey(obj)) _activateCommandLog.Remove(obj);
        if (_activateCommandLog.Count <= 0) CommandLog.SetActive(false);
    } // ✔✔
    #endregion
}