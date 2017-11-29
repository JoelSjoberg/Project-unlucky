using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {

    public static int level;
    public static bool touchingPortal;

    Text levelText;

	// Use this for initialization
	void Start () {
        levelText = GetComponent<Text>();
        level = 1;
        levelText.text = "Level: " + level;
	}
	
	// Update is called once per frame
	void Update () {
        levelText.text = "Level: " + level;
	}
}
