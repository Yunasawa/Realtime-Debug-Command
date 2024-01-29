using YNL.Extension.Method;

public class DC_Detector : DebugCommand
{
    public DC_Detector() : base()
    {
        CommandNodes = new()
        {
            new("command", new() 
            { 
                "crafting", 
                "debug", 
                "inventory" 
            }, true)
        };
    }

    public override void Execute(string[] value)
    {
        WrongCommand(value[0]);
        Log($"<#FF7070>Failed:</color> <#FFF087>{value[0]}</color> is a wrong command", LogType.Failed);
    }


    public override void Update()
    {
        MDebug.Notify("Detector Command");
    }
}