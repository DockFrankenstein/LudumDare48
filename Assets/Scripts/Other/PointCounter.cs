using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PointCounter : MonoBehaviour
{
    public class LevelPointData
    {
        public int maxPoints = 0;
        public int collectedPoints = 0;
    }

    public static Dictionary<int, LevelPointData> PointData = new Dictionary<int, LevelPointData>();

    private void Awake()
    {
        Initialize();      
    }

    private void Initialize()
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        if (!PointData.ContainsKey(sceneID))
        {
            PointData.Add(sceneID, new LevelPointData());
            return;
        }
        PointData[sceneID] = new LevelPointData();
    }

    public static void AddMaxPoint() => PointData[GetCurrentPointDataID()].maxPoints++;
    public static void AddPoint() => PointData[GetCurrentPointDataID()].collectedPoints++;

    private static int GetCurrentPointDataID()
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        if (!PointData.ContainsKey(sceneID))
            PointData.Add(sceneID, new LevelPointData());
        return sceneID;
    }

    public static int GetMaxPoints()
    {
        int points = 0;
        foreach (var levelData in PointData)
            points += levelData.Value.maxPoints;
        return points;
    }

    public static int GetPoints()
    {
        int points = 0;
        foreach (var levelData in PointData)
            points += levelData.Value.collectedPoints;
        return points;
    }
}
