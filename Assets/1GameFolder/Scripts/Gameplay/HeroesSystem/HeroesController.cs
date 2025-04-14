using System.Collections.Generic;
using GameFolder.Gameplay.Networking;
using Unity.Netcode;

namespace GameFolder.Gameplay.HeroesSystem
{
    public class HeroesController : GameplaySystem<HeroesController>
    {
        private readonly Dictionary<ulong, PlayerHeroData> _userHeroesData = new();
        protected override void Initialize()
        {
            if (IsServer)
            {
                var sessionController = SessionController.Instance;
                foreach (var user in sessionController.activeSessionUsers)
                {
                    FillHeroData(user);
                }

                sessionController.OnUserConnected += FillHeroData;
            }
        }
        
        public void FillHeroData(SessionUser user)
        {
            _userHeroesData.Add(user.clientId, GetDefaultHeroData(user.clientId));
        }

        public PlayerHeroData GetHeroDataByClientId(ulong clientId)
        {
            return _userHeroesData[clientId];
        }
        
        public Dictionary<ulong, PlayerHeroData> GetAllClientsHeroesData()
        {
            return _userHeroesData;
        }

        public void RequestChangeHero(ulong clientId, int heroId)
        {
            SendChangeHeroToServerRpc(clientId, heroId);
        }

        [Rpc(SendTo.Server)]
        private void SendChangeHeroToServerRpc(ulong clientId, int heroId)
        {
            _userHeroesData[clientId].heroId = heroId;
            SendChangeHeroToAllRpc(clientId, heroId);
        }

        [Rpc(SendTo.NotMe)]
        private void SendChangeHeroToAllRpc(ulong clientId, int heroId)
        {
            if (!_userHeroesData.ContainsKey(clientId))
                _userHeroesData[clientId] = GetDefaultHeroData(clientId);
            
            _userHeroesData[clientId].heroId = heroId;
        }

        private PlayerHeroData GetDefaultHeroData(ulong clientId)
        {
            return new PlayerHeroData()
            {
                heroId = -1,
                ownerClientId = clientId,
                health = 100
            };
        }
    }
}