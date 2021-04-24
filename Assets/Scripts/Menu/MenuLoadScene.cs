using UnityEngine;
using UnityEngine.SceneManagement;
using qASIC;

public class MenuLoadScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            qDebug.LogError("Scene couldn't be loaded!");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
