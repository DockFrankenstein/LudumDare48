using UnityEngine;

public class CursorUI : MonoBehaviour
{
    public string stateName;

    public bool invert;

    public void ChangeState(bool state)
    {
        CursorManager.ChangeState(stateName, state != invert);
    }
}
