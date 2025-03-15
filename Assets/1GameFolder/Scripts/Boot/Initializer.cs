using GameFolder.Boot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private List<DataLoaderBase> _allDataForLoad;

    private void Start()
    {
        ProfileData.InitializeFromClient();
        InitializeLoadData();
    }

    private void InitializeLoadData()
    {
        _allDataForLoad = new List<DataLoaderBase>
        {
            new AssetDataLoader()
        };

        foreach (var data in _allDataForLoad)
        {
            data.Load();
        }
    }
}
