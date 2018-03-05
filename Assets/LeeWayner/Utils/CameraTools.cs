using UnityEngine;

namespace LeeWayner.CameraTools
{
	public static class CameraTools
	{
		public static Rect GetScreenRect() 
		{
			Camera cam = Camera.main;
			float height = 2f * cam.orthographicSize;
			float width = height * cam.aspect;
			return new Rect (cam.transform.position.x - width/2, cam.transform.position.y - height/2, width, height);
		}
	}
}