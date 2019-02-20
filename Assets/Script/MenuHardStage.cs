using UnityEngine;
using System.Collections;

public class MenuHardStage : MonoBehaviour {

	public void startStageA(){
		Application.LoadLevel ("StageHard");
	}
	
	public void startStageB(){
		Application.LoadLevel ("StageHardB");
	}
	public void back(){
		Application.LoadLevel ("StageMenu");
	}
	public void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("StageMenu");
		}
	}
}
