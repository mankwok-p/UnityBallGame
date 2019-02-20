using UnityEngine;
using System.Collections;

public class MenuMain: MonoBehaviour {
	public void selectStage(){
		Application.LoadLevel("StageMenu");
	}
	public void viewRecords(){
		Application.LoadLevel("Records");
	}
	public void goOptions(){
		Application.LoadLevel("Option");
	}
	public void exitGame(){
		Application.Quit();
	}
}
