using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GenerateNoise : MonoBehaviour
{
    [SerializeField] private GetValueFromDropdownNoise dropdownNoiseType;
    [SerializeField] private Canvas targetCanvas;
    
    public int width = 256;
    public int height = 256;
    [SerializeField] private RawImage image;
    private FastNoiseLite noise = new();
    private FastNoiseLite noise2 = new();

    private InputAction regenerate;
    private InputAction openMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        regenerate = InputSystem.actions.FindAction("Generate");
        openMenu = InputSystem.actions.FindAction("OpenMenu");
    }

    private void Update()
    {
        if(regenerate.WasPressedThisFrame())
        {
            Generate();
        }

        if(openMenu.WasPressedThisFrame())
        {
            targetCanvas.enabled = true;
        }
    }

    private void SetNoiseType(string type)
    {
        switch (type) 
        {
            case "OpenSimplexS":
                noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
                break;
            case "OpenSimplex2S":
                noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
                break;
            case "Cellular":
                noise.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
                break;
            case "Perlin":
                noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
                break;
            case "ValueCubic":
                noise.SetNoiseType(FastNoiseLite.NoiseType.ValueCubic);
                break;
            case "Value":
                noise.SetNoiseType(FastNoiseLite.NoiseType.Value);
                break;
        }
    }

    public void Generate()
    {
        targetCanvas.enabled = false;
        var randSeed = Random.Range(1000, 10000);
        var randFrequency = Random.Range(0.0009f, 0.09f);

        SetNoiseType(dropdownNoiseType.GetDropdownValue());

        noise.SetFractalType(FastNoiseLite.FractalType.FBm);

        noise.SetDomainWarpType(FastNoiseLite.DomainWarpType.OpenSimplex2Reduced);
        noise.SetDomainWarpAmp(Random.Range(1.0f, 300.0f));

        noise.SetSeed(randSeed);

        noise.SetFrequency(randFrequency);

        noise2.SetSeed(randSeed);
        noise2.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        noise2.SetFrequency(randFrequency);

        Texture2D texture = new (width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var noiseVal = noise.GetNoise(x, y);
                var noiseVal2 = noise2.GetNoise(x, y);

                Color flatColor;

                if(noiseVal > -0.15f && noiseVal < 0.05f)
                {
                    flatColor = Color.darkBlue;
                }
                else if(noiseVal < -0.15f)
                {
                    flatColor = Color.lightGoldenRodYellow;
                }
                else
                {
                    flatColor = Color.forestGreen;
                }

                if(flatColor == Color.darkBlue)
                {
                    if (noiseVal2 < 0.14f)
                    {
                        flatColor = Color.Lerp(flatColor, Color.black, 0.2f);
                    }
                    else
                    {
                        flatColor = Color.Lerp(flatColor, Color.white, 0.2f);
                    }
                }

                texture.SetPixel(x, y, flatColor);
            }
        }

        texture.Apply();
        image.texture = texture;
    }
}
