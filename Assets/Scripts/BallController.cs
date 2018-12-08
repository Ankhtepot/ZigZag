using Assets.Scripts.classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    [SerializeField] float moveSpeed;
    [SerializeField] float fallSpeed = -5;
    [SerializeField] float rollSpeed; //3
    [SerializeField] public bool hasStarted = false;
    [SerializeField] public bool gameOver = false;
    [SerializeField] float resetDelay = 2f;
    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 startPosition;
    [SerializeField] ObjectSpawner spawner;
    [SerializeField] ParticleSystem diamondCollision;
    [SerializeField] ParticleSystem dustTrail;

    //caches
    Rigidbody RB;
    GameController GC;

    private void Awake() {
        RB = GetComponent<Rigidbody>();
    }

    void Start() {
        startPosition = transform.position;
        GC = FindObjectOfType<GameController>();
    }

    void Update() {
        MovementOnClick();
        FallDetection();
        MoveShredder();
    }

    void FixedUpdate() {

        //Vector3 moveDelta = new Vector3(Input.GetAxis("Horizontal") * rollSpeed * Time.deltaTime, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, 0);
        //transform.Translate(moveDelta, Space.World);
        //Vector3 rotationAxis = Vector3.Cross(moveDelta.normalized, Vector3.forward);
        //transform.RotateAround(transform.position, rotationAxis, Mathf.Sin(moveDelta.magnitude * r * 2 * Mathf.PI) * Mathf.Rad2Deg);

        if(hasStarted)transform.RotateAround(transform.position, RB.velocity.z == 0 ? Vector3.back : Vector3.right, moveSpeed * rollSpeed/*Mathf.Sin(RB.velocity.magnitude * r * 2 * Mathf.PI) * Mathf.Rad2Deg*/);
    }

    private void MoveShredder() {
        GameObject shredder = GameObject.Find(gameobjects.SHREDDER);
        shredder.transform.position = new Vector3(gameObject.transform.position.x, shredder.transform.position.y, transform.position.z);
    }

    private void FallDetection() {
        if (!Physics.Raycast(transform.position, Vector3.down, 1f)) {
            gameOver = true;
            RB.velocity = new Vector3(0, fallSpeed, 0);
        }
    }

    private void MovementOnClick() {
        if (Input.GetMouseButtonDown(0) && !gameOver) {
            if (!hasStarted) {
                GC.SwitchToInGameCanvas();
                hasStarted = true;
                RB.velocity = new Vector3(moveSpeed, 0, 0);
            } else if (hasStarted) {
                switchDirection();
            }
        }
    }

    void switchDirection() {
        ParticleSystem DSInstance = Instantiate(dustTrail, transform.position, transform.rotation);
        DSInstance.Play();
        if (RB.velocity.z > 0) {
            RB.velocity = new Vector3(moveSpeed, 0, 0);
        } else if (RB.velocity.x > 0) {
            RB.velocity = new Vector3(0, 0, moveSpeed);
        }
    }

    IEnumerator DelayedReset() {
        yield return new WaitForSeconds(resetDelay);
        RB.velocity = new Vector3(0, 0, 0);
        transform.position = startPosition;
        gameOver = false;
        hasStarted = false;
        GC.resetGameSession();
        GC.SwitchToMenuCanvas();
        spawner.Reset();
        StartCoroutine(spawner.SpawnPlatforms());
    }

    private void OnTriggerEnter(Collider other) {
        string otherTag = other.tag;
        //print("Ball triggered with " + otherTag);
        if (otherTag == tags.LOSECOLLIDER) {
            StartCoroutine(DelayedReset());
        }
        if(otherTag == tags.DIAMOND) {
            if (diamondCollision) Instantiate(diamondCollision, other.transform.position, Quaternion.identity);
            Diamond DI = other.GetComponent<Diamond>();
            GC.AddScore(DI.value);
            DI.shinkDestroy();
        }
    }
}
