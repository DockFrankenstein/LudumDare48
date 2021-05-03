using UnityEngine;

public class ScoreReset : MonoBehaviour
{
    public bool ResetOnAwake = true;

    private void Awake()
    {
        if (ResetOnAwake) 
            ResetScore();
    }

    public void ResetScore() => PointCounter.ResetScore();
}
