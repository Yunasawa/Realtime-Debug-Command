using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugCommandManager : MonoBehaviour
{
    [HideInInspector] public DebugCommandList Commands = new();
    private DebugCommandExecuter _executer;

    public GameObject CommandPanel;
    public TMP_InputField CommandInput;
    public TextMeshProUGUI CommandPrompt;
    public RectTransform SuggestionPanel;
    public TextMeshProUGUI CommandText;
    public RectTransform CommandLog;

    private void Awake()
    {
        _executer = GetComponent<DebugCommandExecuter>();

        CommandInput.onSelect.AddListener(delegate { _executer.gameObject.SetActive(true); });
        CommandInput.onDeselect.AddListener(delegate { _executer.gameObject.SetActive(false); });

        UpdateCommandLibrary();
    }

    private void Start()
    {
        _executer.gameObject.SetActive(false);
    }

    public void UpdateCommandLibrary()
    {
        Commands.Keys = new() { "detector" };
        Commands.Keys.AddRange(new List<string>() 
        { 
            "time", "debug"
        });
    }
}

[System.Serializable]
public class DebugCommandList
{
    public List<string> Keys = new();

    public DebugCommandList() { }

    public DebugCommand GetDetector() => new DC_Detector();
    public DebugCommand CreateCommand(string key)
    {
        switch (key)
        {
            case "debug": return new DC_Debug();
            case "time": return new DC_Time();
        }
        return new DC_Detector();
    }
}