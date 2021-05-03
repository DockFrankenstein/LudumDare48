using System.Collections.Generic;
using qASIC.Console.Commands;

public class ConsoleAILevel : GameConsoleCommand
{
    public override string CommandName { get; } = "ai";
    public override string Description { get; } = "changes enemy AI level";
    public override string Help { get; } = "Use ai; ai <value> where 0 - none; 1 - just follows; 2 - normal";
    public override string[] Aliases { get; } = new string[] { "ailevel" };

    public override void Run(List<string> args)
    {
        if (!CheckForArgumentCount(args, 0, 1)) return;
        switch (args.Count)
        {
            case 2:
                if (!int.TryParse(args[1], out int level))
                {
                    ParseException(args[1], "int");
                    return;
                }

                FragmentController.AILevel = level;
                Log($"Changed AI level to {level}", "AI");
                break;
            default:
                Log($"Current AI level is set to {FragmentController.AILevel}", "AI");
                break;
        }
    }
}
