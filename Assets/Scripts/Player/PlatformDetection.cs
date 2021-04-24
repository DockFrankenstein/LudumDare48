using UnityEngine;

public class PlatformDetection : MonoBehaviour
{
    public string platformTag;

    Transform platformTransform;

    public bool isPlatform;

    public Transform Player;

    Vector3 pos;

    private void Awake()
    {
        pos = transform.localPosition;
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != platformTag) return;
        platformTransform = other.transform;
        isPlatform = true;
        Player.SetParent(platformTransform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != platformTag) return;
        platformTransform = null;
        isPlatform = false;
        Player.SetParent(null);
    }

    private void Update()
    {
        transform.position = Player.position + pos;
        transform.eulerAngles = Player.eulerAngles;
    }
}
