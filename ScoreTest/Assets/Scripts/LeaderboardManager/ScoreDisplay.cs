/**
 * 2017 Levi D. Smith
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {


    float fCountdown = 1f;
    List<ScoreEntry> topScores;
    bool keepChecking;

    void Start() {
        topScores = new List<ScoreEntry>();
        keepChecking = true;
        displayTopScores();

    }

    void Update() {

        if (fCountdown > 0f && keepChecking) {
            fCountdown -= Time.deltaTime;

            if (fCountdown <= 0f) {
                fCountdown = 1f;
                Debug.Log("topScores.Count: " + topScores.Count);
                if (topScores.Count > 0) {
                    updateDisplay();
                    keepChecking = false;
                }

            }
        }

    }

    private void displayTopScores() {
        LeaderboardManager hsm = GameObject.Find("LeaderboardManager").GetComponent<LeaderboardManager>();
        topScores = hsm.getTopScores();

    }

    private void updateDisplay() {
        int i;

        Text textNames = GameObject.Find("TextNames").GetComponent<Text>();
        Text textScores = GameObject.Find("TextScores").GetComponent<Text>();

        textNames.text = "";
        textScores.text = "";

        for (i = 0; i < topScores.Count; i++) {

            textNames.text += topScores[i].strName + "\n";
            textScores.text += topScores[i].iScore.ToString() + "\n";

        }
    }



}
