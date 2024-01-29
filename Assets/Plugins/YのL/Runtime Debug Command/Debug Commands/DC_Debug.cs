using System.Linq;
using UnityEngine;
using YNL.Extension.Method;

public class DC_Debug : DebugCommand // /debug selection message
{
    public DC_Debug() : base()
    {
        CommandNodes = new()
        {
            new("debug", new() { "debug" }, true), // 0

            new("selection", new() { "log", "warning", "caution", "notify", "error" }, true), // 1
            new("message", new() { }) // 2  
        };
    }

    public override void Execute(string[] value)
    {
        string content = string.Join(" ", value.Skip(2));

        if (CommandNodes[1].Suggestions.Contains(value[1]))
        {
            string color = "";
            switch (value[1])
            {
                case "log": color = "9EFFF9"; break;
                case "warning": color = "FFE045"; break;
                case "caution": color = "FF983D"; break;
                case "notify": color = "79FF53"; break;
                case "error": color = "FF3A3A"; break;
            }
            Debug.Log($"<color=#{color}><b>▶ {value[1]}: </b></color> {content}");
            Log($"<#{color}>{value[1].ToTitleCase()}:</color> <#FFFFFF>{content}</color>");
        }
        else
        {
            WrongCommand(value[1]);
            Log($"<#FF7070>Failed:</color> <#FFF087>{value[1]}</color> is a wrong command", LogType.Failed);
        }
    }
}



