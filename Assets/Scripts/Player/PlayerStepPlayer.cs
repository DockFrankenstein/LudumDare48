using UnityEngine;

public class PlayerStepPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;
    public float walkDelay = 0.5f;
    public float runDelay = 0.4f;

    float time;

    bool isFloating;

    private void Update()
    {
        if (!CanPlay()) return;

        float delay = GetDelay();
        time += Time.deltaTime;
        if (time >= delay) Play();
        time %= delay;
    }

    bool CanPlay()
    {
        if (PlayerReference.singleton?.move == null || PlayerReference.singleton?.damage == null) return false;
        if (PlayerMove.noclip || PlayerReference.singleton.damage.isDead) return false;

        if (PlayerReference.singleton.move.isGround && isFloating) 
        { 
            time = GetDelay();
            isFloating = !PlayerReference.singleton.move.isGround;
            return true;
        }
        isFloating = !PlayerReference.singleton.move.isGround;
        if (PlayerReference.singleton.move.walking || !PlayerReference.singleton.move.isGround) return false;
        return true;
    }

    float GetDelay() => PlayerReference.singleton.move.running ? runDelay : walkDelay;

    void Play()
    {
        source.Stop();
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }
}
