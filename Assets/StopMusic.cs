using UnityEngine;
using qASIC.AudioManagment;

public class StopMusic : MonoBehaviour
{
    public void StopAllMusic()
    {
        AudioManager.Stop("announcer");
        AudioManager.Stop("ambient");
    }
}
