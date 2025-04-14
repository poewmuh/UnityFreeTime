using System.Collections.Generic;
using GameFolder.Gameplay.Networking;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.BattleLocation
{
    public class BattleStateMachine : GameplaySystem<BattleStateMachine>
    {
        public NetworkVariable<BattleState> currentBattleState = new ();
        private Dictionary<BattleState, IBattleState> _states;
        private IBattleState _currentState;

        protected override void Initialize()
        {
            if (!IsServer) return;
            
            _states = new Dictionary<BattleState, IBattleState> {
                { BattleState.WaitingForPlayers, new WaitingForPlayersState(this) },
                { BattleState.GenerationState, new BattleGenerationState(this) },
                { BattleState.BattleStart, new BattleStartState(this) },
                { BattleState.PlayerTurn, new PlayerTurnState(this) },
                { BattleState.EnemyTurn, new EnemyTurnState(this) },
                { BattleState.Victory, new VictoryState(this) },
                { BattleState.Defeat, new DefeatState(this) },
            };
            
            SetState(BattleState.WaitingForPlayers);
        }

        private void Update()
        {
            _currentState?.Tick();
        }
        
        public void SetState(BattleState newState)
        {
            Debug.Log($"[BattleStateMachine] SET NEW STATE: {newState}");
            _currentState?.Exit();
            currentBattleState.Value = newState;
            _currentState = _states[newState];
            _currentState.Enter();
        }
    }
}