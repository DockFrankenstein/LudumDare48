using UnityEngine;

public class PlatformDetection : MonoBehaviour
{
    public string platformTag;

    Transform platformTransform;

    public bool isPlatform;

    public Vector3 platformMove;

    Vector3 previous;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != platformTag) return;
        platformTransform = other.transform;
        isPlatform = true;
        previous = platformTransform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != platformTag) return;
        platformTransform = null;
        isPlatform = false;
        previous = Vector3.zero;
    }
    
    private void Update()
    {
        qASIC.Displayer.InfoDisplayer.DisplayValue("is platform", isPlatform.ToString(), "debug");
        if (!isPlatform)
        {
            platformMove = Vector3.zero;
            return;
        }
        platformMove = platformTransform.position - previous;
        previous = platformTransform.position;
    }
}
