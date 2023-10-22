using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour
{

	public enum DrawMode{NoiseMap, ColourMap, Mesh}; //different draw modes for different maps to be shown
	public DrawMode drawMode;
	
	const int mapSize = 200;

	[Range(0,50)]
	public float noiseScale;

	[Range(0,30)]
	public int octaves;

	[Range(0,1)]
	public float persistance;

	[Range(0,3)]
	public float lacunarity;

	public int seed;
	public Vector2 offset;

	public GameObject[] treePrefabs;
    public float treeNoiseScale = 0.5f;
    public float treeDensity = 0.2f;

	public float meshHeight;
	public AnimationCurve meshHeightCurve;

	public bool autoUpdate;

	public TerrainType[] regions; //determines the type of terrain into seperate regions (like rocks or grass or sand)

	public void generateGrid() 
	{
		float[,] noiseMap = Noise.GenerateNoise(mapSize, mapSize, seed, noiseScale, octaves, persistance, lacunarity, offset); //applies all noise effects to noisemap

		Color[] colourMap = new Color[mapSize * mapSize];
		for (int y = 0; y < mapSize; y++)
		 {
			for (int x = 0; x < mapSize; x++) 
			{
				float currentHeight = noiseMap[x, y];
				for (int i = 0; i < regions.Length; i++) 
				{
					if (currentHeight <= regions[i].height) 
					{
						colourMap[y * mapSize + x] = regions[i].colour; //changes colour based on the region implemented
						break;
					}
				}
			}
		}

		Display display = FindObjectOfType<Display>();
		if (drawMode == DrawMode.NoiseMap) //draws noise map
		{
			display.DrawTexture(TextureGenerator.TextureHeight(noiseMap));
		} 
		else if (drawMode == DrawMode.ColourMap)  //draws colour map
		{
			display.DrawTexture(TextureGenerator.TextureColour(colourMap, mapSize, mapSize));
		} 
		else if (drawMode == DrawMode.Mesh) //draws mesh with height
		{
			display.DrawMesh(MeshGenerator.GenerateMesh(noiseMap, meshHeight, meshHeightCurve), TextureGenerator.TextureColour(colourMap, mapSize, mapSize));
		}


		// for(int y = 0; y < mapSize; y++) 
        // {
        //     for(int x = 0; x < mapSize; x++) 
        //     {
        //         float currentHeight = meshHeight;
		// 		float v = Random.Range(0f, treeDensity);
        //         if(noiseMap[x,y] < v) 
        //         {
        //             GameObject prefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
        //             GameObject tree = Instantiate(prefab, transform);
        //             tree.transform.position = new Vector3(x, currentHeight, y);
        //             tree.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
        //             tree.transform.localScale = Vector3.one * Random.Range(.2f, .8f);
        //         }
        //     }
            
        // }
	}

	void OnValidate() //checks if values are valid
	{
		if (lacunarity < 1) 
		{
			lacunarity = 1;
		}
		if (octaves < 0) 
		{
			octaves = 0;
		}
	}

	public void generateTrees()
        {
            float[,] treeNoiseMap = Noise.GenerateNoise(mapSize, mapSize, seed, noiseScale, octaves, persistance, lacunarity, offset);
            //MeshGenerator Mesh = MeshGenerator(treeNoiseMap,2,3);

           // Mesh(noiseMap) = treeNoiseMap;
        
        }
}

[System.Serializable]
public struct TerrainType 
{
	public string name;
	public float height;
	public Color colour;
}
