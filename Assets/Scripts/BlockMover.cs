using System;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
	public GameObject blockPrefab;
	public float moveSpeed = 5f;
	public float bound = 3f;
	public Transform currentBlock;
	public bool moveAlongZ = false;
	public bool moveForward = true;
	
	private float stackHeight = 0f;
	private float progress;

	private void Start()
	{
		moveAlongZ = !moveAlongZ;
	}

	void Update()
	{
		if (currentBlock != null)
		{
			MoveBlock();
		}
	}


	void MoveBlock()
	{
		Vector3 moveDirection = new Vector3();
		int axis = moveAlongZ ? 2 : 0;

		moveDirection[axis] = bound;

		Vector3 from = transform.position + moveDirection;
		Vector3 to = transform.position - moveDirection;

		progress += moveSpeed * Time.deltaTime * (moveForward ? 1 : -1);
		if (moveForward && progress >= 1)
		{
			moveForward = false;
		}
		else if (moveForward == false && progress <= 0)
		{
			moveForward = true;
		}
		progress = Mathf.Clamp01(progress);

		currentBlock.transform.position = Vector3.Lerp(from, to, progress);
	}

	public CubeCutter.CuttingAxis GetCuttingAxis()
	{
		return moveAlongZ ? CubeCutter.CuttingAxis.CutByZ : CubeCutter.CuttingAxis.CutByX;
	}
}