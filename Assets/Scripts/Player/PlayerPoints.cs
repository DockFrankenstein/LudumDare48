using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public int Points { get; private set; }

    public void Score() => Points++;
}
