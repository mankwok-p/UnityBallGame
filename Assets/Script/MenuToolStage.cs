using UnityEngine;
using System.Collections;

public class MenuToolStage : MonoBehaviour {

	public void startStageA(){
		Application.LoadLevel ("StageTool");
	}

	public void startStageB(){
		Application.LoadLevel ("StageToolB");
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
