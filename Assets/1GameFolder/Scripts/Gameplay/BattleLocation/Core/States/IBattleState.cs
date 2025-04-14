namespace GameFolder.Gameplay.BattleLocation
{
    public interface IBattleState
    {
        void Enter();
        void Exit();
        void Tick();
    }
}