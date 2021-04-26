using UnityEngine;
using qASIC;
using qASIC.AudioManagment;

public class AmbientManager : MonoBehaviour
{
    public AudioClip ambient;

    public static AmbientManager singleton;

    public AudioData data;

    private void Awake()
    {
        AssignSingleton();
        Play();
    }

    private void Update()
    {
        Transform target = Camera.main.transform;
        if (target == null) return;
        transform.position = target.position;
    }

    private void AssignSingleton()
    {
        if (singleton == null)
        {
            singleton = this;
            qDebug.Log("Assigned Ambient singleton", "Audio");
        }
        if (singleton != this)
        {
            if(singleton.ambient != ambient)
                singleton.ChangeAmbient(ambient);
            Destroy(gameObject);
        }
    }

    public void ChangeAmbient(AudioClip clip)
    {
        ambient = clip;
        Play();
    }

    public void Play()
    {
        data.clip = ambient;
        if (data.clip == null)
        {
            AudioManager.Stop("ambient");
            return;
        }
        AudioManager.Play("ambient", data);
    }
}
