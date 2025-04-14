using System;
using Unity.Netcode;
using Unity.Networking.Transport.Utilities;
using UnityEngine;

namespace GameFolder.Gameplay.UnitComponents
{
    public class Unit : NetworkBehaviour
    {
        [SerializeField] private StatsBoard _statsBoard;

        public StatsBoard statsBoard => _statsBoard;
        public SimpleMovement Movement { get; private set; }
        public UnitInteractions Interactions { get; private set; }
        
        private void Start()
        {
            Movement = GetComponent<SimpleMovement>();
            Movement.Init(this);
            Interactions = new UnitInteractions(this);
            
            if (IsOwner)
            {
                CameraController.Instance.SetFocusOn(transform);
            }
        }
    }
}