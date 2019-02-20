using UnityEngine;
using System.Collections;

public class MenuStage : MonoBehaviour {
	public void startBeginnerGame(){
		Application.LoadLevel ("StageBeginner");
	}
	public void startHardGame(){
		Application.LoadLevel ("StageHardMenu");
	}
	public void startChanllegeGame(){
		Application.LoadLevel ("StageChallenge");
	}
	public void startToolGame(){
		Application.LoadLevel("StageToolMenu");
	}
	public void back(){
		Application.LoadLevel ("MainMenu");
	}
	public void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("MainMenu");
		}
	}
}

