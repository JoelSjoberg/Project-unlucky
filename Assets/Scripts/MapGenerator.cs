using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	//Creates CaveGenerators

	public Transform prefab;
	public Transform caveGenerator;
	public Transform doorParent;
	public int xNumberRooms = 5;
	public int yNumberRooms = 3;
	float caveWidth;
	float caveHeight;

	// Use this for initialization
	void Start () {
		//cubes
		for (int i = 0; i < 10; i++) {
			Instantiate(prefab, new Vector3(i * 2f, 0, 27), Quaternion.identity);
		}

		spawnRooms ();
		spawnDoors ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void spawnRooms(){
		//access script before initiation
		GameObject gameObject = caveGenerator.GetComponent<CellularAutomata> ().gameObject;
		CellularAutomata script = gameObject.GetComponent<CellularAutomata> ();
		caveWidth = script.width + 5f;
		caveHeight = script.height + 5f;
		//caves
		for (int x = 0; x < xNumberRooms; x++) {
			for (int y = 0; y < yNumberRooms; y++) {
				Instantiate (caveGenerator, new Vector3 (x * caveWidth, 0, y * caveHeight), Quaternion.identity);
			}

		}
	}

	void spawnDoors(){
		for (int x = 0; x < xNumberRooms; x++) {
			for (int y = 0; y < yNumberRooms; y++) {
				Instantiate (doorParent, new Vector3 (x * 80, 2 , y * 60), Quaternion.identity);
			}

		}
	}
}
