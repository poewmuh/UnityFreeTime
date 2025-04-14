namespace GameFolder.Gameplay.BattleLocation
{
    public class PlayerTurnState : IBattleState
    {
        private BattleStateMachine _stateMachine;
        
        public PlayerTurnState(BattleStateMachine battleStateMachine)
        {
            _stateMachine = battleStateMachine;
        }

        public void Enter() { }

        public void Exit() { }

        public void Tick() { }
    }
}