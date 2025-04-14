using System;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.Networking
{
    public class SpawnNetworkObjects : MonoBehaviour
    {
        private void Awake()
        {
            if (!NetworkManager.Singleton.IsHost) return;
            
            foreach (var netObj in GetComponentsInChildren<NetworkObject>())
            {
                if (!netObj.IsSpawned)
                {
                    Debug.Log($"NET OBJECT SPAWNED {netObj.gameObject.name}");
                    netObj.Spawn();
                }
            }
        }
    }
}