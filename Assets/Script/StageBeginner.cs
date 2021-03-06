﻿using UnityEngine;
using System.Collections;

public class StageBeginner : MonoBehaviour {
	public float speed;
	// Use this for initialization
	public Rigidbody rb;
	public GUIText timeText;
	public GUIText scoreText;
	public GUIText recordText;
	public UnityEngine.UI.Text btnLabel;
	public float startTime;
	public float elapsed;
	public int pickCount;
	public bool isAndroid; // Use for test in PC delete when publish to phone 
	public bool gameOver;
	public bool isStop;

	void Start () {
		loadRecord ();
		isStop = true;
		handler();
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
		if (!gameOver) {
			elapsed = Time.time - startTime;
			timeText.text = elapsed.ToString (("F1")) + " S";
		}
		if(Input.GetKeyDown (KeyCode.Escape)){
			Application.LoadLevel ("StageMenu");
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
		if (other.gameObject.tag == "PickUp") {
			other.gameObject.SetActive(false);
			pickCount++;
			setScoreText();
		}		
	}
	void setScoreText(){
		if (pickCount == 5) {
			scoreText.text = "You Win";
			gameOver = true;
			saveRecord(elapsed);
		} else {
			scoreText.text = "Picked: " + pickCount  + " / 5";
		}
	}
	void loadRecord(){
		if (PlayerPrefs.HasKey ("BeginnerRecord")) {
			float record = PlayerPrefs.GetFloat ("BeginnerRecord");
			recordText.text = "Record: " + record.ToString (("F1")) + " S";
		} else {
			recordText.text = "Record: None";
		}
	}
	void saveRecord(float time){
		if (PlayerPrefs.HasKey ("BeginnerRecord")) {
			float record = PlayerPrefs.GetFloat ("BeginnerRecord");
			if (time < record) {
				PlayerPrefs.SetFloat ("BeginnerRecord", time);
				recordText.text = "Record: " + time.ToString (("F1")) + " S";
			}
		} else {
			PlayerPrefs.SetFloat ("BeginnerRecord", time);
			recordText.text = "Record: " + time.ToString (("F1")) + " S";
		} 
	}
}
