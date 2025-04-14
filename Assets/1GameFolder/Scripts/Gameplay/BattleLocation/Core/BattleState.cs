namespace GameFolder.Gameplay.BattleLocation
{
    public enum BattleState
    {
        WaitingForPlayers = 0,
        GenerationState,
        BattleStart,
        PlayerTurn,
        EnemyTurn,
        Victory,
        Defeat
    }
}