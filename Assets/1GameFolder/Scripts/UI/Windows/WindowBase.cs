using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TriInspector;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup)), DeclareBoxGroup("Base", Title = "Base")]
public abstract class WindowBase : MonoBehaviour
{
    public event Action<WindowBase> onWindowClose;
    public event Action<WindowBase> onStartClose;

    [Group("Base"), SerializeField] private bool _showOnAwake;

    [Header("Animations")] [Group("Base"), SerializeField] private float _fadeTime = 1f;
    [Group("Base"),SerializeField] private bool _fadeOnShow;
    [Group("Base"),SerializeField] private bool _fadeOnHide;
    
    public bool IsShowed { get; private set; }

    private CanvasGroup _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_showOnAwake)
        {
            Show();
        }
    }

    public virtual void OnCreated()
    {
        _canvasGroup.alpha = 0.1f;
    }

    public virtual void Hide()
    {
        if (_fadeOnHide)
        {
            _canvasGroup.DOFade(0, _fadeTime).OnComplete(() =>
            {
                gameObject.SetActive(false);
                IsShowed = false;
            });
            return;
        }

        _canvasGroup.alpha = 0;
        IsShowed = false;
        gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        if (_fadeOnShow)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, _fadeTime).OnComplete(()=> IsShowed = true);
            gameObject.SetActive(true);
            return;
        }

        _canvasGroup.alpha = 1;
        IsShowed = true;
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        onStartClose?.Invoke(this);
        
        if (IsShowed)
        {
            Hide();
        }

        WaitAndDestroy().Forget();
    }

    private async UniTaskVoid WaitAndDestroy()
    {
        await UniTask.WaitWhile(() => IsShowed, cancellationToken:this.GetCancellationTokenOnDestroy());
        onWindowClose?.Invoke(this);
        Destroy(gameObject);
    }
}
