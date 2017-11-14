using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public Camera cam;
	public GameObject ball;
	public GameObject bomb;
	public float timeLeft;
	public Text timerText;
	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject splashScreen;
	public GameObject startButton;
	public HatController hatController;

	private float maxWidth;
	private bool playing;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		playing = false;

		// Get maxWidth
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = ball.GetComponent<Renderer> ().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;

		UpdateText ();
	}

	void FixedUpdate() {
		if (playing) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = 0;
			}
			UpdateText ();
		}
	}

	public void StartGame() {
		splashScreen.SetActive (false);
		startButton.SetActive (false);
		playing = true;
		hatController.ToggleControl (true);
		StartCoroutine (Spawn ());
	}

	IEnumerator Spawn() {
		yield return new WaitForSeconds(2.0f);
		while (timeLeft > 0) {
			Vector3 spawnPosition = new Vector3 (
				                        Random.Range (-maxWidth, maxWidth), 
				                        transform.position.y, 
				                        0.0f
			                        );
			Quaternion spawnRotation = Quaternion.identity;
			GameObject objectToDrop = (Random.Range (1, 11) < 5) ? bomb : ball;
			Instantiate (objectToDrop, spawnPosition, spawnRotation);
			yield return new WaitForSeconds(Random.Range (1.0f, 2.0f));
		}

		yield return new WaitForSeconds (2.0f);
		gameOverText.SetActive (true);
		restartButton.SetActive (true);
	}

	void UpdateText() {
		timerText.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft);
	}
}
