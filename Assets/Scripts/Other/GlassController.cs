using UnityEngine;

public class GlassController : MonoBehaviour
{
    public TriggerController collision;
    public LayerMask enemyMask;
    public ParticleSystem glassParticles;
    public GameObject Glass;

    private void Awake()
    {
        collision?.CollisionEnter?.AddListener(OnCollision);
    }

    public void OnCollision(Collision collision)
    {
        if (((1 << collision.transform.gameObject.layer) & enemyMask) == 0) return;
        Destroy(Glass);
        glassParticles?.Play();
    }
}
