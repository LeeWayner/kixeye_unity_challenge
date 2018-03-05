using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
public class PlayerController : MonoBehaviour
{
	private Animator mAnimator; // Controls the sprite animations 
	private float initialY; // To check double jump when no grounded 
	private bool gamePlaying; // State of the game 
	private bool isGrounded; // If the player is on the ground 



	private bool userAction; // Checks for the user keys inputs

	void Start()
	{
		mAnimator = GetComponent<Animator>();
		initialY = this.transform.position.y;
		LeanTouch.OnFingerDown += OnFingureDown;
	}

	private void OnFingureDown(LeanFinger fingerInfo)
	{
		isGrounded = this.transform.position.y == initialY;
		gamePlaying = GameController.Instance.GameStateController.CurrentState == GameState.Play;
		userAction = !fingerInfo.IsOverGui;

		if (isGrounded && gamePlaying && userAction)
		{
			mAnimator.SetTrigger("jump");
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		// Tags are used to check whose the enemy 
		if (other.gameObject.CompareTag("Enemy"))
		{
			// Logic changes 
			mAnimator.SetBool("dead", true);
			mAnimator.SetBool("run", false);
			GameController.Instance.GameStateController.ChangeState(GameState.GameOver);
		}
		else if (other.gameObject.CompareTag("Point"))
		{
			GameController.Instance.IncreasePoints();
		}
	}

	public void ResetToIdle()
	{
		mAnimator.Play("PlayerIdle");
		mAnimator.SetBool("dead", false);
		mAnimator.SetBool("run", false);

	}

	public void Run()
	{
		mAnimator.SetBool("run", true);
	}

}
