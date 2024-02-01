using System.Linq;
using UnityEngine;
using YNL.RuntimeDebugCommand;

public class DC_Debug : DebugCommand // /debug selection message
{
    public DC_Debug() : base()
    {
        CommandNodes = new CommandNode[]
        {
            new("debug", new[] { "debug" }, true, false), // 0

            new("selection", new[] { "log", "warning", "ca ution", "notify", "error" }, true, false), // 1

            new("message", new string[0], false, true) // 2 
        };
    }

    public override void Execute(string[] value)
    {
        if (CheckWrongCommand(value)) return;

        string content = string.Join(" ", value.Skip(2));

        string color = "";
        switch (value[1])
        {
            case "log": color = "9EFFF9"; break;
            case "warning": color = "FFE045"; break;
            case "caution": color = "FF983D"; break;
            case "notify": color = "79FF53"; break;
            case "error": color = "FF3A3A"; break;
        }
        Log($"<#{color}>{value[1].ToTitleCase()}:</color> <#FFFFFF>{content}</color>");
        Debug.Log($"<color=#{color}><b>{value[1].ToTitleCase()} ▶</b></color> <color=#FFFFFF>{content}</color>");
    }
}



