using Assets.Scripts.classes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] int score = 0;
    [SerializeField] int highscore;

    //Caches
    [SerializeField] TextMeshProUGUI FinalHighscoreText;
    [SerializeField] TextMeshProUGUI FinalScoreText;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] Text HighscoreText;
    [SerializeField] Text ScoreText;
    [SerializeField] Text TapToStartText;
    [SerializeField] GameObject TitlePanel;
    [SerializeField] bool isNewHighscore = false;
    [SerializeField] bool isAfterGameStarted = false;

    public int Score {
        get { return score; }
        set {
            score = value;
            UpdateText(ScoreText, score.ToString());
        }
    }

    public int Highscore {
        get { return highscore; }
        set { ProcessHighscore(value); }
    }

    private void Awake() {
        if (FindObjectsOfType<GameController>().Length > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        AssignCaches();
        SwitchToMenuCanvas();
    }

    private void AssignCaches() {
        /// Uncomment line bellow to reset playerPref for Highscore for release
        /// and deploy again commented out again
        //PlayerPrefs.SetInt(prefs.HIGHSCORE, 0);
        FinalHighscoreText = GameObject.Find(gameobjects.FINALHIGHSCORETEXT).GetComponent<TextMeshProUGUI>();
        FinalScoreText = GameObject.Find(gameobjects.FINALSCORETEXT).GetComponent<TextMeshProUGUI>();
        GameOverPanel = GameObject.Find(gameobjects.GAMEOVERPANEL);
        HighscoreText = GameObject.Find(gameobjects.HIGHSCORETEXT).GetComponent<Text>();
        ScoreText = GameObject.Find(gameobjects.SCORETEXT).GetComponent<Text>();
        TapToStartText = GameObject.Find(gameobjects.TAPTOSTARTTEXT).GetComponent<Text>();
        TitlePanel = GameObject.Find(gameobjects.TITLEPANEL);
        if (PlayerPrefs.HasKey(prefs.HIGHSCORE)) Highscore = PlayerPrefs.GetInt(prefs.HIGHSCORE);
        else PlayerPrefs.SetInt(prefs.HIGHSCORE, 0);
    }

    public void AddScore(int value) {
        Score += value;
    }

    private void UpdateText(Text text, String value) {
        if (text) text.text = value;
        else if (text == null) print("GC/UpdateText: Text missing");
    }

    private void UpdateTMP(TextMeshProUGUI TMP, string value) {
        TMP.text = value;
    }

    private void ProcessHighscore(int value) {
        if (value > highscore) {
            isNewHighscore = true;
            highscore = value;
            PlayerPrefs.SetInt(prefs.HIGHSCORE, Highscore);
            UpdateText(HighscoreText, highscore.ToString());
            UpdateTMP(FinalHighscoreText, highscore.ToString());
        } else isNewHighscore = false;
    }

    public void resetGameSession() {
        UpdateTMP(FinalScoreText, Score.ToString());
        Highscore = Score;
    }

    public void SwitchToInGameCanvas() {
        isAfterGameStarted = true;
        ScoreText.gameObject.GetComponent<Animator>().SetBool(triggers.SWIPEIN, true);
        TapToStartText.GetComponent<Animator>().SetBool(triggers.SWIPEDOWN, true);
        TitlePanel.GetComponent<Animator>().SetBool(triggers.SWIPEUP, true);
        GameOverPanel.GetComponent<Animator>().SetBool(triggers.SWIPEDOWN, false);
    }

    public void SwitchToMenuCanvas() {
        TapToStartText.GetComponent<Animator>().SetBool(triggers.SWIPEDOWN, false);
        ScoreText.gameObject.GetComponent<Animator>().SetBool(triggers.SWIPEIN, false);
        if (!isAfterGameStarted) {
            TitlePanel.GetComponent<Animator>().SetBool(triggers.SWIPEUP, false);
        } else if (isAfterGameStarted) {
            GameOverPanel.GetComponent<Animator>().SetBool(triggers.SWIPEDOWN, true);
            GameOverPanel.GetComponent<Animator>().SetBool(triggers.NEWHIGHSCORE, isNewHighscore ? true : false);
        }
    }

    public void ResetScore() {
        Score = 0;
    }
}
