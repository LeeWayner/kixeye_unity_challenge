using System.Collections;
using System.Collections.Generic;
using GameSparks.Api.Requests;
using LeeWayner.FSM;
using UnityEngine;

public class PreStartAction : FSMAction
{
	public PreStartAction(FSMState owner) : base(owner)
	{
	}
	public void Init()
	{

	}

	public override void OnEnter()
	{
		if (PlayerPrefs.GetString("username", string.Empty) == string.Empty)
		{
			UIController.Instance.ShowUsernameInputPanel();
		}
		else
		{
			UIController.Instance.ShowPreStartPanel();
			GameController.Instance.StartCoroutine(CheckAuthenGameSpark());
		}
	}

	IEnumerator CheckAuthenGameSpark()
	{
		while (!GameSparks.Core.GS.Available)
		{
			yield return new WaitForSeconds(1f);
		}

		if (!GameSparks.Core.GS.Authenticated)
		{
			new DeviceAuthenticationRequest()
			.SetDisplayName(PlayerPrefs.GetString("username", string.Empty))
			.Send((response) =>
			{
				Debug.Log("DeviceAuthenticationRequest.JSON:" + response.JSONString);
				Debug.Log("DeviceAuthenticationRequest.HasErrors:" + response.HasErrors);
				Debug.Log("DeviceAuthenticationRequest.UserId:" + response.UserId);
			});
		}
	}
}
