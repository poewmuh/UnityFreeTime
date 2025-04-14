using System;
using GameFolder.Tools;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class LobbyManager : SharedBehaviour
{
    public LobbyUser currentLobbyUser;

    protected override void Init()
    {
        TrySignIn();
    }

    private async void TrySignIn()
    {
        if (string.IsNullOrEmpty(Application.cloudProjectId))
        {
            OnSignInFailed();
            return;
        }
        
        try
        {
            var initilizationOption = new InitializationOptions();
            initilizationOption.SetProfile("Player_" + Random.Range(0, 10000));
            await UnityServices.InitializeAsync(initilizationOption);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            
            OnSignInComplete();
        }
        catch (Exception)
        {
            OnSignInFailed();
        }
    }

    private void OnSignInFailed()
    {
        Debug.LogError("[LobbyManager] SignIn Failed");
    }

    private void OnSignInComplete()
    {
        Debug.Log($"[LobbyManager] SIGN IN COMPLETE ID: {AuthenticationService.Instance.PlayerId}");
        currentLobbyUser = new LobbyUser(AuthenticationService.Instance.PlayerId, "noName");
    }
}
