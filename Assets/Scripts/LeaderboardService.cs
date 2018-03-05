using GameSparks.Api.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderboardService : MonoBehaviour {

    public InputField scoreInput;
    public string outputTxt;
	// Use this for initialization
	void Start () {
        //new DeviceAuthenticationRequest().Send((response) =>
        //{
        //    Debug.Log("DeviceAuthenticationRequest.JSON:" + response.JSONString);
        //    Debug.Log("DeviceAuthenticationRequest.HasErrors:" + response.HasErrors);
        //    Debug.Log("DeviceAuthenticationRequest.UserId:" + response.UserId);
        //});
    }

    void Awake()
    {
        GameSparks.Api.Messages.NewHighScoreMessage.Listener += HighScoreMessageHandler; // assign the New High Score message
    }

    void HighScoreMessageHandler(GameSparks.Api.Messages.NewHighScoreMessage _message)
    {
        Debug.Log("NEW HIGH SCORE \n " + _message.LeaderboardName);
        //highScorePopup.GetComponent<Popup>().CallPopup(_message);
    }

    public void PostScoreBttn()
    {
        Debug.Log("Posting Score To Leaderboard...");
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("EVT_SCORE_UPDATE")
            .SetEventAttribute("score", scoreInput.text)
            .Send((response) => {

                if (!response.HasErrors)
                {
                    Debug.Log("Score Posted Sucessfully...");
                }
                else
                {
                    Debug.Log("Error Posting Score...");
                }
            });
    }

    public void GetLeaderboard()
    {
        Debug.Log("Fetching Leaderboard Data...");

        new GameSparks.Api.Requests.LeaderboardDataRequest()
            .SetLeaderboardShortCode("HIGHSCORE")
            .SetEntryCount(10) // we need to parse this text input, since the entry count only takes long
            .Send((response) => {

                if (!response.HasErrors)
                {
                    Debug.Log("Found Leaderboard Data...");
                    outputTxt = System.String.Empty; // first clear all the data from the output
                    foreach (GameSparks.Api.Responses.LeaderboardDataResponse._LeaderboardData entry in response.Data) // iterate through the leaderboard data
                    {
                        int rank = (int)entry.Rank; // we can get the rank directly
                        string playerName = entry.UserName;
                        string score = entry.JSONData["MAX-score"].ToString(); // we need to get the key, in order to get the score
                        outputTxt += rank + "   Name: " + playerName + "        Score:" + score + "\n"; // addd the score to the output text
                        Debug.Log(outputTxt);

                    }
                }
                else
                {
                    Debug.Log("Error Retrieving Leaderboard Data...");
                }

            });
    }
}
