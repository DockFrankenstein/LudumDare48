using UnityEngine;
using qASIC.AudioManagment;

public class PlayAudioAwake : MonoBehaviour
{
    public string channelName;
    public AudioData data;

    private void Awake()
    {
        if (data == null) return;
        AudioManager.Play(channelName, data);
    }
}
