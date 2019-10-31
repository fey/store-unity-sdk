using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
	Container container;

    void Start()
    {
		container = gameObject.GetComponent<Container>();
    }

    public GameObject GetItem()
	{
		GameObject go = container.GetGameObject();
		if(go != null) {
			go.AddComponent<Destroyer>();
			go.AddComponent<Movement>();
			go.AddComponent<Rotation>();
		}
		return go;
	}
}
