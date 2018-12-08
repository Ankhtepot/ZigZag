using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePivot : MonoBehaviour {

    [SerializeField] Vector3 RotateVector;

	void Update () {
        transform.Rotate(RotateVector, 0.01f);
	}
}
