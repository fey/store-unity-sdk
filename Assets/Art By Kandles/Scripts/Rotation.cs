using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
	RectTransform rect;

	private void Awake()
	{
		rect = gameObject.GetComponent<RectTransform>();
	}

	void Update()
    {
		rect.Rotate(Vector3.forward, Time.deltaTime * 60.0F);
    }
}
