using System.Collections.Generic;
using qASIC.Console.Commands;

public class ConsoleSpeed : GameConsoleCommand
{
    public override string CommandName { get; } = "speed";
    public override string Description { get; } = "changes player speed";
    public override string Help { get; } = "Use speed; speed <value>";
    public override string[] Aliases { get; } = new string[] { "sd" };

    public override void Run(List<string> args)
    {
        if (!CheckForArgumentCount(args, 0, 1)) return;
        switch (args.Count)
        {
            case 2:
                if (!float.TryParse(args[1], out float result))
                {
                    ParseException(args[1], "float");
                    return;
                }

                PlayerMove.speedMultiplier = result;
                Log($"Changed player speed multiplier to {result}", "cheat");
                break;
            default:
                Log($"Current speed multiplier: {PlayerMove.speedMultiplier}", "cheat");
                break;
        }
    }
}