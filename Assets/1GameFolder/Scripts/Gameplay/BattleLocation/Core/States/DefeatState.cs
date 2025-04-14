namespace GameFolder.Gameplay.BattleLocation
{
    internal class DefeatState : IBattleState
    {
        private BattleStateMachine _stateMachine;
        
        public DefeatState(BattleStateMachine battleStateMachine)
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