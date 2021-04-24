using System.Collections.Generic;
using qASIC.Console.Commands;
using UnityEngine;

public class ConsoleNoclip : GameConsoleCommand
{
    public override string CommandName { get; } = "noclip";
    public override string Description { get; } = "toggles noclip";
    public override string Help { get; } = "toggles noclip";
    public override string[] Aliases { get; } = new string[] { "nc" };
    
    public override void Run(List<string> args)
    {
        if (!CheckForArgumentCount(args, 0)) return;
        PlayerMove.noclip = !PlayerMove.noclip;
        for (int i = 0; i < 31; i++)
            if(LayerMask.LayerToName(i).Length > 0)
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), i, PlayerMove.noclip);
        Log($"Noclip has been {(PlayerMove.noclip ? "enabled" : "disabled")}", "cheat");
    }
}
