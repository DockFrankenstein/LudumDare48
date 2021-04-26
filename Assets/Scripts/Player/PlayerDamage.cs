using UnityEngine;
using qASIC;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public float velocityDamageThreshold = 10f;
    public bool reciveFallDamage = true;

    [Header("States")]
    public bool isDead;

    [Header("Dead Rigidbody")]
    public Vector3 maxDrag = new Vector3(3f, 1f, 3f);

    public void HandleVelocity(float velocity)
    {
        if (isDead || !reciveFallDamage) return;
        if (velocity < -velocityDamageThreshold) Kill();
    }

    public void Kill()
    {
        if (isDead) return;
        isDead = true;
        qDebug.Log("Player died, restarting level", "Player");

        if (PlayerReference.singleton?.move == null || PlayerReference.singleton?.look == null)
        {
            qDebug.LogError("Cannot kill player, player reference not set to an instance of an object! Loading scene instant");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        PlayerReference.singleton.move.enabled = false;
        PlayerReference.singleton.look.enabled = false;
        PlayerReference.singleton.move.charControl.enabled = false;
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();

        rb.angularVelocity = new Vector3(Random.Range(0, maxDrag.x), Random.Range(0, maxDrag.y), Random.Range(0, maxDrag.z));

        ScreenBlanker.BlackOutScreen(3, () =>
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
}