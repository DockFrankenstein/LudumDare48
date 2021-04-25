using UnityEngine;
using qASIC;

public class PlayerDamage : MonoBehaviour
{
    public float velocityDamageThreshold = 10f;
    public bool reciveFallDamage = true;

    [Header("States")]
    public bool isDead;

    public void HandleVelocity(float velocity)
    {
        if (isDead || !reciveFallDamage) return;
        if (velocity < -velocityDamageThreshold) Kill();
    }

    public void Kill()
    {
        isDead = true;
        qDebug.Log("Player died, restarting level", "Player");
    }
}