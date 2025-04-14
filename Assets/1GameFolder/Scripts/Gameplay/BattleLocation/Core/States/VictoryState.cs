namespace GameFolder.Gameplay.BattleLocation
{
    public class VictoryState : IBattleState
    {
        private BattleStateMachine _stateMachine;
        
        public VictoryState(BattleStateMachine battleStateMachine)
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