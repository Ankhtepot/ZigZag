using Assets.Scripts.classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour {

    [SerializeField] float fallDelay = 0.3f;
    [SerializeField] Rigidbody parentRB;

    void Start() {
        parentRB = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == tags.BALL) {
            FindObjectOfType<ObjectSpawner>().SpawnOnePlatform();
            StartCoroutine(FallDown());
        }
    }

    IEnumerator FallDown() {
        yield return new WaitForSeconds(fallDelay);
        parentRB.isKinematic = false;
        parentRB.useGravity = true;
        StartCoroutine(GetComponentInParent<Platform>().delayedDestroy());
    }
}
