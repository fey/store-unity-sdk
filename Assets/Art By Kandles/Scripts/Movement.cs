using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	RectTransform rect;
	Vector2 targetPosition;
	float speed = 3.0F;

	private void Awake()
	{
		rect = gameObject.GetComponent<RectTransform>();
	}

	public Movement SetSpeed(float value)
	{
		speed = value;
		return this;
	}

	public Movement SetPosition(Vector2 position)
	{
		rect.anchoredPosition = targetPosition = position;
		return this;
	}

	public Movement MoveTo(Vector2 position)
	{
		targetPosition = position;
		return this;
	}

    void Update()
    {
		Vector2 direction = targetPosition - rect.anchoredPosition;
		if (direction.magnitude > 1.0F) {
			rect.anchoredPosition += direction.normalized * Time.deltaTime * speed;
		}
    }
}
