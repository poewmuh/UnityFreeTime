using System;
using Cysharp.Threading.Tasks;
using GameFolder.GlobalManagers;
using GameFolder.UI;
using UnityEngine;

public class JoinGameTab : MonoBehaviour
{
    [SerializeField] private CustomButton _quickJoinButton;

    private void Awake()
    {
        _quickJoinButton.onClick.AddListener(OnQuickJoinClick);
    }

    private async void OnQuickJoinClick()
    {
        _quickJoinButton.onClick.RemoveListener(OnQuickJoinClick);
        var lobby = await LobbyHelper.QuickJoinLobby();
        if (lobby == null) return;
        SceneController.LoadNetworkScene(SceneType.HubLocation);
    }
}
