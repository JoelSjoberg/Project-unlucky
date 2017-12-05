using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthScript : MonoBehaviour {

    EnemyBehaviour boss;
	// Use this for initialization
	void Start () {
        boss = FindObjectOfType<EnemyBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {

        // hp   maxHp
        // __ =  __
        // x     maxWidth
        GetComponent<RectTransform>().sizeDelta = new Vector2(boss.health * 235 / boss.maxHp, 15);
	}
}
