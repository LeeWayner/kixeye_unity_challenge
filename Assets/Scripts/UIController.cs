using System;
using System.Collections;
using System.Collections.Generic;
using LeeWayner.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController> {
	[SerializeField]
	private GameObject inGamePanel, gameoverPanel, instructionText, scorePanel, inputNamePanel;
	[SerializeField]
	private Button startButton, restartButton, quitButton, resumeButton, pauseButton, nameOkButton;

	public void ShowUsernameInputPanel()
	{
		inputNamePanel.SetActive(true);
		inGamePanel.SetActive(false);
		gameoverPanel.SetActive(false);
		scorePanel.SetActive(false);
		instructionText.SetActive(false);
	}

	[SerializeField]
	private Text scoreTxt, gameoverScoreTxt, gameOverBestScoreTxt;
	[SerializeField]
	private InputField nameInput;
	public void ShowPreStartPanel()
	{
		inputNamePanel.SetActive(false);
		inGamePanel.SetActive(true);
		gameoverPanel.SetActive(false);
		scorePanel.SetActive(false);
		instructionText.SetActive(false);
		startButton.gameObject.SetActive(true);
		restartButton.gameObject.SetActive(false);
		quitButton.gameObject.SetActive(true);
		resumeButton.gameObject.SetActive(false);
		pauseButton.gameObject.SetActive(false);
	}
	public void ShowPausePanel()
	{
		inputNamePanel.SetActive(false);
		inGamePanel.SetActive(true);
		gameoverPanel.SetActive(false);
		scorePanel.SetActive(true);
		instructionText.SetActive(false);
		startButton.gameObject.SetActive(false);
		restartButton.gameObject.SetActive(true);
		quitButton.gameObject.SetActive(true);
		resumeButton.gameObject.SetActive(true);
	}

	public void HidePausePanel()
	{
		inGamePanel.SetActive(false);
	}

	public void ShowGameOverPanel()
	{
		inGamePanel.SetActive(false);
		gameoverPanel.SetActive(true);
		scorePanel.SetActive(true);
		instructionText.SetActive(false);
		pauseButton.gameObject.SetActive(false);
		gameoverScoreTxt.text = GameController.Instance.Score.ToString("N0");
		gameOverBestScoreTxt.text = GameController.Instance.GetMaxScore().ToString("N0");
	}

	public void ShowInstruction()
	{
		inGamePanel.SetActive(false);
		gameoverPanel.SetActive(false);
		scorePanel.SetActive(false);
		instructionText.SetActive(true);
		pauseButton.gameObject.SetActive(false);
	}

	public void ShowGamePlayUI()
	{
		inGamePanel.SetActive(false);
		gameoverPanel.SetActive(false);
		scorePanel.SetActive(true);
		instructionText.SetActive(false);
		pauseButton.gameObject.SetActive(true);
	}

	public void OnStartButtonClicked()
	{
		GameController.Instance.GameStateController.ChangeState(GameState.Idle);
	}
	public void OnRestartButtonClicked()
	{
		GameController.Instance.EnemyGenerator.CancelGenerator(true);
		GameController.Instance.GameStateController.ChangeState(GameState.Idle);
	}

	public void OnQuitButtonClicked()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
	}
	public void OnResumeButtonClicked()
	{
		GameController.Instance.GameStateController.ChangeState(GameState.Play);
	}

	public void OnPauseButtonClicked()
	{
		GameController.Instance.GameStateController.ChangeState(GameState.Pause);
	}
	
	public void OnFinishInstruction()
	{
		GameController.Instance.GameStateController.ChangeState(GameState.Play);
	}

	public void OnNameInputChanged(string _nameInput)
	{
		if(_nameInput.Trim().Length > 0)
		{
			nameOkButton.interactable = true;
		}
		else
		{
			nameOkButton.interactable = false;
		}
	}
	public void OnNameOkButtonClicked()
	{
		PlayerPrefs.SetString("username", nameInput.text.Trim());
		ShowPreStartPanel();
	}
	public void UpdateScore(int _score)
	{
		scoreTxt.text = _score.ToString("N0");
	}
}
