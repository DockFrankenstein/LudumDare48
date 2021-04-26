using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenBlanker : MonoBehaviour
{
    static ScreenBlanker instance;
    BlankScreen blankScreen;
    
    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        blankScreen = GetComponentInChildren<BlankScreen>();
        ShowScreen(); // potential bugs origins from there...
        SceneManager.sceneLoaded += SceneLoaded;
    }
    private void SceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        PresentScreen(null);
    }
    public static void BlackOutScreen(Action onBlackedOutEvent)
    {
        instance.blankScreen.HideAnimated(onBlackedOutEvent);
    }
    public static void BlackOutScreen(float duration, Action onBlackedOutEvent)
    {
        instance.blankScreen.HideAnimated(duration, onBlackedOutEvent);
    }
    public static void PresentScreen(Action onPresentedCallback)
    {
        instance.blankScreen.ShowAnimated(onPresentedCallback);
    }
    public static void SetBlackScreen()
    {
        instance.blankScreen.Hide();
    }
    public static void ShowScreen()
    {
        instance.blankScreen.Show();
    }

}
