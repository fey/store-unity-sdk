using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelection : MonoBehaviour
{
	Image image;
	float opacity = 1.0F;

	private void Awake()
	{
		Transform t1 = transform.Find("Item.Image");
		Transform t2 = t1?.Find("Image");
		image = t2?.GetComponent<Image>();
	}

	private void ChangeOpacity(float value)
	{
		Color c = image.color;
		c.a = value;
		image.color = c;
	}

	private void OnDestroy()
	{
		ChangeOpacity(1.0F);
	}

    void Update()
    {
		opacity -= Time.deltaTime * 2;
		if (opacity < 0.3F)
			opacity = 1.0F;
		ChangeOpacity(opacity);
	}
}
