using UnityEngine;

public class ParticleCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            FragmentController particle = other.gameObject.GetComponent<FragmentController>();
            if (particle == null || !particle.state.Equals(FragmentController.FragmentState.discharged)) return;
            Destroy(other.gameObject);
            PointCounter.AddPoint();
        }
    }
}
