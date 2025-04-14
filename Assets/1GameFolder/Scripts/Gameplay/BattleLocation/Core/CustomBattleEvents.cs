using System;

namespace GameFolder.Gameplay.BattleLocation
{
    public static class CustomBattleEvents
    {
        public static Action<ulong> OnPlayerReady;

        public static void NotifyPlayerReady(ulong clientId) 
        {
            OnPlayerReady?.Invoke(clientId);
        }
    }
}