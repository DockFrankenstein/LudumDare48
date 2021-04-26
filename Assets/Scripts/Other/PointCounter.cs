using UnityEngine;
using UnityEngine.SceneManagement;

public class PointCounter : MonoBehaviour
{
    public static PointCounter instance;
    
    int maxPoints;
    int points;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    public static void IncrementMaxPoints()
    {
        instance.maxPoints++;
    }
    public static void ScorePoint()
    {
        instance.points++;
    }
    public static int GetMaximumPoints()
    {
        return instance.maxPoints;
    }
    public static int GetPoints()
    {
        return instance.points;
    }
}
