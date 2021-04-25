using UnityEngine;
using qASIC;
using UnityEngine.SceneManagement;

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
        PlayerReference.singleton.move.enabled = false;
        PlayerReference.singleton.look.enabled = false;
        PlayerReference.singleton.gameObject.GetComponent<CharacterController>().enabled = false;
        Rigidbody rb = (Rigidbody)PlayerReference.singleton.gameObject.AddComponent(typeof(Rigidbody));

        rb.angularVelocity = new Vector3(Random.Range(0, 3.0f), Random.Range(0, 1.0f), Random.Range(0, 3.0f));

        ScreenBlanker.BlackOutScreen(3, () =>
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
}