using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] BallController ball;
    [SerializeField] Vector3 offset;
    [SerializeField] float lerpRate;

	void Start () {
        ball = FindObjectOfType<BallController>();
        offset = ball.transform.position - transform.position;
	}
	
	void Update () {
        if (!ball.gameOver) {
            Follow();
        }
	}

    private void Follow() {
        Vector3 position = transform.position;
        Vector3 targetPosition = ball.transform.position - offset;
        position = Vector3.Lerp(position,targetPosition,lerpRate * Time.deltaTime);
        transform.position = position;
    }
}
