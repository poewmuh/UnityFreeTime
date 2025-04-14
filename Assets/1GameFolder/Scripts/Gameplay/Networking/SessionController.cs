using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFolder.Gameplay.Networking
{
    public class SessionController : GameplaySystem<SessionController>
    {
        public Action<SessionUser> OnUserConnected;
        public Action<SessionUser> OnUserDisconnected;
        
        public readonly List<SessionUser> activeSessionUsers = new(4);
        
        protected override void Initialize()
        {
            SubscribeOnConnection();
        }
        
        private void SubscribeOnConnection()
        {
            if (NetworkManager.IsHost)
            {
                if (NetworkManager.ServerIsHost)
                {
                    RegisterSessionUser(NetworkManager.LocalClientId);
                }

                NetworkManager.OnClientConnectedCallback += OnClientConnected;
                NetworkManager.OnClientDisconnectCallback += OnClientDisconnected;
            }
        }

        public override void OnDestroy()
        {
            UnsubscriveOnConnection();
            base.OnDestroy();
        }

        private void OnClientConnected(ulong clientId)
        {
            Debug.Log($"[GameplayNetworking] JOINED CLIENT WITH ID: {clientId}");
            RegisterSessionUser(clientId);
        }

        private void OnClientDisconnected(ulong clientId)
        {
            Debug.Log($"[GameplayNetworking] DISCONNECTED CLIENT WITH ID: {clientId}");
            var sessionUser = activeSessionUsers.Find(x => x.clientId == clientId);
            if (sessionUser != null)
            {
                activeSessionUsers.Remove(sessionUser);
                OnUserDisconnected?.Invoke(sessionUser);
            }
        }

        private void RegisterSessionUser(ulong clientId)
        {
            var sessionUser = new SessionUser(clientId);
            activeSessionUsers.Add(sessionUser);
            
            OnUserConnected?.Invoke(sessionUser);
        }
        
        private void UnsubscriveOnConnection()
        {
            if (!NetworkManager) return;
            
            if (NetworkManager.IsHost)
            {
                NetworkManager.OnClientConnectedCallback -= OnClientConnected;
                NetworkManager.OnClientDisconnectCallback -= OnClientDisconnected;
            }
        }
    }
}