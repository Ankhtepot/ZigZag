using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {

    [SerializeField] public int value = 5;
    [SerializeField] Vector3 rotationOnUpdate;
    [SerializeField] float shrinkRatio;
    [Range(0.1f, 1f)] [SerializeField] float scaleChange = 0.001f;
    [SerializeField] bool shrink = false;

    //caches
    Transform Body;

	void Start () {
        Body = GetComponentInChildren<DiamondBodyPivot>().transform;
    }
	
	void Update () {
        Body.Rotate(rotationOnUpdate * Time.deltaTime);
        if(shrink) {
            scaleChange = shrinkRatio;
            transform.localScale -= new Vector3(scaleChange, scaleChange, scaleChange);
            if (transform.localScale.x < 0.02f) ImmidiateDestroy();
        }
	}

    public void ImmidiateDestroy() {
        Destroy(gameObject);
    }

    public void shinkDestroy() {
        shrink = true;
    }
}
