using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class DC_Time : DebugCommand // /time selection amount
{
    public DC_Time() : base()
    {
        CommandNodes = new CommandNode[]
        {
            new("time", new[] { "time" }, true, false), // 0

            new("selection", new[] { "timescale" }, true, false), // 1
            new("amount", new[] { "0", "1" }, false, true) // 2
        };
    }

    public override void Execute(string[] value)
    {
        if (CheckWrongCommand(value)) return;

        if (int.TryParse(value[2], out int amount)) { }
        else
        {
            Log($"<#FF7070>Failed:</color> <#FFF087>{value[2]}</color> is in wrong format", LogType.Failed);
            return;
        }
        Time.timeScale = amount;
        Log($"Time set to <#FFE045>{value[2]}</color>");
    }

}




