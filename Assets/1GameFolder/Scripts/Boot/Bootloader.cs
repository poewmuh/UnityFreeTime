using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameFolder.GlobalManagers;
using UnityEngine;

namespace GameFolder.Boot
{
    public class Bootloader : MonoBehaviour
    {
        [SerializeField] private float _customLoadDelay = 2;
        
        private void Start()
        {
            LoadMenu(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid LoadMenu(CancellationToken cancellationToken)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_customLoadDelay), cancellationToken: cancellationToken);
            SceneController.LoadScene(SceneType.MainMenu);
        }
    }
}