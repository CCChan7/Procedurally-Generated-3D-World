using UnityEngine;
using System.Collections;

public static class TextureGenerator 
{

	public static Texture2D TextureColour(Color[] colourMap, int xAxis, int yAxis) 
	{
		Texture2D texture = new Texture2D(xAxis, yAxis);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.SetPixels(colourMap);
		texture.Apply(); //applies colour map to the texture
		return texture;
	}


	public static Texture2D TextureHeight(float[,] height) 
	{
		int xAxis = height.GetLength(0);
		int yAxis = height.GetLength(1);

		Color[] colourMap = new Color[xAxis * yAxis]; //applies colour to colourmap
		for (int y = 0; y < yAxis; y++) 
		{
			for (int x = 0; x < xAxis; x++) 
			{
				colourMap[y * xAxis + x] = Color.Lerp(Color.black, Color.white, height [x, y]); //applies linear interpolation to colour map
			}
		}

		return TextureColour(colourMap, xAxis, yAxis);
	}

}
