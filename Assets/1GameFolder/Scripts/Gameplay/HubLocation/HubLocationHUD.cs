using GameFolder.Tools;
using UnityEngine;

namespace GameFolder.Gameplay.HubLocation
{
    public class HubLocationHUD : MonoSingleton<HubLocationHUD>
    {
        [SerializeField] private GameObject _heroWindow;

        public void ShowChangeHeroWindow()
        {
            _heroWindow.SetActive(true);
            HubUnitsManager.mineUnit.Movement.SetActiveMovement(false);
        }

        public void CloseHeroChangeWindow()
        {
            _heroWindow.SetActive(false);
            HubUnitsManager.mineUnit.Movement.SetActiveMovement(true);
        }
    }
}