using UnityEngine;

public class UpdateWindowController : MonoBehaviour
{
    static bool wasOppened;
    public GameObject window;

    private void Awake()
    {
        if (wasOppened) return;
        wasOppened = true;
        window.SetActive(true);
    }
}
