using System;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Tools
{
    public class DontDestroyOnLoadNet : NetworkBehaviour
    {
        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
