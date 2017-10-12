using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	//Creates CaveGenerators

	public Transform prefab;
	public Transform caveGenerator;

	// Use this for initialization
	void Start () {
		//cubes
		for (int i = 0; i < 10; i++) {
			Instantiate(prefab, new Vector3(i * 2f, 0, 27), Quaternion.identity);
		}

		//access script before initiation
		GameObject gameObject = caveGenerator.GetComponent<CellularAutomata> ().gameObject;
		CellularAutomata script = gameObject.GetComponent<CellularAutomata> ();
		int caveWidth = script.width;
		int caveHeight = script.height;
		//caves
		for (int x = 0; x < 5; x++) {
			for (int y = 0; y < 2; y++) {
				Instantiate (caveGenerator, new Vector3 (x * caveWidth, 0, y * caveHeight), Quaternion.identity);
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
