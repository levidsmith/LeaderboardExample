using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void doDisplayScores() {
        SceneManager.LoadScene("DisplayScoreTest");


    }

    public void doSubmitScore() {
        SceneManager.LoadScene("SubmitScoreTest");

    }
}
