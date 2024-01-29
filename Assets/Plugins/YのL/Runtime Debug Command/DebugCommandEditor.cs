using UnityEngine;

[System.Serializable]
public class DebugCommandEditor : MonoBehaviour
{
    public bool EnableSuggestionPanel = true;
    public int MaximumSuggestion;

    public bool EnableSuggestionPrompt = true;

    public bool EnableFinishCommand = true;

    public bool EnableRemoveDuplicated = true;
    public string RemovableCharacters = "";

    public bool EnableRemoveUselessSpace = true;

    public int MaximumCommandLog = 10;

    // If true: After reused/typed a command that existed in history, the old one will be removed.
    public bool RemoveDuplicatedCommandHistory = true;

    // If false: Wrong command will not be saved into history.
    public bool SaveWrongCommandToHistory = false;

    // [Maximum] amount of commands saved in history; -1 for infinite amount (Not recommended).
    public int MaximumCommandHistory = 10;

    public int TimeToHideCommandMessage = 10;

    private void Start()
    {
        if (RemovableCharacters.Length < 1) RemovableCharacters = @"/\";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            DebugCommandAction.ActivateDebugCommand?.Invoke(true);
        }
    }
}