using UnityEngine;

public class PlatformDetection : MonoBehaviour
{
    public LayerMask platformMask;
    PlatformBase platform;
    public bool isPlatform;
    public Vector3 platformMove;

    public float radius;

    private void CheckPlatform()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, platformMask);
        if (isPlatform && colliders.Length != 0) return;

        for (int i = 0; i < colliders.Length; i++)
        {
            PlatformBase colliderBase = colliders[i].GetComponent<PlatformBase>();
            if (colliderBase == null) continue;
            platform = colliderBase;
            isPlatform = true;
            return;
        }
        isPlatform = false;
    }

    private void Update()
    {
        CheckPlatform();

        if (!isPlatform)
        {
            platformMove = Vector3.zero;
            return;
        }
        Debug.Log(platform.direction);
        platformMove = platform.direction;
    }
}
