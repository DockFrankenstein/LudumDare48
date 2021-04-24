using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private void Update()
    {
        if (PlayerReference.singleton?.look == null) return;
        transform.eulerAngles = Vector3.right * PlayerReference.singleton.look.rotation.y + Vector3.up * PlayerReference.singleton.look.rotation.x;
    }
}
