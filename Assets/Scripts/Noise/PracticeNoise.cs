using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class PracticeNoise : MonoBehaviour
{

    public int width = 256;
    public int height = 256;
    private RawImage image;
    private FastNoiseLite noise = new();

    private InputAction regenerate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        regenerate = InputSystem.actions.FindAction("Generate");

        
        Generate();
    }

    private void Update()
    {
        if(regenerate.WasPressedThisFrame())
        {
            Generate();
        }
    }

    private void Generate()
    {
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
        noise.SetFractalType(FastNoiseLite.FractalType.DomainWarpProgressive);
        noise.SetSeed(Random.Range(0, 10000));
        noise.SetFrequency(Random.Range(0f, 10f));

        image = GetComponent<RawImage>();

        Texture2D texture = new Texture2D(width, height);

        List<float> data = new();


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var noiseVal = noise.GetNoise(x, y);
                data.Append(noiseVal);

                Color flatColor;

                if(noiseVal <= -0.2)
                {
                    //clr = Random.Range(0.0f, 255.0f);
                    flatColor = Color.yellowNice;
                }
                else if(noiseVal > -0.2 && noiseVal < 0.09)
                {
                    flatColor = Color.forestGreen;
                }
                else
                {
                    flatColor = Color.blueViolet;
                }
                    texture.SetPixel(x, y, flatColor);
            }
        }

        texture.Apply();
        image.texture = texture;
           
    }
}
