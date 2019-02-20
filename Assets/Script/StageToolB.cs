using UnityEngine;
using System.Collections;

public class StageToolB : MonoBehaviour {
	public float speed;
	// Use this for initialization
	public GameObject player;
	public GameObject[] rotateObstacles;
	public Rigidbody rb;
	public GUIText timeText;
	public GUIText scoreText;
	public GUIText recordText;
	public UnityEngine.UI.Text btnLabel;
	public float startTime;
	public float elapsed;
	public float reversedStartTime;
	public int pickCount;
	public bool isAndroid; // Use for test in PC delete when publish to phone 
	public bool gameOver;
	public Vector3 respawnPos;
	public bool isStop;
	public bool virbation;
	public bool timeInc = false;
	public bool timeDec = false;
	public bool reverse = false;

	void Start () {
		loadRecord ();
		isStop = true;
		handler();
		respawnPos = player.transform.position;
		if (PlayerPrefs.HasKey ("FlagVirbate")) {
			string flag = PlayerPrefs.GetString ("FlagVirbate");
			if (flag.Equals ("ON")) {
				virbation = true;
			} else {
				virbation = false;
			}
		} else {
			virbation = true;
		}
		// Use for test in PC delete when publish to phone
		if (Application.platform == RuntimePlatform.Android) {
			isAndroid = true;
		} else {
			isAndroid = false;
		}
		gameOver = false;
		startTime = Time.time;
		timeText.text = startTime.ToString(("F1")) + " S";
		pickCount = 0;
		setScoreText();
	}
	
	// Update is called once per frame
	public void Update () {
		if (timeInc) {
			startTime -= 5.0f;
			timeInc = false;
		} else if (timeDec) {
			startTime += 5.0f;
			timeDec = false;
		}if (reverse && Time.time -reversedStartTime>5) {
			reverse =false;
		}
		if (!gameOver) {
			elapsed = Time.time - startTime;
			timeText.text = elapsed.ToString (("F1")) + " S";
		}
		if(Input.GetKeyDown (KeyCode.Escape)){
			Application.LoadLevel ("StageToolMenu");
		}
	}
	
	public void FixedUpdate(){
		Vector3 movement;
		if (isAndroid) {
			movement = new Vector3 (Input.acceleration.x, 0, Input.acceleration.y);
			if (reverse) {
				movement = new Vector3 (-Input.acceleration.x, 0, -Input.acceleration.y);
			}
		} else {
			float moveHorziontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			movement = new Vector3 (moveHorziontal, 0, moveVertical);
			if (reverse) {
				movement = new Vector3 (-moveHorziontal, 0, -moveVertical);
			}
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
		if (other.gameObject.tag == "PickUp") {
			other.gameObject.SetActive (false);
			pickCount++;
			setScoreText ();
			
		} else if (other.gameObject.tag == "Respawn") {
			other.gameObject.SetActive (false);
			float x = player.transform.position.x;
			float y = player.transform.position.y;
			float z = player.transform.position.z;
			respawnPos = new Vector3 (x, y, z);
		} else if (other.gameObject.tag == "SpeedUp") {
			other.gameObject.SetActive (false);
			speed = speed * 1.5f;
		} else if (other.gameObject.tag == "SpeedDown") {
			other.gameObject.SetActive (false);
			speed = speed / 1.5f;
		} else if (other.gameObject.tag == "Restart") {
			other.gameObject.SetActive (false);
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag ("Respawn");
			foreach (GameObject go in gos) {
				go.SetActive (false);
			}
		} else if (other.gameObject.tag == "TimeInc") {
			other.gameObject.SetActive (false);
			timeInc = true;
		} else if (other.gameObject.tag == "TimeDec") {
			other.gameObject.SetActive (false);
			timeDec = true;
		} else if (other.gameObject.tag == "Reverse") {
			other.gameObject.SetActive(false);
			reversedStartTime = Time.time;
			reverse = true;
		}

	}
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Obstacle" && !gameOver) {
			if (virbation) {
				Handheld.Vibrate ();
			}
			player.transform.position = respawnPos;
		}
	}
	void setScoreText(){
		if (pickCount == 5) {
			scoreText.text = "You Win";
			gameOver = true;
			saveRecord(elapsed);
		} else {
			scoreText.text = "Picked: " + pickCount + " / 5";
		}
	}

	void loadRecord(){
		if (PlayerPrefs.HasKey ("ToolsBRecord")) {
			float record = PlayerPrefs.GetFloat ("ToolsBRecord");
			recordText.text = "Record: " + record.ToString (("F1")) + " S";
		} else {
			recordText.text = "Record: None";
		}
	}
	void saveRecord(float time){
		if (PlayerPrefs.HasKey ("ToolsBRecord")) {
			float record = PlayerPrefs.GetFloat ("ToolsBRecord");
			if (time < record) {
				PlayerPrefs.SetFloat ("ToolsBRecord", time);
				recordText.text = "Record: " + time.ToString (("F1")) + " S";
			}
		} else {
			PlayerPrefs.SetFloat ("ToolsBRecord", time);
			recordText.text = "Record: " + time.ToString (("F1")) + " S";
		} 
	}
}

