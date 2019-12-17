using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotate : MonoBehaviour
{
    void Update()
    {
		transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 10.0F, Space.Self);
    }
}
