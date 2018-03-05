using LeeWayner.FSM;
using UnityEngine;

public class PauseAction : FSMAction
{
	private float currentTimeScale;
    public PauseAction(FSMState owner) : base(owner)
    {
    }

    public void Init()
    {

    }

    public override void OnEnter()
    {
		UIController.Instance.ShowPausePanel();
		currentTimeScale = Time.timeScale;
		Time.timeScale = 0;
    }

	public override void OnExit()
	{
		UIController.Instance.HidePausePanel();
		Time.timeScale = currentTimeScale;
		currentTimeScale = 0;
	}
}

