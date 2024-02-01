using System;
using System.Linq;

namespace YNL.RuntimeDebugCommand
{
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
            if (_manager.CommandInput.text.Contains('/')) Log($"<#FF7070><b>Failed:</b></color> <#FFF087>{value[0]}</color> is a wrong command", ExecuteType.Failed);
            else Log($"<#FFE045><b>@</b></color>: {_manager.CommandInput.text}", ExecuteType.Failed);
        }
    }
}