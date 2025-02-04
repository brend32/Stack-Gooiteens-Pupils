using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public MeshRenderer Renderer;

    public Color BlockColor => Renderer.sharedMaterial.color;

    public void SetColor(Color color)
    {
        if (Renderer.sharedMaterial.color == color)
        {
            return;
        }

        Renderer.material.color = color;
    }
}
