using UnityEngine;

public class MenuOpenLink : MonoBehaviour
{
    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
}
