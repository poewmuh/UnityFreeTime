using System;
using GameFolder.UI.Windows;
using UnityEngine;

namespace GameFolder.UI.Menu
{
    public class MainMenuWindow : WindowBase
    {
        [SerializeField] private GameObject _buttonsHolder;
        public override void Show()
        {
            _buttonsHolder.SetActive(true);
            base.Show();

            WindowsManager.OnWindowOpen += OnSomeWindowOpen;
            WindowsManager.OnWindowStartClose += OnSomeWindowStartClose;
        }

        private void OnSomeWindowOpen(WindowBase window)
        {
            _buttonsHolder.SetActive(false);
        }

        private void OnSomeWindowStartClose(WindowBase window)
        {
            _buttonsHolder.SetActive(true);
        }

        public override void Hide()
        {
            base.Close();
            Unsubscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            WindowsManager.OnWindowOpen -= OnSomeWindowOpen;
            WindowsManager.OnWindowStartClose -= OnSomeWindowStartClose;
        }
    }
}