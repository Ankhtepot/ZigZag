using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour {

    public void ResetScore() {
        FindObjectOfType<GameController>().ResetScore();
    }
}
