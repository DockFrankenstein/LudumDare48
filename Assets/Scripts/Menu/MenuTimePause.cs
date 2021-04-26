using UnityEngine;

public class MenuTimePause : MonoBehaviour
{
    public bool SetOnAwake;
    public float StartTime;

    private void Awake()
    {
        if (SetOnAwake) SetTime(StartTime);
    }

    public void SetTime(float time)
    {
        Time.timeScale = time;
    }

    public void PauseTime(bool paused) => SetTime(paused ? 0f : 1f);
}
