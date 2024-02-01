using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UIElements;

namespace YNL.RuntimeDebugCommand
{
    [CustomEditor(typeof(DebugCommandSetting))]
    public class DebugCommandEditorWindow : Editor
    {
        [SerializeField] DebugCommandSetting main;

        private void Awake()
        {
            main = (DebugCommandSetting)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();


            GUIStyle labelStyle = GUI.skin.GetStyle("Label");
            labelStyle.fontSize = 13;
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.alignment = TextAnchor.MiddleCenter;

            GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1);
            using (new GUILayout.VerticalScope("box"))
            {
                GUI.backgroundColor = Color.white;

                GUI.contentColor = new(0.357f, 1, 0.906f, 1);
                EditorGUILayout.LabelField("Command Input Settings ▼", labelStyle);
                EditorGUILayout.Space(10);
                GUI.contentColor = Color.white;

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Remove Duplicated Chars ▶", "Remove all duplicated characters in InputField and leave just one there."));
                    main.EnableRemoveDuplicated = EditorGUILayout.Toggle(new GUIContent(""), main.EnableRemoveDuplicated);
                    GUILayout.FlexibleSpace();
                }

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Removable Chars ▶", "Characters that can be removed if duplicated"));
                    main.RemovableCharacters = EditorGUILayout.TextField(new GUIContent(""), main.RemovableCharacters, GUILayout.MinWidth(0), GUILayout.MaxWidth(1920));
                    GUILayout.FlexibleSpace();
                }

                EditorGUILayout.Space(5);

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Remove Useless Space ▶", "Remove all duplicated spaces at the end of line."));
                    main.EnableRemoveUselessSpace = EditorGUILayout.Toggle("", main.EnableRemoveUselessSpace);
                    GUILayout.FlexibleSpace();
                }
            }

            GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1);
            using (new GUILayout.VerticalScope("box"))
            {
                GUI.backgroundColor = Color.white;

                GUI.contentColor = new(0.357f, 1, 0.906f, 1);
                EditorGUILayout.LabelField("Command Suggestion Settings ▼", labelStyle);
                EditorGUILayout.Space(10);
                GUI.contentColor = Color.white;

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Enable Suggestion Panel ▶", "Allow [Suggestion Panel] to show up."));
                    main.EnableSuggestionPanel = EditorGUILayout.Toggle("", main.EnableSuggestionPanel);
                    GUILayout.FlexibleSpace();
                }

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Max Suggestion Showed ▶", "Only display [Maximum] suggestions, even when the actual amount is larger."));
                    main.MaximumSuggestion = EditorGUILayout.IntField("", main.MaximumSuggestion, GUILayout.MinWidth(0), GUILayout.MaxWidth(1920));
                    GUILayout.FlexibleSpace();
                }

                EditorGUILayout.Space(5);

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Enable Suggestion Prompt ▶", "Allow [Suggestion Prompt] to show up."));
                    main.EnableSuggestionPrompt = EditorGUILayout.Toggle("", main.EnableSuggestionPrompt);
                    GUILayout.FlexibleSpace();
                }

                EditorGUILayout.Space(5);

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Enable Suggestion Prompt ▶", "Allow completing inputing command by pressing [Tab] key."));
                    main.EnableFinishCommand = EditorGUILayout.Toggle("", main.EnableFinishCommand);
                    GUILayout.FlexibleSpace();
                }
            }


            GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1);
            using (new GUILayout.VerticalScope("box"))
            {
                GUI.backgroundColor = Color.white;

                GUI.contentColor = new(0.357f, 1, 0.906f, 1);
                EditorGUILayout.LabelField("Command Log Settings ▼", labelStyle);
                EditorGUILayout.Space(10);
                GUI.contentColor = Color.white;

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Max Message Showed ▶", "Only display [Maximum] messages on [Command Log]."));
                    main.MaximumCommandMessage = EditorGUILayout.IntField("", main.MaximumCommandMessage, GUILayout.MinWidth(0), GUILayout.MaxWidth(1920));
                    GUILayout.FlexibleSpace();
                }

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Time To Hide Message ▶", "Time to hide command messages on [Command Log] after showed by seconds."));
                    main.TimeToHideCommandMessage = EditorGUILayout.IntField("", main.TimeToHideCommandMessage, GUILayout.MinWidth(0), GUILayout.MaxWidth(1920));
                    GUILayout.FlexibleSpace();
                }

                EditorGUILayout.Space(5);

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Remove Duplicated Command ▶", "Remove that just inputted [Command] if it's already in history."));
                    main.RemoveDuplicatedCommandHistory = EditorGUILayout.Toggle("", main.RemoveDuplicatedCommandHistory);
                    GUILayout.FlexibleSpace();
                }

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Save Wrong Command ▶", "Allow saving wrong [Command] to history."));
                    main.SaveWrongCommandToHistory = EditorGUILayout.Toggle("", main.SaveWrongCommandToHistory);
                    GUILayout.FlexibleSpace();
                }

                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField(new GUIContent("Max Commands In History ▶", "Only save [Maximum] amount of commands to history."));
                    main.MaximumCommandHistory = EditorGUILayout.IntField("", main.MaximumCommandHistory, GUILayout.MinWidth(0), GUILayout.MaxWidth(1920));
                    GUILayout.FlexibleSpace();
                }
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(main);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}