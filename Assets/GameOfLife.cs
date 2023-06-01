using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{

    public ComputeShader shader;
    public RenderTexture texture;

    private int width = 10000;
    private int height = 10000;
    
    // Start is called before the first frame update
    void Start()
    {
        int kernelHandle = shader.FindKernel("Randomise");
        
        texture = new RenderTexture(width, height, 24);
        texture.enableRandomWrite = true;
        texture.Create();
        
        shader.SetTexture(kernelHandle, "Result", texture);
        shader.Dispatch(kernelHandle, width / 8, height / 8, 1);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        int kernelHandle = shader.FindKernel("NextGeneration");
        
        if (texture == null)
        {
            texture = new RenderTexture(width, height, 24);
            texture.enableRandomWrite = true;
            texture.Create();
        }
        
        shader.SetTexture(kernelHandle, "Result", texture);
        //var pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //shader.SetFloats("mouse_pos", pos.x, pos.y, pos.z);
        //shader.SetFloat("width", width);
        //shader.SetFloat("height", height);
        shader.Dispatch(kernelHandle, width / 8, height / 8, 1);
        Graphics.Blit(texture, dest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
