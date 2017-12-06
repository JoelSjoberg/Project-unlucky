using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {


	public void StartGame(string firstLevel){
		SceneManager.LoadScene (firstLevel);
		Debug.Log ("Start game pressed");
	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void Menu(string menuLevel){
		SceneManager.LoadScene (menuLevel);
		Debug.Log ("Menu button pressed");
	}

	public void Buy(){
		Debug.Log ("Buy pressed");
	}

	public void Leave(){
		Debug.Log ("Leave pressed");
	}
}


