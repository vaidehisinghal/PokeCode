using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayFabManager : MonoBehaviour
{
    public Text messageText;
    public InputField emailInput;
    public InputField passwordInput;
    private void Awake()
    {
        //Event Subscription
        PokemonCollector.OnUpdate += SendLeaderboard;
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged In";
        SceneManager.LoadScene("Main");
    }
    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }
    
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registered and logged in!";
    }

    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
    }

    public void SendLeaderboard( int Score )
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Pokecode Score",
                    Value = Score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log(result);
    }
}