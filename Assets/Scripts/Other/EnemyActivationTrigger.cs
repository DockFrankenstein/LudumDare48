using UnityEngine;

public class EnemyActivationTrigger : MonoBehaviour
{
    public FragmentController target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player") return;
        if (target == null) return;
        target?.SpotPlayer();
    }
}
