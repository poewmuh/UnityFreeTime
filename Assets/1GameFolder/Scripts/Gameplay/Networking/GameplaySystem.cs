using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.Networking
{
    public abstract class GameplaySystem<T> : NetworkBehaviour where T : NetworkBehaviour
    {
        public static T Instance => _instance as T;
        private static GameplaySystem<T> _instance;

        protected void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"[GameplaySystem] Detect multiple instance {GetType()}");
            }

            _instance = this;
        }
        
        protected void Start()
        {
            Initialize();
        }

        protected abstract void Initialize();

        public override void OnDestroy()
        {
            Debug.Log($"[GameplaySystem] Destroyed instance {GetType()}");
			
            if (_instance == this)
            {
                _instance = null;
            }
            
            base.OnDestroy();
        }
    }
}