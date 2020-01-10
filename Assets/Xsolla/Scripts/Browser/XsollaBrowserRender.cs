using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class XsollaBrowserRender : MonoBehaviour
{
	public UnityEngine.UI.Image image;
	public SpriteRenderer spriteRenderer;

	private XsollaBrowser browser;

    void Awake()
    {
		browser = gameObject.GetComponent<XsollaBrowser>();
		if(browser) {
			browser.RedrawComplete += Browser_RedrawComplete;
		}
    }

	private void Browser_RedrawComplete(byte[] obj, Size size)
	{
		Sprite sprite = GetSprite(obj, size);

		if(image != null) {
			image.overrideSprite = sprite;
		}
		if(spriteRenderer != null) {
			spriteRenderer.sprite = sprite;
		}
	}

	private Sprite GetSprite(byte[] obj, Size size)
	{
		Texture2D texture = new Texture2D(size.Width, size.Height, TextureFormat.RGBA32, false);
		texture.LoadImage(obj);
		texture.Apply();

		Rect rect = new Rect(Vector2.zero, new Vector2(size.Width, size.Height));
		return Sprite.Create(texture, rect, Vector2.zero);
	}
}
