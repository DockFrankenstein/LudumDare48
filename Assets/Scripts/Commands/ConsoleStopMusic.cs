using System.Collections.Generic;
using qASIC.AudioManagment;
using qASIC.Console.Commands;

public class ConsoleStopMusic : GameConsoleCommand
{
    public override string CommandName { get; } = "stopmusic";
    public override string Description { get; } = "stops music on audio manager";
    public override string Help { get; } = "stops music on audio manager";
    public override string[] Aliases { get; } = new string[] { "sm" };
    
    public override void Run(List<string> args)
    {
        if (!CheckForArgumentCount(args, 0)) return;
        AudioManager.Stop("announcer");
        AudioManager.Stop("ambient");
    }
}
