using Assets.Scripts.classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedScore : MonoBehaviour {

    [SerializeField] float addScoreFreqency = 0.5f;
    [SerializeField] int timedScore = 1;

    [Header("Caches")]
    [SerializeField] BallController Ball;
    [SerializeField] GameController GC;
    [SerializeField] bool scoreTimerActive = false;
    [SerializeField] bool scoreTimerOnCD = false;

    private void Start() {
        Ball = FindObjectOfType<BallController>();
        GC = FindObjectOfType<GameController>();
        PlayerPrefs.SetInt(prefs.TIMEDSCOREVALUE, timedScore);
    }

    void Update () {
        scoreTimerActive = !Ball.gameOver && Ball.hasStarted;
        if (scoreTimerActive && !scoreTimerOnCD) StartCoroutine(addTimedScore());
	}

    IEnumerator addTimedScore() {
        scoreTimerOnCD = true;
        yield return new WaitForSeconds(addScoreFreqency);
        GC.AddScore(PlayerPrefs.GetInt(prefs.TIMEDSCOREVALUE));
        scoreTimerOnCD = false;
    }
}
