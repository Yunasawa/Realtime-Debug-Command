using UnityEngine;
using System.Collections.Generic;

public abstract class DebugCommand
{
    public List<CommandNode> CommandNodes = new();
    public List<string> CommandTexts = new();

    public DebugCommand() { }

    public virtual void Update() { }
    public abstract void Execute(string[] value);
    public virtual void Log(string log, LogType type = LogType.Executed)
    {
        DebugCommandAction.OnShowLog?.Invoke(type, log);
    }

    public void WrongCommand(string command)
    {
        Debug.Log($"<color=#FF3A3A><b>⚠ Command Error:</b></color> <b><color=#9EFFF9>{command}</color></b> is a wrong command!");
    }
}

public class CommandNode
{
    public string Nodes = "";
    public List<string> Suggestions = new();
    public bool StartWith = false;

    public CommandNode(string nodeCommand, List<string> suggestions, bool startWith = false)
    {
        Nodes = nodeCommand;
        Suggestions = suggestions;
        StartWith = startWith;
    }
}