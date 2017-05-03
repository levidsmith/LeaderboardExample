/**
 * 2017 Levi D. Smith
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour {

    public bool scoreAdded;

    void Start() {
    }

    void Update() {

    }

    private string privateKey = "randomlygeneratedkey";
    private string TopScoresURL = "http://example.com/scores/TopScores.php";
    private int iGameID = 0;

    private string AddScoreURL = "http://example.com/scores/AddScore.php?";

    
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

        strName = GameObject.Find("TextNameValue").GetComponent<Text>().text;
        iScore = Int32.Parse(GameObject.Find("TextScoreValue").GetComponent<Text>().text);
        iGame = Int32.Parse(GameObject.Find("TextGameIDValue").GetComponent<Text>().text);

        StartCoroutine(AddScore(strName, iScore, iGame));
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
        List<ScoreEntry> topScores = new List<ScoreEntry>();

        StartCoroutine(GetTopScores(topScores));

        Debug.Log("topScores size: " + topScores.Count);

        return topScores;

    }

    IEnumerator GetTopScores(List<ScoreEntry> listScores) {
        int i;
        WWW GetScoresAttempt = new WWW(TopScoresURL + "?game=" + iGameID);
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
    }
}