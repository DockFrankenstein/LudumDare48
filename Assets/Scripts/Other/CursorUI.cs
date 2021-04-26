using UnityEngine;

public class CursorUI : MonoBehaviour
{
    public string stateName;

    public bool invert;
    public bool onAwake;

    private void Awake()
    {
        if (onAwake) ChangeState(true);
    }

    public void ChangeState(bool state)
    {
        CursorManager.ChangeState(stateName, state != invert);
    }
}
