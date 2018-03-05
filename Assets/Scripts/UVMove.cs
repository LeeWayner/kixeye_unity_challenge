using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UVMove : MonoBehaviour {

	private Rect rect;
	private RawImage image;
	private bool isMoving = false;
	[SerializeField]
	private float speed = 0.2f;
	private void Start()
	{
		image = GetComponent<RawImage>();
		rect = new Rect(0, 0, 1, 1);
	}

	private void Update()
	{
		if(isMoving)
		{
			rect.x += speed * Time.deltaTime;
			if(rect.x > 1000)
			{
				rect.x -= 1000;
			}
			image.uvRect = rect;
		}
	}
	public void SetMoving(bool _isMoving)
	{
		isMoving = _isMoving;
	}


}
