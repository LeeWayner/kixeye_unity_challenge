using UnityEngine;
using System.Collections;
using LeeWayner.FSM;

public class GameOverAction	: FSMAction
{
	public GameOverAction(FSMState owner) : base(owner)
	{
	}

	public void Init()
	{

	}

	public override void OnEnter()
	{
		UIController.Instance.ShowGameOverPanel();
		GameController.Instance.UpdateNewHighscore();
		GameController.Instance.EnemyGenerator.CancelGenerator(true);
		GameController.Instance.SetBackgroundMoving(false);
		GameController.Instance.ResetTimeScale(1);
	}
}
