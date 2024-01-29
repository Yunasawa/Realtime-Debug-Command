using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#if false
[CustomEditor(typeof(DebugCommandEditor))]
public class DebugCommandEditorWindow : Editor
{
    [SerializeField] DebugCommandEditor main;

    private void Awake()
    {
        main = (DebugCommandEditor)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox("With this enabled, it will allow [Suggestion Panel] to show up and display [Maximum] suggestions.", MessageType.None);
        main.EnableSuggestionPanel = EditorGUILayout.Toggle("Enable ▶", main.EnableSuggestionPanel);
        main.MaximumSuggestion = EditorGUILayout.IntField("Maximum ▶", main.MaximumSuggestion);

        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox("Command Log Panel will show [Maximum] command logs.", MessageType.None);
        main.MaximumCommandLog = EditorGUILayout.IntField("Maximum ▶", main.MaximumCommandLog);

        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox("With this enabled, it will allow [Suggestion Prompt] to show up.", MessageType.None);
        main.EnableSuggestionPrompt = EditorGUILayout.Toggle("Enable ▶", main.EnableSuggestionPrompt);

        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox("With this enabled, pressing [Tab] to complete command is allowed.", MessageType.None);
        main.EnableFinishCommand = EditorGUILayout.Toggle("Enable ▶", main.EnableFinishCommand);

        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox("With this enabled, it will remove all duplicated characters in InputField and leave just one there.", MessageType.None);
        main.EnableRemoveDuplicated = EditorGUILayout.Toggle("Enable ▶", main.EnableRemoveDuplicated);
        main.RemovableCharacters = EditorGUILayout.TextField("Characters ▶", main.RemovableCharacters);

        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox("With this enabled, it will remove duplicated spaces at the end of line.", MessageType.None);
        main.EnableRemoveUselessSpace = EditorGUILayout.Toggle("Enable ▶", main.EnableRemoveUselessSpace);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(main);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif