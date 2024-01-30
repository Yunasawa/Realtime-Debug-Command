using System.Linq;

public abstract class DebugCommand
{
    protected DebugCommandManager _manager;

    public CommandNode[] CommandNodes = new CommandNode[0];

    public DebugCommand() { }

    public DebugCommand(DebugCommandManager manager)
    {
        _manager = manager;
    }

    public virtual void Update() { }
    public abstract void Execute(string[] value);
    public virtual void Log(string log, LogType type = LogType.Executed)
    {
        DebugCommandAction.OnShowLog?.Invoke(type, log);
    }

    public bool CheckWrongCommand(string[] commandNodes)
    {
        for (int i = 0; i < commandNodes.Length; i++)
        {
            if (CommandNodes[i].Customable) return false;
            if (!CommandNodes[i].Suggestions.Contains(commandNodes[i]))
            {
                Log($"<#FF7070>Failed:</color> <#FFF087>{commandNodes[i]}</color> is a wrong command", LogType.Failed);
                return true;
            }
        }
        return false;
    }
}

public class CommandNode
{
    public string Nodes = "";
    public string[] Suggestions = new string[0];
    public bool MustStartWith = false;
    public bool Customable = false;

    public CommandNode(string nodeCommand, string[] suggestions, bool startWith = false, bool customable = false)
    {
        Nodes = nodeCommand;
        Suggestions = suggestions;
        MustStartWith = startWith;
        Customable = customable;
    }
}