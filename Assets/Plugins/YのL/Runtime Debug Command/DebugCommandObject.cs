using System.Collections.Generic;

public static class DebugCommandObject
{
    public static string[] CommandList = new string[]
    {
        "debug", "time"
    };
}

[System.Serializable]
public class DebugCommandList
{
    DebugCommandManager _manager;

    public string[] Keys = new string[0];

    public DebugCommandList(DebugCommandManager manager)
    {
        _manager = manager;
    }

    public DebugCommand GetDetector() => new DC_Detector(_manager);
    public DebugCommand CreateCommand(string key)
    {
        switch (key)
        {
            case "debug": return new DC_Debug();
            case "time": return new DC_Time();
        }
        return new DC_Detector(_manager);
    }
}