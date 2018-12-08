using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour {

    [SerializeField] float rotationMultiplier;

	void Update () {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationMultiplier);
    }
}
