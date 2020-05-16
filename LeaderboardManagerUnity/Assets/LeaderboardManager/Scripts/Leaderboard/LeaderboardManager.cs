//2020 Levi D. Smith
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour {

    int iGameID = Constants.GAME_ID;
    string strSecretKey = Constants.LEADERBOARD_KEY;
    bool useTimeFormat = false;


    public Canvas canvasLeaderboardName;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    public void doSubmitScore(string strName, int iScore) {
        Debug.Log("doSubmitScore: " + iScore);
        string strHash = Md5Sum(strName + iScore.ToString() + iGameID.ToString() + strSecretKey);
        string strURL = "https://" + Constants.LEADERBOARD_DOMAIN + "/scores/AddScore.php?game=" + iGameID + "&name=" + UnityWebRequest.EscapeURL(strName) +
            "&score=" + iScore + "&hash=" + strHash;

        Debug.Log("URL submit: " + strURL);
        StartCoroutine(submitScore(strURL));
    }

    IEnumerator submitScore(string strURL) {
        Debug.Log("url: " + strURL);
        UnityWebRequest www = UnityWebRequest.Get(strURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log("Error: " + www.error);
        } else {
            Debug.Log("Submit results" + www.downloadHandler.text);
            byte[] results = www.downloadHandler.data;
        }


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

    public void getTopScores(Text textNames, Text textScores) {
        string strURL = "https://" + Constants.LEADERBOARD_DOMAIN + "/scores/TopScores.php?game=" + iGameID;
        StartCoroutine(getScores(strURL, textNames, textScores));

    }
    IEnumerator getScores(string strURL, Text textNames, Text textScores) {
//        bool useTimeFormat = true;
        int iMaxEntries = 10;

        UnityWebRequest www = UnityWebRequest.Get(strURL);
        yield return www.SendWebRequest();

        //text.text = "Top Scores\n";
        textNames.text = "";
        textScores.text = "";

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log("Error: " + www.error);
        } else {
            Debug.Log("Get Scores:\n" + www.downloadHandler.text);
            byte[] results = www.downloadHandler.data;

            string[] strScores = www.downloadHandler.text.Split("\n".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);

            int i;
            int iMax;
            iMax = strScores.Length;
            if (iMax > iMaxEntries) {
                iMax = iMaxEntries;
            }

            for (i = 0; i < iMax; i++) {
                string[] strEntry = strScores[i].Split("\t".ToCharArray(), System.StringSplitOptions.None);
                if (strEntry.Length >= 1) {
                    textNames.text += strEntry[0] + "\n";
                }

                if (strEntry.Length >= 2) {
                    if (useTimeFormat) {
                        int iTime = Int32.Parse(strEntry[1]);
                        int iMins = iTime / 6000;
                        int iSecs = (iTime / 100) % 60;
                        int iHundredths = iTime % 100;
                        //                    textScores.text += iMins + ":" + iSecs + "." + iHundredths + "\n";
                        textScores.text += string.Format("{0:0}:{1:00}.{2:00}\n", iMins, iSecs, iHundredths);
                    } else {
                        textScores.text += strEntry[1] + "\n";
                    }
                }
            }

        }


    }

}