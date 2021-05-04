using System.Collections.Generic;
using qASIC.Console.Commands;

public class ConsoleInteract : GameConsoleCommand
{
    public override string CommandName { get; } = "interact";
    public override string Description { get; } = "changes players ability to interact";
    public override string Help { get; } = "Use interact; interact <state>";
    public override string[] Aliases { get; } = new string[] { "it" };
    
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

                PlayerReference.interactable = result;
                break;
            default:
                PlayerReference.interactable = !PlayerReference.interactable;
                break;
        }
        Log($"Changed player interact to {PlayerReference.interactable}", "cheat");
    }
}
