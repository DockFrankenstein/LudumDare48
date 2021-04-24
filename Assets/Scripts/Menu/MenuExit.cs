using UnityEngine;

namespace Menu
{
    public class MenuExit : MonoBehaviour
    {
        public void Exit()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer) return;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}