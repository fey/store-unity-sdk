using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
	public Transform canvas;
	public List<GameObject> prefabs;

	public GameObject GetGameObject()
	{
		if (prefabs.Count == 0) {
			Debug.LogError("Halloween collection is empty!");
			return null;
		}
		int index = Random.Range(0, prefabs.Count);
		return Instantiate(prefabs[index], canvas);
	}
}
