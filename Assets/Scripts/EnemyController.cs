using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float velocity = 2f;
	private Rigidbody2D rb2d; 

	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D>();
	}

	public void StartMove()
	{
		rb2d.velocity = Vector2.left * velocity;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		// Tags are used to identify which object 
		if (other.gameObject.CompareTag("Destroyer")) {
			GameController.Instance.EnemyGenerator.RestoreEnemy(this);
		}

	}
}
