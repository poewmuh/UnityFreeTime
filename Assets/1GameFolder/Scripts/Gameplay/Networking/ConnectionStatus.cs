using System;
using GameFolder.Tools;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.Networking
{
    public class ConnectionStatus : RareUpdateBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pingText;

        private NetworkTransport _netTransport;
        private void Awake()
        {
            _netTransport = NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        }

        protected override void RareUpdate()
        {
            ulong clientIdForRtt = NetworkManager.ServerClientId;
            if (NetworkManager.Singleton.IsServer)
            {
                foreach (var connectedClientId in NetworkManager.Singleton.ConnectedClientsIds)
                {
                    if (connectedClientId != NetworkManager.ServerClientId)
                    {
                        clientIdForRtt = connectedClientId;
                    }
                }
            }

            _pingText.text = $"Ping: {_netTransport.GetCurrentRtt(clientIdForRtt)}";
        }
    }
}