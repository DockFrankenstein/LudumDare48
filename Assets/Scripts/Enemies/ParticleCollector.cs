using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Debug.Log("Player collected point");
            PlayerReference.singleton.points.Score();
            Destroy(other.gameObject);
        }
    }
}
