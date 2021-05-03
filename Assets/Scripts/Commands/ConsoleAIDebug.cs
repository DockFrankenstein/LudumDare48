using System.Collections.Generic;
using qASIC.Console.Commands;

public class ConsoleAIDebug : GameConsoleCommand
{
    public override string CommandName { get; } = "aidebug";
    public override string Description { get; } = "shows ai debug values";
    public override string Help { get; } = "Use aidebug; aidebug <state>";

    public override void Run(List<string> args)
    {
        if (!CheckForArgumentCount(args, 0, 1)) return;
        switch (args.Count)
        {
            case 2:
                if (!bool.TryParse(args[1], out bool result))
                {
                    ParseException(args[1], "bool");
                    return;
                }
                FragmentController.showDebugValues = result;
                break;
            default:
                FragmentController.showDebugValues = !FragmentController.showDebugValues;
                break;
        }
        Log($"Changed AI debug to {FragmentController.showDebugValues}", "AI");
    }
}
