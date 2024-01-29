using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DC_Time : DebugCommand // /time selection amount
{
    public DC_Time() : base()
    {
        CommandNodes = new()
        {
            new("time", new() { "time" }, true), // 0

            new("selection", new() { "timescale" }, true), // 1
            new("amount", new() { "0", "1" }) // 2
        };
    }

    public override void Execute(string[] value)
    {
        int amount = int.Parse(value[2]);
        Time.timeScale = amount;
    }

}




