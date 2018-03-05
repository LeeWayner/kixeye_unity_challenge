
using LeeWayner.FSM;
using UnityEngine;

public class PlayAction : FSMAction
{
    private float timer = 0;
    public PlayAction(FSMState owner) : base(owner)
    {
    }

    public void Init()
    {

    }
    public override void OnEnter()
    {
		timer = 0;		
		UIController.Instance.ShowGamePlayUI();
		GameController.Instance.ResetScore();
		GameController.Instance.SetBackgroundMoving(true);
		GameController.Instance.PlayerController.Run();
		GameController.Instance.EnemyGenerator.StartGenerating();
	}

	public override void OnExit()
	{
		GameController.Instance.SetBackgroundMoving(false);
	}

	public override void OnUpdate()
	{
		timer += Time.deltaTime;
		if(timer >= GameController.Instance.TimeScaleChangeInterval)
		{
			GameController.Instance.IncreaseTimeScale();
			timer = 0;
		}
	}
}

