using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class CreateUser : MonoBehaviour
{

    public string username;
    public int player_score;

    public PlayFabAuthenticationContext authContext;

    public void createUser() {
        LoginWithCustomIDRequest req = new LoginWithCustomIDRequest();
        req.CreateAccount = true;
        req.CustomId = username;
        req.TitleId = PlayFabSettings.TitleId;
        PlayFabClientAPI.LoginWithCustomID(req, loginSuccessCallback, loginErrorCallback);
    }

    public void storeScore() {

        List<StatisticUpdate> stats = new List<StatisticUpdate>();
        StatisticUpdate score = new StatisticUpdate();
        score.StatisticName = "score";
        score.Value = player_score;

        stats.Add(score);

        UpdatePlayerStatisticsRequest req = new UpdatePlayerStatisticsRequest();
        req.AuthenticationContext = authContext;
        req.Statistics = stats;

        PlayFabClientAPI.UpdatePlayerStatistics(req, statsSuccessCallback, statsErrorCallback);
    }


    void statsSuccessCallback(UpdatePlayerStatisticsResult result) {
        Debug.Log("stats update success!");
        Debug.Log(result.ToJson());
    }

    void statsErrorCallback(PlayFabError err) {
        Debug.Log(err.ToString());
    }


    void loginSuccessCallback(LoginResult result) {
        Debug.Log("Login success!");
        authContext = result.AuthenticationContext;
    }

    void loginErrorCallback(PlayFabError err) {
        Debug.Log(err.GenerateErrorReport());
    }


}
