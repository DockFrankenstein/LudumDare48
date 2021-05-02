using System.Collections.Generic;
using UnityEngine;

public static class CursorManager
{
    private static Dictionary<string, bool> states = new Dictionary<string, bool>();

    /// <summary>true is locked, false is none</summary>
    public static bool GlobalState { get; private set; }

    public static void ChangeState(string stateName, bool state)
    {
        if (!states.ContainsKey(stateName)) states.Add(stateName, state);
        states[stateName] = state;
        RefreshGlobal();
    }

    public static void RefreshGlobal()
    {
        GlobalState = true;
        foreach (var item in states)
            if (!item.Value)
                GlobalState = false;
        Cursor.lockState = GlobalState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
