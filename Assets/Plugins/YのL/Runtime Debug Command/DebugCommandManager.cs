using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugCommandManager : MonoBehaviour
{
    [HideInInspector] public DebugCommandList Commands;
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
        Commands = new(this);
        Commands.Keys = new[] { "detector" };
        Commands.Keys = Commands.Keys.Concat(DebugCommandObject.CommandList).ToArray();
    }
}