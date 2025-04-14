using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameFolder.Gameplay.UnitComponents
{
    public class UnitInteractions
    {
        public Action<Unit> OnInteractionClicked;

        private Unit _unit;
        private bool _isInteractionActive;
        private CancellationTokenSource _cancellationTokenSource;

        public UnitInteractions(Unit unit)
        {
            _unit = unit;
        }

        public void EnableInteractions()
        {
            _isInteractionActive = true;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            WaitInteraction(_cancellationTokenSource.Token).Forget();
        }

        public void DisableInteractions()
        {
            _isInteractionActive = false;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;
        }

        private async UniTaskVoid WaitInteraction(CancellationToken _cancellationToken)
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OnInteractionClicked?.Invoke(_unit);
                }

                await UniTask.DelayFrame(1, cancellationToken: _cancellationToken);
            }
        }
    }
}