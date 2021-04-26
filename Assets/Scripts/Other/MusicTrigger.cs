using UnityEngine;
using qASIC.AudioManagment;

public class MusicTrigger : MonoBehaviour
{
    public bool playOnce = true;

    public AudioData data;

    bool wasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (playOnce && wasPlayed) return;
        if (other.tag != "Player") return;
        wasPlayed = true;
        AudioManager.Play("ambient", data);
    }
}
