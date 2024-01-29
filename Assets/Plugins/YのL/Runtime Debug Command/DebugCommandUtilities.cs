using System;

public static class DebugCommandAction
{
    public static Action<bool> ActivateDebugCommand;
    public static Action OnStartInputCommand;
    public static Action OnCaretPositionChanged;
    public static Action OnChangeNode;
    public static Action OnFinishNode;
    public static Action OnCommandSuccess;
    public static Action OnCommandFail;
    public static Action<LogType, string> OnShowLog;
    public static Action<string, string[]> OnExecuteCommand;
}

public static class DebugCommandCode
{
    public const string DetectorKey = "detector";
}

public enum LogType
{
    Executed, Failed
}