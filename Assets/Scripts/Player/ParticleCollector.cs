using UnityEngine;
using System.Collections.Generic;

public class ParticleCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            FragmentController particle = other.gameObject.GetComponent<FragmentController>();
            if (particle == null || particle.chasing) return;
            Destroy(other.gameObject);
            PointCounter.ScorePoint();
        }
    }
}
