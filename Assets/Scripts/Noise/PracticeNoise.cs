using UnityEngine;
using UnityEngine.UI;

public class PracticeNoise : MonoBehaviour
{

    public int width = 256;
    public int height = 256;
    private RawImage image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<RawImage>();

        Texture2D texture = new Texture2D(width, height);

        FastNoiseLite noise = new();
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);   


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float clr = Random.Range(0.0f, 1.0f);
                //noise.GetNoise(x, y);
            }
        }
    }
}
