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
            var settingsWindow = WindowsManager.CreateWindow<SettingsWindow>(SettingsWindow.assetPath, transform.parent);
            if (settingsWindow)
            {
                settingsWindow.Show();
            }
        }

        public void OnClickExit()
        {
            Application.Quit();
        }
    }
}