namespace GameFolder.Gameplay.BattleLocation
{
    public class EnemyTurnState : IBattleState
    {
        private BattleStateMachine _stateMachine;
        
        public EnemyTurnState(BattleStateMachine battleStateMachine)
        {
            _stateMachine = battleStateMachine;
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}