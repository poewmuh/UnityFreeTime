using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoaderHandler
{
    private AsyncOperationHandle _handler;

    public T LoadImmediate<T>(string path) where T : Object
    {
        LoadInternal<GameObject>(path);
        _handler.WaitForCompletion();
        return (_handler.Result as GameObject).GetComponent<T>();
    }

    private void LoadInternal<T>(string path)
    {
        _handler = Addressables.LoadAssetAsync<T>(path);
    }

    public void Unload()
    {
        Addressables.Release(_handler);
    }
}
