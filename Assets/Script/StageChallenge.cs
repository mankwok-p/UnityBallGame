using UnityEngine;
using System.Collections;

public class StageChallenge : MonoBehaviour {
	public float speed;
	// Use this for initialization
	public GameObject player;
	public GameObject[] rotateObstacles;
	public Rigidbody rb;
	public GUIText timeText;
	public GUIText scoreText;
	public GUIText recordText;
	public UnityEngine.UI.Text btnLabel;
	public float countDown;
	public float elapsed;
	public int pickCount;
	public bool isAndroid; // Use for test in PC delete when publish to phone 
	public bool gameOver;
	public bool isStop;
	public float startTime;

	
	void Start () {
		loadRecord ();
		isStop = true;
		countDown = 60.0f;
		elapsed = 60.0f;
		startTime = Time.time;
		handler();
		// Use for test in PC delete when publish to phone
		if (Application.platform == RuntimePlatform.Android) {
			isAndroid = true;
		} else {
			isAndroid = false;
		}
		gameOver = false;
		timeText.text = countDown + " S";
		pickCount = 0;
		setScoreText();
	}
	
	// Update is called once per frame
	public void Update () {
		if (!gameOver) {

			elapsed = countDown - startTime;
			startTime = Time.time; 
			timeText.text = elapsed.ToString (("F1")) + " S";
			if (elapsed <= 0.0f) {
				gameOver = true;
				scoreText.text = "You Lose";
				
			}
		}
		if(Input.GetKeyDown (KeyCode.Escape)){
			Application.LoadLevel ("StageMenu");
			startTime = 0.0f;
			elapsed = 60.0f;
		}

	}
	
	public void FixedUpdate(){
		Vector3 movement;
		if (isAndroid) {
			movement = new Vector3 (Input.acceleration.x, 0, Input.acceleration.y);
		} else {
			float moveHorziontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			movement = new Vector3 (moveHorziontal, 0, moveVertical);
		}
		rb.AddForce(movement*speed*Time.deltaTime);
	}
	
	public void handler(){
		if (isStop) {
			Time.timeScale = 1;
			isStop = false;
			btnLabel.text = "Pause";
		} else {
			Time.timeScale = 0;
			isStop = true;
			btnLabel.text = "Resume";
		}
	}
	void OnTriggerEnter(Collider other){
		if (!gameOver) {
			if (other.gameObject.tag == "PickUp") {
				other.gameObject.SetActive (false);
				pickCount++;
				setScoreText ();
			} else if (other.gameObject.tag == "BlackKey") {
				other.gameObject.SetActive (false);
				GameObject[] gos;
				gos = GameObject.FindGameObjectsWithTag ("BlackKey");
				foreach (GameObject go in gos) {
					go.SetActive (false);
				}
			} else if (other.gameObject.tag == "GreenKey") {
				other.gameObject.SetActive (false);
				GameObject[] gos;
				gos = GameObject.FindGameObjectsWithTag ("GreenKey");
				foreach (GameObject go in gos) {
					go.SetActive (false);
				}
			} else if (other.gameObject.tag == "PurpleKey") {
				other.gameObject.SetActive (false);
				GameObject[] gos;
				gos = GameObject.FindGameObjectsWithTag ("PurpleKey");
				foreach (GameObject go in gos) {
					go.SetActive (false);
				}
			} else if (other.gameObject.tag == "BlueKey") {
				other.gameObject.SetActive (false);
				GameObject[] gos;
				gos = GameObject.FindGameObjectsWithTag ("BlueKey");
				foreach (GameObject go in gos) {
					go.SetActive (false);
				}
			}
		}
	}
	void setScoreText(){
		if (pickCount == 1) {
			scoreText.text = "You Win";
			gameOver = true;
			saveRecord(elapsed);
		} else {
			scoreText.text = "Picked: " + pickCount + " / 1";
		}
	}
	void loadRecord(){
		if (PlayerPrefs.HasKey ("ChallengeRecord")) {
			float record = PlayerPrefs.GetFloat ("ChallengeRecord");
			recordText.text = "Record: " + record.ToString (("F1")) + " S";
		} else {
			recordText.text = "Record: None";
		}
	}
	void saveRecord(float time){
		if (PlayerPrefs.HasKey ("ChallengeRecord")) {
			float record = PlayerPrefs.GetFloat ("ChallengeRecord");
			if (time > record) {
				PlayerPrefs.SetFloat ("ChallengeRecord", time);
				recordText.text = "Record: " + time.ToString (("F1")) + " S";
			}
		} else {
			PlayerPrefs.SetFloat ("ChallengeRecord", time);
			recordText.text = "Record: " + time.ToString (("F1")) + " S";
		} 
	}
}
