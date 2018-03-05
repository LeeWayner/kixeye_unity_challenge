using GameSparks.Api.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class LeaderboardService : MonoBehaviour
{
	[SerializeField]
	private string restApiUrl;
	//public InputField scoreInput;
	private string outputTxt;
	// Use this for initialization
	void Start()
	{
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

	public void PostScoreGameSparks(int newScore)
	{
		Debug.Log("Posting Score To Leaderboard...");
		new GameSparks.Api.Requests.LogEventRequest()
			.SetEventKey("EVT_SCORE_UPDATE")
			.SetEventAttribute("score", newScore)
			.Send((response) =>
			{

				if (!response.HasErrors)
				{
					Debug.Log("Score Posted Sucessfully...");
					GetLeaderboard();
				}
				else
				{
					Debug.Log("Error Posting Score..." + response.Errors);
				}
			});
	}

	public IEnumerator PostScoreRestAPI(int newScore)
	{
		UserScore userScore = new UserScore { userName = PlayerPrefs.GetString("username", string.Empty), score = newScore };
		string dataJson = JsonUtility.ToJson(userScore);
		Debug.Log("user score json = " + dataJson);
		UnityWebRequest request = UnityWebRequest.Post(restApiUrl, dataJson);
		request.method = "POST";
		request.SetRequestHeader("accept", "application/json; charset=UTF-8");
		request.SetRequestHeader("content-type", "application/json; charset=UTF-8");
		yield return request.SendWebRequest();

		if (request.isNetworkError || request.isHttpError)
		{
			Debug.Log(request.error);
			if(request.responseCode == 404)
			{
				Debug.Log("Username not found (user has not registered with the leaderboard service)");
			}
			else if (request.responseCode == 405)
			{
				Debug.Log("Invalid Username supplied");
			}
		}
		else
		{
			Debug.Log("score update complete!");
		}
	}
	public void UpdateScore(int newScore)
	{
		StartCoroutine(PostScoreRestAPI(newScore));
		PostScoreGameSparks(newScore);
	}
	public void GetLeaderboard()
	{
		Debug.Log("Fetching Leaderboard Data...");

		new GameSparks.Api.Requests.LeaderboardDataRequest()
			.SetLeaderboardShortCode("HIGHSCORE")
			.SetEntryCount(10) // we need to parse this text input, since the entry count only takes long
			.Send((response) =>
			{

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
					}
				}
				else
				{
					Debug.Log("Error Retrieving Leaderboard Data...");
				}
				Debug.Log(outputTxt);

			});
	}
}

public class UserScore
{
	public string userName;
	public int score;
}