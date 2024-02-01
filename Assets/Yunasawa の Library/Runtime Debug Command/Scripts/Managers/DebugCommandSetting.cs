using UnityEngine;

namespace YNL.RuntimeDebugCommand
{
    public class DebugCommandSetting : MonoBehaviour
    {
        private DebugCommandManager _manager;

        #region ▶ Command Input Settings
        public bool EnableRemoveDuplicated = true;
        public string RemovableCharacters = "";
        public bool EnableRemoveUselessSpace = true;

        #endregion

        #region ▶ Command Suggestion Settings
        public bool EnableSuggestionPanel = true;
        public int MaximumSuggestion;
        public bool EnableSuggestionPrompt = true;
        public bool EnableFinishCommand = true;

        #endregion

        #region ▶ Command Log Settings
        public int MaximumCommandMessage = 10;
        public bool RemoveDuplicatedCommandHistory = true;
        public bool SaveWrongCommandToHistory = false;
        public int MaximumCommandHistory = 10;
        public int TimeToHideCommandMessage = 10;

        #endregion


        private void Awake()
        {
            _manager = GetComponentInChildren<DebugCommandManager>();
        }

        private void Start()
        {
            if (RemovableCharacters.Length < 1) RemovableCharacters = @"/\";
        }

        private void Update()
        {
            if (_manager.CommandInput.isFocused) return;
            if (Input.GetKeyDown(KeyCode.Slash)) DebugCommandConstants.ActivateDebugCommand?.Invoke(true, true);
            if (Input.GetKeyDown(KeyCode.T)) DebugCommandConstants.ActivateDebugCommand?.Invoke(true, false);
        }
    }
}