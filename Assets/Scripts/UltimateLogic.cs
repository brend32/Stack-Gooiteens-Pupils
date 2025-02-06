using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateLogic : MonoBehaviour
{
    public ScoreDisplay ScoreDisplay;
    public BlockSpawner Spawner;
    public CubeCutter Cutter;
    public BlockMover Mover;
    public PerfectSliceSpawner PerfectSliceSpawner;

    private Transform _previousBlock;
    private Block _currentBlock;
    
    public void Start()
    {
        _previousBlock = Spawner.SpawnBlock().transform;
        MoveSpawnPointUp();
        _currentBlock = Spawner.SpawnBlock();

        Mover.currentBlock = _currentBlock.transform;
    }

    public void Update()
    {
        // Потрібен кубик
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact(); 
        }
    }

    public void Interact()
    {
        CubeCutter.CuttingAxis axis = Mover.GetCuttingAxis();
        CuttingResult result = Cutter.Cut(axis, _currentBlock, _previousBlock, out float newSize);

        switch (result)
        {
            case CuttingResult.Split:
            case CuttingResult.Perfect:
                Vector3 previousBlockScale = _previousBlock.localScale;
                
                _previousBlock = _currentBlock.transform;
                Mover.transform.position = _previousBlock.position;
                MoveSpawnPointUp();
                _currentBlock = Spawner.SpawnBlock();
                Mover.currentBlock = _currentBlock.transform;
                previousBlockScale[(int)axis] = newSize;
                _currentBlock.transform.localScale = previousBlockScale;

                Mover.moveAlongZ ^= true;
                ScoreDisplay.AddScore(1);

                if (result == CuttingResult.Perfect)
                {
                    PerfectSliceSpawner.PlayAnimation(_currentBlock.transform.position);
                }
                break;
            
            case CuttingResult.GameOver:
                // Enable window
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void MoveSpawnPointUp()
    {
        Vector3 position = Mover.transform.position;
        position.y += Spawner.GetBlockHeight();
        Mover.transform.position = position;
    }
}
