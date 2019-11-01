using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
	int index;
	public Transform canvas;
	public List<GameObject> prefabs;

	public GameObject GetGameObject()
	{
		if (prefabs.Count == 0) {
			Debug.LogError("Halloween collection is empty!");
			return null;
		}
		GameObject go = Instantiate(prefabs[index++], canvas);
		if (index >= prefabs.Count)
			index = 0;
		return go;
	}
}
