using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubmitScoreMenu : MonoBehaviour {

    float fScoreValue;
    public Text textScoreValue;
    public Text textInputName;

    // Use this for initialization
    void Start () {

        //do whatever you need to do to load the score into fScoreValue
        fScoreValue = 9999;

        textScoreValue.text = string.Format("{0}", fScoreValue);

        //Format for times
        /*
        int iMinutes = Mathf.FloorToInt(fTotalTimeSeconds) / 60;
        float fSeconds = fTotalTimeSeconds - (iMinutes * 60);
        textScoreValue.text = string.Format("{0}:{1:0.00}", iMinutes, fSeconds);
        */


    }

    // Update is called once per frame
    void Update () {

        HighScoreManager hsm = GameObject.FindObjectOfType<HighScoreManager>();
        if (hsm.scoreAdded) {
            //            SceneManager.LoadScene("title");
            SceneManager.LoadScene("title");

        }


    }


    public void doSubmitScore() {
        string strSubmitName = textInputName.text.Trim();

        int iSubmitScore = Mathf.FloorToInt(fScoreValue);


        //Use line below to multiply scores with two decimal places
        //        int iSubmitScore = Mathf.FloorToInt(fScoreValue * 100);


        if (strSubmitName != "") {
            if (iSubmitScore > 0) {
                HighScoreManager hsm = GameObject.FindObjectOfType<HighScoreManager>();
                hsm.addScoreValues(strSubmitName, iSubmitScore);
            } else {
                SceneManager.LoadScene("title");
            }
        }

    }

}
