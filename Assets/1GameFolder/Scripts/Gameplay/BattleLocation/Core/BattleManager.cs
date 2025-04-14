using System;
using GameFolder.Gameplay.Networking;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.BattleLocation
{
    public class BattleManager : GameplaySystem<BattleManager>
    {
        protected override void Initialize()
        {
            
        }

        [Rpc(SendTo.Server)]
        public void NotifyClientReadyRpc(ulong clientId)
        {
            CustomBattleEvents.NotifyPlayerReady(clientId);
        }
    }
}