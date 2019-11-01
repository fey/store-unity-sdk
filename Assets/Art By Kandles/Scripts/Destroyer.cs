using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Destroyer : MonoBehaviour, IPointerClickHandler
{
	void Start()
    {
		StartCoroutine(DestroyCoroutine(10.0F));
    }

	IEnumerator DestroyCoroutine(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Destroy(gameObject, 0.001F);
		StopAllCoroutines();
	}

	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		StartCoroutine(DestroyCoroutine(0.001F));
	}
}
