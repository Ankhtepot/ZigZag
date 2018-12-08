using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField] Vector3 lastPosition;
    [SerializeField] Vector3 origPosition;
    [SerializeField] float size;
    [SerializeField] Platform platformPrefab;
    [SerializeField] GameObject diamonds;
    [SerializeField] Vector3 diamondSpawnOffset;
    [SerializeField] int numberOfSpawns = 15;
    [SerializeField] float timeBetweenSpawns = 0.15f;

    public Vector3 LastPosition {
        get { return lastPosition;}
        set { lastPosition = value;}
    }

    void Start() {
        origPosition = new Vector3(8, -0.945f,14);
        LastPosition = origPosition;
        size = platformPrefab.transform.localScale.x;
        StartCoroutine(SpawnPlatforms());
    }

    public void SpawnOnePlatform() {
        SpawnObjects();
    }

    public IEnumerator SpawnPlatforms() {
        resetLastPositionToOrigin();
        for (int i = 0; i < numberOfSpawns; i++) {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnObjects();
        }
    }

    void Update() {
        
    }

    void SpawnObjects() {
        Instantiate(platformPrefab, LastPosition, Quaternion.identity);
        if(Random.Range(0,4) < 1) {
            Instantiate(diamonds, LastPosition + diamondSpawnOffset, Quaternion.identity);
        }
        Vector3 position = LastPosition;
        int rnd = Random.Range(0, 2);
        if (rnd == 0) {
            position.x += size;
        } else if (rnd == 1) {
            position.z += size;
        }
        LastPosition = position;
    }

    void gravitizeObjects() {
        foreach (Platform PL in FindObjectsOfType<Platform>()) {
            PL.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void destroyObjects() {
        foreach (Platform PL in FindObjectsOfType<Platform>()) {
            PL.immidiateDestroy();
        }
        foreach(Diamond DI in FindObjectsOfType<Diamond>()) {
            DI.shinkDestroy();
        }
        resetLastPositionToOrigin();
    }

    public void Reset() {
        gravitizeObjects();
        destroyObjects();
    }

    public void resetLastPositionToOrigin() {
        LastPosition = origPosition;
    }
}
