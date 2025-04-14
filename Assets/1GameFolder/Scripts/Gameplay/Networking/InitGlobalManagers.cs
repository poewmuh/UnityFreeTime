using Unity.Netcode;
using UnityEngine;

public class InitGlobalManagers : MonoBehaviour
{
    [SerializeField] private NetworkObject _sessionController;
    private void Awake()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(_sessionController);
        }
    }
}
