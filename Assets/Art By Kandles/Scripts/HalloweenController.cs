using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalloweenController : MonoBehaviour
{
	public GameObject canvas;
	Generator generator;
	RectTransform canvasRect;

	public List<GameObject> additionals;

	void Start()
    {
		generator = gameObject.GetComponent<Generator>();
		canvasRect = canvas.GetComponent<RectTransform>();
		additionals.ForEach(a => a.SetActive(false));
		StartCoroutine(HalloweenCoroutine());
    }

    IEnumerator HalloweenCoroutine()
	{
		yield return new WaitForSeconds(3.0F);
		additionals.ForEach(a => a.SetActive(true));
		while (true) {
			yield return new WaitForSeconds(Random.Range(4.0f, 7.0f));

			GameObject go = generator.GetItem();
			Vector2 position = GetItemPosition();
			go.GetComponent<Movement>().
				SetSpeed(GetSpeed()).
				SetPosition(position).
				MoveTo(GetTargetPosition(position));
		}
	}

	float GetSpeed()
	{
		return Random.Range(200.0F, 400.0F);
	}

	Vector2 GetItemPosition()
	{
		return GetNormalizedDirection() * canvasRect.sizeDelta.magnitude / 2.0F;
	}

	Vector2 GetTargetPosition(Vector2 from)
	{
		Vector2 direction = from * -1;
		direction.Normalize();
		direction = direction.Rotate(Random.Range(-30.0F, 30.0F));
		return direction * canvasRect.sizeDelta.magnitude * 10.0F;
	}

	Vector2 GetNormalizedDirection()
	{
		bool sign = Random.Range(0, 100) > 50;
		float x = Random.Range(canvasRect.offsetMin.x, canvasRect.offsetMax.x) * (sign ? 1 : (-1));
		sign = Random.Range(0, 100) > 50;
		float y = Random.Range(canvasRect.offsetMin.y, canvasRect.offsetMax.y) * (sign ? 1 : (-1));
		return new Vector2(x, y).normalized;
	}
}
