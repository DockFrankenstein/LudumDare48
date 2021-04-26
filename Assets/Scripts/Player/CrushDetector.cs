using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        PlayerReference.singleton.damage.Kill();
    }
}
