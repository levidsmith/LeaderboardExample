using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitScoreTestMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addHighScoreTest() {
        HighScoreManager hsm = GameObject.FindObjectOfType<HighScoreManager>();
        hsm.addScoreValues("test", 42);
    }
}
