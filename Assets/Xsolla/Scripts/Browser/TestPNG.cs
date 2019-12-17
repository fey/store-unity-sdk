using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class TestPNG : MonoBehaviour
{
	public Image image;
	public Sprite source;

	private Vector2 pivot;
	private Rect rect;
	private Size size;
	// Start is called before the first frame update
	void Start()
    {
		string filePath = Application.persistentDataPath + "/";
		Debug.Log("persistentDataPath: " + filePath);

		if(source != null) {
			byte[] data = GetImageData(source);
			SaveImageData(data, filePath + "TestPNG.png");
		}
	}

	private byte[] GetImageData(Sprite image)
	{
		rect = image.textureRect;
		pivot = image.pivot;
		size = new Size(image.texture.width, image.texture.height);

		return image.texture.GetRawTextureData();
	}

	private void SaveImageData(byte[] data, string path)
	{
		Texture2D texture = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGBA32, false);
		texture.LoadRawTextureData(data);	
		texture.Apply();

		image.overrideSprite = Sprite.Create(texture, rect, pivot);
	}
}
