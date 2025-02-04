using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectSliceAnimation : MonoBehaviour
{
	public Animator Animator;

	public void PlayStreak()
	{
		Animator.Play("Streak");
	}
	
	public void PlayNormal()
	{
		Animator.Play("Normal");
	}	
}
