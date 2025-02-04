using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform Target;
	public float Offset = -0.5f;
	[Range(1, 25)] public float Decay = 16;

	private void Update()
	{
		if (Target == null)
		{
			return;
		}
		
		Vector3 position = transform.position;
		Vector3 targetPosition = Target.position;
		targetPosition.y += Offset;

		position = ExpDecay(position, targetPosition, Decay);
		transform.position = position;
	}
	
	// Source: https://www.youtube.com/watch?v=LSNQuFEDOyQ&t=3006s
	private Vector3 ExpDecay(Vector3 a, Vector3 b, float decay)
	{
		return b + (a - b) * Mathf.Exp(-decay * Time.deltaTime);
	}
}