using System;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
	public GameObject blockPrefab;
	public float moveSpeed = 5f;
	public float bound = 3f;
	public GameObject currentBlock;
	public bool moveAlongZ = false;
	public bool moveForward = true;
	
	private float blockHeight = 0.5f;
	private float stackHeight = 0f;
	private float progress;

	private void Start()
	{
		currentBlock.transform.localScale = new Vector3(3f, blockHeight, 3f); 
		moveAlongZ = !moveAlongZ;
	}

	void Update()
	{
		if (currentBlock != null)
		{
			MoveBlock();
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			PlaceBlock();
		}
	}

	void SpawnBlock()
	{
		currentBlock = Instantiate(blockPrefab, new Vector3(-bound, stackHeight, 0), Quaternion.identity);
		currentBlock.transform.localScale = new Vector3(3f, blockHeight, 3f); 
		moveAlongZ = !moveAlongZ;
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

	void PlaceBlock()
	{
		if (currentBlock == null) return;
		stackHeight += blockHeight; 
		currentBlock = null;
		Invoke("SpawnBlock", 0.2f);
	}
}