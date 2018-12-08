using Assets.Scripts.classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour {

    [SerializeField] float destroyDelay = 1f;

    public IEnumerator delayedDestroy() {
        yield return new WaitForSeconds(destroyDelay);
        immidiateDestroy();
    }

    public void immidiateDestroy() {
        Destroy(gameObject);
    }
}
