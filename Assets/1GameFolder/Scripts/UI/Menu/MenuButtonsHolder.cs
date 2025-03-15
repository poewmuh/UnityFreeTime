using GameFolder.UI.Menu.Settings;
using GameFolder.UI.Windows;
using UnityEngine;

namespace GameFolder.UI.Menu
{
    public class MenuButtonsHolder : MonoBehaviour
    {
        public void OnClickPlay()
        {
            Debug.Log("[MenuButtonHolder] On Play Button Clicked");
        }

        public void OnClickSettings()
        {
            WindowsManager.CreateWindow<SettingsWindow>(SettingsWindow.assetPath, transform.parent);
        }

        public void OnClickExit()
        {
            Application.Quit();
        }
    }
}