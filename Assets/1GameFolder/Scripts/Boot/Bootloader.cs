using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameFolder.GlobalManagers;
using UnityEngine;

namespace GameFolder.Boot
{
    public class Bootloader : MonoBehaviour
    {
        private const float fadeTime = 1;
        
        [SerializeField] private CanvasGroup _mainCanvas;
        [SerializeField] private float _customLoadDelay = 2;
        
        private void Start()
        {
            LoadMenu(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid LoadMenu(CancellationToken cancellationToken)
        {
            _mainCanvas.DOFade(1, fadeTime);
            await UniTask.Delay(TimeSpan.FromSeconds(fadeTime + _customLoadDelay), cancellationToken: cancellationToken);
            _mainCanvas.DOFade(0, fadeTime);
            await UniTask.Delay(TimeSpan.FromSeconds(fadeTime), cancellationToken: cancellationToken);
            SceneController.LoadScene(SceneType.MainMenu);
        }
    }
}