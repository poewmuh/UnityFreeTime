using GameFolder.Gameplay.BattleLocation.Map;

namespace GameFolder.Gameplay.BattleLocation
{
    public class BattleGenerationState : IBattleState
    {
        private BattleStateMachine _stateMachine;
        
        public BattleGenerationState(BattleStateMachine battleStateMachine)
        {
            _stateMachine = battleStateMachine;
        }

        public void Enter()
        {
            MapManager.Instance.GenerateMapOnServer();
            _stateMachine.SetState(BattleState.BattleStart);
        }

        public void Exit() { }

        public void Tick() { }
    }
}