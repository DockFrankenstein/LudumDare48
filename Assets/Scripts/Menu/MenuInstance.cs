using UnityEngine;

namespace Menu
{
    public class MenuInstance : MonoBehaviour
    {
        public string menuName;
        public string[] menusToActivate;
        public GameObject[] ObjectsToEnable;

        public void Toggle(bool state)
        {
            for (int i = 0; i < ObjectsToEnable.Length; i++)
                ObjectsToEnable[i].SetActive(state);
            gameObject.SetActive(state);
        }

        public void ChangeMenu(string menu) =>
            MenuController.ChangeMenu(menu);
    }
}