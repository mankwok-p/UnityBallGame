using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour {
	public UnityEngine.UI.Text virbateLabel;

	public 	void Start () {
		if (PlayerPrefs.HasKey ("FlagVirbate")) {
			string flag = PlayerPrefs.GetString ("FlagVirbate");
			if (flag.Equals ("OFF")) {
				virbateLabel.text = "Virbation: Off";
			} else if (flag.Equals ("ON")) {
				virbateLabel.text = "Virbation: On";
			}
		} else {
			PlayerPrefs.SetString("FlagVirbate","ON");
			virbateLabel.text = "Virbation: On";
		}
	}

	public void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel (0);
		}
	}
	public void removeRecords(){
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetString("FlagVirbate","ON");
		virbateLabel.text = "Virbation: On";
	}
	public void back(){
		Application.LoadLevel (0);
	}
	public void setVirbate(){
		if (PlayerPrefs.HasKey ("FlagVirbate")) {
			string flag = PlayerPrefs.GetString ("FlagVirbate");
			if(flag.Equals("OFF")){
				PlayerPrefs.SetString("FlagVirbate","ON");
				virbateLabel.text = "Virbation: On";
			}else if(flag.Equals("ON")){
				PlayerPrefs.SetString("FlagVirbate","OFF");	
				virbateLabel.text = "Virbation: Off";

			}
		} 
	}
}
