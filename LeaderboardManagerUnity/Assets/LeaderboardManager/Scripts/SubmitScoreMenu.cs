//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SubmitScoreMenu : MonoBehaviour {

    int iScore;
    public Text TextName;
    public Text TextScore;

    void Start() {
        getRandomScore();
    }


    void getRandomScore() {
        iScore = UnityEngine.Random.Range(1, 100);
        TextScore.text = string.Format("{0}", iScore);
    }

    public void doSubmitScore() {
        LeaderboardManager leaderboardmanager = GameObject.FindObjectOfType<LeaderboardManager>();
        leaderboardmanager.doSubmitScore(TextName.text, Int32.Parse(TextScore.text));
        SceneManager.LoadScene("LeaderboardManager/Scenes/DisplayScoreTest");

    }
}
