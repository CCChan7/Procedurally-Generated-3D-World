using UnityEngine;
using System.Collections;

public class Display : MonoBehaviour 
{

	public Renderer textureRender;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;

	public void DrawTexture(Texture2D texture) 
	{
		textureRender.sharedMaterial.mainTexture = texture; //applies texture generated to the main texture
		textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height); //applies texture height and width values to the scale
	}

	public void DrawMesh(MeshData meshData, Texture2D texture) 
	{
		meshFilter.sharedMesh = meshData.CreateMesh(); //applies mesh generated to the main mesh used
		meshRenderer.sharedMaterial.mainTexture = texture; //applies mesh texture
	}

}
