using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WindowBase : MonoBehaviour
{
    public event Action<WindowBase> onWindowClose;

    public virtual void OnCreated()
    {
        
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        Hide();
        onWindowClose?.Invoke(this);
        Destroy(gameObject);
    }
}
