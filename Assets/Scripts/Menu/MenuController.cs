using System;
using qASIC;
using UnityEngine;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController singleton;

        public MenuInstance[] menus;

        private void Awake()
        {
            AssignSingleton();
        }

        void AssignSingleton()
        {
            if (singleton == null)
            {
                singleton = this;
                return;
            }
            if (singleton != this) Destroy(this);
        }

        public static void ChangeMenu(string menuName)
        {
            if (singleton == null)
            {
                qDebug.LogError("Singleton has not been assigned!");
                return;
            }

            if (singleton.menus.Length == 0) return;

            int menuIndex = 0;
            for (int i = 0; i < singleton.menus.Length; i++)
            {
                singleton.menus[i].Toggle(false);
                if (singleton.menus[i].menuName == menuName) menuIndex = i;
            }

            singleton.menus[menuIndex].Toggle(true);
            for (int i = 0; i < singleton.menus.Length; i++)
                if (Array.IndexOf(singleton.menus[menuIndex].menusToActivate, singleton.menus[i].menuName) >= 0)
                    singleton.menus[i].Toggle(true);
        }
    }
}