using UnityEngine;
using System.Collections;

public class ViewRecords : MonoBehaviour {

	public UnityEngine.UI.Text recordText;
	// Use this for initialization
	void Start () {
		string beginnerRecord;
		string hardRecord;
		string hardBRecord;
		string toolRecord;
		string toolBRecord;
		string challengeRecord;
		//string 
		if (PlayerPrefs.HasKey ("BeginnerRecord")) {
			beginnerRecord = "Beginner: " + PlayerPrefs.GetFloat ("BeginnerRecord").ToString (("F1")) + " S";
		} else {
			beginnerRecord = "Beginner: None";
		}
		if (PlayerPrefs.HasKey ("HardRecord")) {
			hardRecord = "Hard Stage 1: " + PlayerPrefs.GetFloat ("HardRecord").ToString (("F1")) + " S";
		} else {
			hardRecord = "Hard Stage 1: None";
		}
		if (PlayerPrefs.HasKey ("HardBRecord")) {
			hardBRecord = "Hard Stage 2: " + PlayerPrefs.GetFloat ("HardBRecord").ToString (("F1")) + " S";
		} else {
			hardBRecord = "Hard Stage 2: None";
		}
		if (PlayerPrefs.HasKey ("ToolsRecord")) {
			toolRecord = "Tool Stage 1: " + PlayerPrefs.GetFloat ("ToolsRecord").ToString (("F1")) + " S";
		} else {
			toolRecord = "Tool Stage 1: None";
		}
		if (PlayerPrefs.HasKey ("ToolsBRecord")) {
			toolBRecord = "Tool Stage 2: " + PlayerPrefs.GetFloat ("ToolsBRecord").ToString (("F1")) + " S";
		} else {
			toolBRecord = "Tool Stage 2: None";
		}
		if (PlayerPrefs.HasKey ("ChallengeRecord")) {
			challengeRecord = "Challenge: " + PlayerPrefs.GetFloat ("ChallengeRecord").ToString (("F1")) + " S";
		} else {
			challengeRecord = "Challenge: None";
		}
		recordText.text = beginnerRecord + "\n" + hardRecord + "\n" + hardBRecord + "\n" + toolRecord + "\n" + toolBRecord + "\n" + challengeRecord;
	}

	public void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("MainMenu");
		}
	}
	public void back(){
		Application.LoadLevel ("MainMenu");
	}
}
