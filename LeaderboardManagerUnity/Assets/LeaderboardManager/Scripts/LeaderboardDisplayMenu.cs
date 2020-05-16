//2020 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardDisplayMenu : MonoBehaviour {

    public Text textNames;
    public Text textScores;
    void Start() {
        loadScores();
        
    }

    void loadScores() {
        LeaderboardManager leaderboardmanager = GameObject.FindObjectOfType<LeaderboardManager>();
        leaderboardmanager.getTopScores(textNames, textScores);

    }

    public void doLoadTitle() {
        SceneManager.LoadScene("LeaderboardManager/Scenes/title");

    }

}
