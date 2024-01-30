using System;
using System.Linq;
using YNL.Extension.Method;

public class DC_Detector : DebugCommand
{
    public DC_Detector(DebugCommandManager manager) : base(manager)
    {
        CommandNodes = new CommandNode[]
        {
            new("command", _manager.Commands.Keys.Except(new[] { "detector" }).ToArray(), true)
        };
    }

    public override void Execute(string[] value)
    {
        if (_manager.CommandInput.text[0] == '/') Log($"<#FF7070><b>Failed:</b></color> <#FFF087>{value[0]}</color> is a wrong command", LogType.Failed);
        else Log($"<#FFE045><b>@</b></color>: {_manager.CommandInput.text}", LogType.Failed);
    }


    public override void Update()
    {
        MDebug.Notify("Detector Command");
    }
}