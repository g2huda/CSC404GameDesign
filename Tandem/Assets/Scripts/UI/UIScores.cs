﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScores : MonoBehaviour {

    private Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (text.text != Scores.totalScore.ToString())
        {
            text.text = Scores.totalScore.ToString();
        }
	
	}
}
