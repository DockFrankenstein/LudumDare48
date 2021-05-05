using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using qASIC.Console.Commands;

public class ConsoleKill : GameConsoleCommand
{
    public override string CommandName { get; } = "kill";
    public override string Description { get; } = "kills player";
    public override string Help { get; } = "kills player";
    
    public override void Run(List<string> args)
    {
        if (!CheckForArgumentCount(args, 0)) return;
        Log("Executing Player Kill", "player");
        PlayerReference.singleton.damage.Kill();
    }
}
