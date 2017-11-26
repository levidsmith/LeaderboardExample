using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardMenu : MonoBehaviour {


    float fCountdown = 1f;
    List<ScoreEntry> topScores;
    bool keepChecking;
    bool doDisplayTime = false;

    // Use this for initialization
    void Start() {
        topScores = new List<ScoreEntry>();
        keepChecking = true;
        displayTopScores();

    }

    // Update is called once per frame
    void Update() {

        if (fCountdown > 0f && keepChecking) {
            fCountdown -= Time.deltaTime;

            if (fCountdown <= 0f) {
                fCountdown = 1f;
                HighScoreManager hsm = GameObject.FindObjectOfType<HighScoreManager>();
                if (hsm.isFinishedLoading) {
                    updateDisplay();
                    keepChecking = false;

                }

            }
        }

    }

    private void displayTopScores() {
        //        HighScoreManager hsm = GameObject.Find("HighScoreManager").GetComponent<HighScoreManager>();
        HighScoreManager hsm = GameObject.FindObjectOfType<HighScoreManager>();
        topScores = hsm.getTopScores();
        Debug.Log("displayTopScores");

    }

    private void updateDisplay() {
        int i;

        Text textNames = GameObject.Find("TextNames").GetComponent<Text>();
        Text textScores = GameObject.Find("TextScores").GetComponent<Text>();

        textNames.text = "";
        textScores.text = "";

        if (topScores.Count == 0) {
            textNames.text = "No scores";

        }

        for (i = 0; i < topScores.Count; i++) {

            textNames.text += topScores[i].strName + "\n";
            if (doDisplayTime) {
                textScores.text += string.Format("{0:0.00}", ((float)topScores[i].iScore) / 100f) + "\n";
            } else {
                textScores.text += string.Format("{0}", ((float)topScores[i].iScore)) + "\n";
            }

        }
    }

    public void doContinue() {
        SceneManager.LoadScene("title");
        
    }
}
