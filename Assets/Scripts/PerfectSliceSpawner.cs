using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectSliceSpawner : MonoBehaviour
{
	public int Streak;
	public PerfectSliceAnimation Prefab;
	
	public void PlayAnimation(Vector3 spawnPosition)
	{
		PerfectSliceAnimation animation = Instantiate(Prefab);
		animation.transform.position = spawnPosition;
		
		if (Streak > 5)
		{
			animation.PlayStreak();	
		}
		else
		{
			animation.PlayNormal();	
		}

		Streak++;
	}

	public void ResetStreak()
	{
		Streak = 0;
	}
}
