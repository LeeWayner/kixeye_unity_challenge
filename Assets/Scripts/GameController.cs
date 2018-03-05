using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LeeWayner.Singleton;

// This class controls all the mechanics and interactions in this game 
public class GameController : Singleton<GameController>
{
	[SerializeField]
	private int pointPerObstacle;
	[SerializeField]
	private UVMove[] backgroundUVMoves;
	[SerializeField]
	private PlayerController playerController;
	[SerializeField]
	private EnemyGeneratorController enemyGenerator;
	[SerializeField]
	private float timeScaleChangeInterval = 6f; // Every n seconds 
	[SerializeField]
	private float scaleIncrement = .25f;
	[SerializeField]
	private LeaderboardService leaderboardService;
	public GameStateController GameStateController { get; private set; }

	public PlayerController PlayerController
	{
		get
		{
			return playerController;
		}
	}

	public EnemyGeneratorController EnemyGenerator
	{
		get
		{
			return enemyGenerator;
		}
	}

	public int Score { get; set; }

	public float TimeScaleChangeInterval { get { return timeScaleChangeInterval; } }

	public float ScaleIncrement { get { return scaleIncrement; } }

	void Start()
	{
		GameStateController = new GameStateController();
		GameStateController.Start();
	}

	// Update is called once per frame
	void Update()
	{
		GameStateController.Update();		
	}

	public void SetBackgroundMoving(bool _isMoving)
	{
		for (int i = 0; i < backgroundUVMoves.Length; i++)
		{
			backgroundUVMoves[i].SetMoving(_isMoving);
		}
	}

	public void PauseGame(bool _isPause)
	{

	}
	public void IncreaseTimeScale()
	{
		Time.timeScale += ScaleIncrement;
	}

	private IEnumerator IEChangeTimeScale()
	{
		yield return new WaitForSeconds(TimeScaleChangeInterval);
		Time.timeScale += ScaleIncrement;
	}
	// Reset the scale of the time 
	public void ResetTimeScale(float newTimeScale = 1f)
	{
		Time.timeScale = newTimeScale;
	}

	// Updates the obtained points 
	public void IncreasePoints()
	{
		Score += pointPerObstacle;
		UIController.Instance.UpdateScore(Score);
		if (Score >= GetMaxScore())
		{
			SaveMaxScore(Score);
		}
	}

	public void ResetScore()
	{
		Score = 0;
		UIController.Instance.UpdateScore(Score);
	}
	// Gets the actual maximum score 
	public int GetMaxScore()
	{
		return PlayerPrefs.GetInt("Max Points", 0);
	}

	// Saves the new score 
	public void SaveMaxScore(int currentPoints)
	{
		PlayerPrefs.SetInt("Max Points", currentPoints);
	}

	public void UpdateNewHighscore()
	{
		leaderboardService.UpdateScore(Score);
	}
}
