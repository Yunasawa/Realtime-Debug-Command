using System.Linq;
using TMPro;
using UnityEngine;

namespace YNL.RuntimeDebugCommand
{
    public class DebugCommandManager : MonoBehaviour
    {
        [HideInInspector] public DebugCommandList Commands;
        private DebugCommandExecuter _setting;

        public GameObject CommandPanel;
        public TMP_InputField CommandInput;
        public TextMeshProUGUI CommandPrompt;
        public RectTransform SuggestionPanel;
        public TextMeshProUGUI CommandText;
        public RectTransform CommandLog;

        private void Awake()
        {
            _setting = GetComponent<DebugCommandExecuter>();

            CommandInput.onSelect.AddListener(delegate { _setting.gameObject.SetActive(true); });
            CommandInput.onDeselect.AddListener(delegate { _setting.gameObject.SetActive(false); });

            UpdateCommandLibrary();
        }

        private void Start()
        {
            _setting.gameObject.SetActive(false);
        }

        public void UpdateCommandLibrary()
        {
            Commands = new(this);
            Commands.Keys = new[] { "detector" };
            Commands.Keys = Commands.Keys.Concat(DebugCommandObject.CommandList).ToArray();
        }
    }
}