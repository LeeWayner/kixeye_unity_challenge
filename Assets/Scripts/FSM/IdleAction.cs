
using LeeWayner.FSM;
using UnityEngine;

public class IdleAction : FSMAction
{
    private float timer = 0;
	private float increaseLovePointInterval = 0;
    public IdleAction(FSMState owner) : base(owner)
    {
    }

    public void Init()
    {

    }
    public override void OnEnter()
    {
		UIController.Instance.ShowInstruction();
		GameController.Instance.PlayerController.ResetToIdle();
	}	    
}

