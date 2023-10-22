using UnityEngine;
using System.Collections;

public static class MeshGenerator
{

	public static MeshData GenerateMesh(float[,] height, float heightIncrease, AnimationCurve heightCurve) 
	{
		int xAxis = height.GetLength(0);
		int yAxis = height.GetLength(1);
		float corner1 = (xAxis - 1) / -2f; //finds top left of the X axis
		float corner2 = (yAxis - 1) / 2f; //finds top left of 2D grid Y axis (Z axis)
		int meshIncrease = 1;
		int vertexAmount = (xAxis - 1) / meshIncrease + 1;
		MeshData meshData = new MeshData(vertexAmount, vertexAmount);
		int vertex = 0;

		for (int y = 0; y < yAxis; y += meshIncrease) 
		{
			for (int x = 0; x < xAxis; x += meshIncrease)
			{
				meshData.vertices[vertex] = new Vector3(corner1 + x, heightCurve.Evaluate(height [x, y]) * heightIncrease, corner2 - y); //applies yAxis map created from noise to mesh
				meshData.uvs[vertex] = new Vector2(x / (float)xAxis, y / (float)yAxis); //adds index to the mesh UV

				if (x < xAxis - 1 && y < yAxis - 1) 
				{
					//creates triangles that make up the mesh through the yAxis and xAxis until the whole grid is covered
					meshData.AddTriangle(vertex, vertex + vertexAmount + 1, vertex + vertexAmount); 
					meshData.AddTriangle(vertex + vertexAmount + 1, vertex, vertex + 1);
				}
				vertex++;
			}
		}
		return meshData;
	}
}

public class MeshData 
{
	public Vector3[] vertices;
	public int[] triangles;
	public Vector2[] uvs;

	int triangleValue;

	public MeshData(int meshWidth, int meshHeight) 
	{
		vertices = new Vector3[meshWidth * meshHeight]; //finds vertices of the mesh
		uvs = new Vector2[meshWidth * meshHeight]; //finds 2D UV 
		triangles = new int[(meshWidth-1)*(meshHeight-1)*6]; //finds triangles
	}

	public void AddTriangle(int x, int y, int z) //creates triangles to be added to mesh
	{
		triangles[triangleValue] = x;
		triangles[triangleValue + 1] = y;
		triangles[triangleValue + 2] = z;
		triangleValue += 3;
	}

	public Mesh CreateMesh()  
	{
		Mesh mesh = new Mesh();
		mesh.vertices = vertices; //adds vertices to mesh
		mesh.triangles = triangles; //adds triangles to mesh
		mesh.uv = uvs; //adds uv to mesh
		mesh.RecalculateNormals();
		return mesh;
	}

}