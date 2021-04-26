using UnityEngine;
using qASIC;

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
