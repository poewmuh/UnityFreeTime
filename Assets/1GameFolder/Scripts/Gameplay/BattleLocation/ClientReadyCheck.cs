using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.BattleLocation
{
    public class ClientReadyCheck : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _statusText;

        private bool _isWaitingStatus;

        private void Start()
        {
            if (BattleStateMachine.Instance.currentBattleState.Value == BattleState.WaitingForPlayers)
            {
                _statusText.text = "Press any button";
                _isWaitingStatus = true;
                BattleStateMachine.Instance.currentBattleState.OnValueChanged += OnBattleStateChange;
            }
        }

        private void Update()
        {
            if (!_isWaitingStatus) return;
            
            if (Input.anyKeyDown)
            {
                _isWaitingStatus = false;
                BattleManager.Instance.NotifyClientReadyRpc(NetworkManager.Singleton.LocalClientId);
                _statusText.text = "Waiting for other players..";
            }
        }

        private void OnBattleStateChange(BattleState prevState, BattleState newState)
        {
            gameObject.SetActive(false);
        }
    }
}