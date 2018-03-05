﻿using UnityEngine;
using System.Collections;

namespace LeeWayner.FSM
{
	public class FSMAction
	{
		private readonly FSMState owner;

		public FSMAction (FSMState owner)
		{
			this.owner = owner;
            owner.AddAction(this);
		}

		public FSMState GetOwner ()
		{
			return owner;
		}

		///<summary>
		/// Starts the action.
		///</summary>
		public virtual void OnEnter ()
		{
		}

		///<summary>
		/// Updates the action.
		///</summary>
		public virtual void OnUpdate ()
		{
		}

		///<summary>
		/// Finishes the action.
		///</summary>
		public virtual void OnExit ()
		{
		}
	}
}
