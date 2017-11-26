using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {

    //CONFIGURATION VALUES
    private string privateKey = "****************";
    private string TopScoresURL = "http://**********.com/scores/TopScores.php";
    private string AddScoreURL = "http://**********.com/scores/AddScore.php?";
    private int iGameID = -1;
    private string strOrder = "ASC";
    //END CONFIGURATION VALUES

    public bool scoreAdded;
    public bool isFinishedLoading;

    void Start() {
    }

    void Update() {

    }

    private string Md5Sum(string strToEncrypt) {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++) {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }


    void Error() {
        Debug.Log("Connection error.");
    }

    public void addScore() {
        string strName = "";
        int iScore = -1;
        int iGame = -1;

        strName = GameObject.Find("TextInputName").GetComponent<Text>().text;
        iScore = Mathf.FloorToInt(float.Parse(GameObject.Find("TextScoreValue").GetComponent<Text>().text) * 100);
        iGame = iGameID;

        if (iScore > 0) {
            StartCoroutine(AddScore(strName, iScore, iGame));
        } else {
            scoreAdded = true;
        }
    }

    public void addScoreValues(string strName, int iScore) {
        if (iScore > 0) {
            StartCoroutine(AddScore(strName, iScore, iGameID));
        } else {
            scoreAdded = true;
        }

    }

    IEnumerator AddScore(string strName, int iScore, int iGame) {
        string hash = Md5Sum(strName + iScore.ToString() + iGame.ToString() + privateKey);
        Debug.Log("Hash: " + hash);
        scoreAdded = false;

        WWW ScorePost = new WWW(AddScoreURL + "name=" + WWW.EscapeURL(strName) + "&score=" + iScore + "&game=" + iGame + "&hash=" + hash); //Post our score
        yield return ScorePost;

        if (ScorePost.error == null) {
            print("Added the score");
        } else {
            Error();
        }
        scoreAdded = true;
    }

    public List<ScoreEntry> getTopScores() {
        isFinishedLoading = false;
        List<ScoreEntry> topScores = new List<ScoreEntry>();

        StartCoroutine(GetTopScores(topScores));

        Debug.Log("topScores size: " + topScores.Count);

        return topScores;

    }

    IEnumerator GetTopScores(List<ScoreEntry> listScores) {
        int i;

        

        WWW GetScoresAttempt = new WWW(TopScoresURL + "?game=" + iGameID + "&order_by=" + strOrder);
        yield return GetScoresAttempt;

        if (GetScoresAttempt.error != null) {
            Error();
        } else {
            //Collect up all our data
            string[] textlist = GetScoresAttempt.text.Split(new string[] { "\n", "\t" }, System.StringSplitOptions.RemoveEmptyEntries);

            //Split it into two smaller arrays
            string[] strNames = new string[Mathf.FloorToInt(textlist.Length / 2)];
            string[] strScores = new string[strNames.Length];
            for (i = 0; i < textlist.Length; i++) {
                if (i % 2 == 0) {
                    strNames[Mathf.FloorToInt(i / 2)] = textlist[i];
                } else {
                    strScores[Mathf.FloorToInt(i / 2)] = textlist[i];
                }
            }

            for (i = 0; i < strNames.Length; i++) {
                listScores.Add(new ScoreEntry(strNames[i], Convert.ToInt32(strScores[i])));
            }

        }

        isFinishedLoading = true;
    }
}
