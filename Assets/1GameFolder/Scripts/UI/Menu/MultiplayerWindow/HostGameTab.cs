using System;
using Cysharp.Threading.Tasks;
using GameFolder.GlobalManagers;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameFolder.UI.Menu.Multiplayer
{
    public class HostGameTab : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _sessionName;
        [SerializeField] private TMP_Dropdown _isPrivateDropdown;
        [SerializeField] private CustomButton _onHostButton;

        private void Awake()
        {
            _onHostButton.interactable = false;
            _onHostButton.onClick.AddListener(OnClickHost);
            _sessionName.onValueChanged.AddListener(OnChangeSessionName);
        }

        private void OnDestroy()
        {
            _sessionName.onValueChanged.RemoveListener(OnChangeSessionName);
            _onHostButton.onClick.RemoveListener(OnClickHost);
        }

        private void OnChangeSessionName(string sessionName)
        {
            _onHostButton.interactable = sessionName.Length > 0;
        }

        private async void OnClickHost()
        {
            _onHostButton.interactable = false;
            _onHostButton.onClick.RemoveListener(OnClickHost);
            await LobbyHelper.CreateLobby(_sessionName.text, 4, false);
            SceneController.LoadNetworkScene(SceneType.HubLocation);
        }
    }
}