using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Vector1 = System.Single;

public struct Transform1D
{
    public Vector1 position;
    public Vector1 scale;
    public Transform1D(Vector1 scale, Vector1 position)
    {
        this.scale = scale;
        this.position = position;
    }
}

public enum CuttingResult
{
    Split,
    Perfect,
    GameOver
}

public class CubeCutter : MonoBehaviour
{
    public enum CuttingAxis : int
    {
        CutByX = 0,
        CutByZ = 2,
    }
    
    public UnityEvent OnCut;
    public bool isCanCut = true;
    public float SmallThreshold = 0.015f;

    public Block FallingBlock;

    private void CalculateCut(
            Transform1D victim, 
            Transform1D cutter,
            out Transform1D inBlock,
            out Transform1D outBlock)
	{
        Transform1D left_transform = new();
        Transform1D right_transform = new();

        Vector1 right_cutter_border = (cutter.position + (cutter.scale / 2f));
        Vector1 left_cutter_border = (cutter.position - (cutter.scale / 2f));
        
        Vector1 right_victim_border = (victim.position + (victim.scale / 2f));
        Vector1 left_victim_border = (victim.position - (victim.scale / 2f));

        if(cutter.position < victim.position)
        {
            // +
            Vector1 right_distance = Mathf.Abs(right_victim_border - right_cutter_border);
            right_transform.scale = right_distance;
            right_transform.position = (right_cutter_border + (right_distance / 2f));

            left_transform.scale = (victim.scale - right_transform.scale);
            left_transform.position = (right_cutter_border - (left_transform.scale / 2f));

            outBlock = right_transform;
            inBlock = left_transform;
        }
        else
        {
            // -
            Vector1 left_distance = Mathf.Abs(left_victim_border - left_cutter_border);
            left_transform.scale = left_distance;
            left_transform.position = (left_cutter_border - (left_distance / 2f));

            right_transform.scale = (victim.scale - left_transform.scale);
            right_transform.position = (left_cutter_border + (right_transform.scale / 2f));

            outBlock = left_transform;
            inBlock = right_transform;
        }
	}
    /*--------------------------------------------------------*/
    public CuttingResult Cut(CuttingAxis axis, Block victimBlock, Transform cutter, out float newSize)
    {
        int targetAxis = (int)axis;

        Transform victim = victimBlock.transform;
        Vector3 position = victim.position;
        Vector3 scale = victim.localScale;
        
        Transform1D victimTransform = new Transform1D(victim.localScale[targetAxis], victim.position[targetAxis]);
        Transform1D cutterTransform = new Transform1D(cutter.localScale[targetAxis], cutter.position[targetAxis]);
        
        CalculateCut(victimTransform, cutterTransform, out Transform1D inBlock, out Transform1D outBlock);
        newSize = inBlock.scale;

        if (inBlock.scale < SmallThreshold)
        {
            // Game over
            Destroy(victim.gameObject);
            
            Block fallingBlock = Instantiate(FallingBlock);
            fallingBlock.SetColor(victimBlock.BlockColor);
            
            position[targetAxis] = outBlock.position;
            scale[targetAxis] = outBlock.scale;
            fallingBlock.transform.position = position;
            fallingBlock.transform.localScale = scale;
            
            return CuttingResult.GameOver;
        }

        if (outBlock.scale < SmallThreshold)
        {
            // Perfect
            position = new Vector3(cutter.position.x, position.y, cutter.position.z);
            victim.position = position;

            return CuttingResult.Perfect;
        }
        
        // Split
        {
            Block fallingBlock = Instantiate(FallingBlock);
            fallingBlock.SetColor(victimBlock.BlockColor);
                
            position[targetAxis] = outBlock.position;
            scale[targetAxis] = outBlock.scale;
            fallingBlock.transform.position = position;
            fallingBlock.transform.localScale = scale;
        }
        
        position[targetAxis] = inBlock.position;
        scale[targetAxis] = inBlock.scale;
        victim.position = position;
        victim.localScale = scale;

        return CuttingResult.Split;
    }
    /*--------------------------------------------------------*/
    public void ToggleAbilityToCut()
    {
        isCanCut = !isCanCut;
    }
}
