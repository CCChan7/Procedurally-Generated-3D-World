using UnityEngine;
using System.Collections;

public static class Noise
{

	public static float[,] GenerateNoise(int gridX, int gridY, int seed, float multiplier, int octaves, float persistance, float lacunarity, Vector2 offset)
	{
		float[,] noiseMap = new float[gridX,gridY]; //creates noisemap on a x and y grid

		System.Random prng = new System.Random(seed);
		Vector2[] octaveOffsets = new Vector2[octaves]; 
		for (int i = 0; i < octaves; i++)
		{
			float offsetX = prng.Next(-9999, 9999) + offset.x;
			float offsetY = prng.Next(-9999, 9999) + offset.y;
			octaveOffsets[i] = new Vector2 (offsetX, offsetY); //creates random octaves that will offset the noisemap
		}

		if (multiplier <= 0) //creates a multiplier for the noise map incase the number is not valid
		{
			multiplier = 0.01f;
		}

		float maximumHeight = float.MinValue;
		float minimumHeight = float.MaxValue;

		float halfX = gridX / 2f;
		float halfY = gridY / 2f;


		for (int y = 0; y < gridY; y++) 
		{
			for (int x = 0; x < gridX; x++)
			{
				//gives values incase they are empty
				float amplitude = 1; 
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++) 
				{
					float sampleX = (x-halfX) / multiplier * frequency + octaveOffsets[i].x;
					float sampleY = (y-halfY) / multiplier * frequency + octaveOffsets[i].y;

					float perlin = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
					noiseHeight += perlin * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity; //changes the amplitude to be added based on persistence of noise and lacunarity and frequency
				}

				if (noiseHeight > maximumHeight)
				{
					maximumHeight = noiseHeight;
				} 

				else if (noiseHeight < minimumHeight)
				{
					minimumHeight = noiseHeight;
				}

				noiseMap[x, y] = noiseHeight; //adds height to the noise map
			}
		}

		for (int y = 0; y < gridY; y++)
		{
			for (int x = 0; x < gridX; x++)
			{
				noiseMap[x, y] = Mathf.InverseLerp(minimumHeight, maximumHeight, noiseMap [x, y]); //lerp is the unity built in function for linear interpolation
				//will add inverse linear interpolation which will smooth out the numbers given by the noise map
			}
		}

		return noiseMap;
	}

}
