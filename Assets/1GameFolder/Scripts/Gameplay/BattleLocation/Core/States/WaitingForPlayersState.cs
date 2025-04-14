using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.BattleLocation
{
    public class WaitingForPlayersState : IBattleState
    {
        private readonly BattleStateMachine _stateMachine;
        private readonly HashSet<ulong> _readyPlayers = new();
        public WaitingForPlayersState(BattleStateMachine battleStateMachine)
        {
            _stateMachine = battleStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Waiting for all players...");
            CustomBattleEvents.OnPlayerReady += OnPlayerReady;
        }

        public void Exit()
        {
            CustomBattleEvents.OnPlayerReady -= OnPlayerReady;
        }

        public void Tick()
        {
        }
        
        private void OnPlayerReady(ulong clientId) 
        {
            if (_readyPlayers.Add(clientId) && _readyPlayers.Count == NetworkManager.Singleton.ConnectedClients.Count)
            {
                SetBattleState();
            }
        }

        private void SetBattleState()
        {
            _stateMachine.SetState(BattleState.GenerationState);
        }
    }
}