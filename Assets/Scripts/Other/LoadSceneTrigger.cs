using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneTrigger : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return; 
        ScreenBlanker.BlackOutScreen(() =>
             SceneManager.LoadScene(sceneName));
    }
}
