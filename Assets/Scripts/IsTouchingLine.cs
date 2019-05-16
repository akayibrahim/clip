using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class IsTouchingLine : MonoBehaviour {

	private GameController gameController;

	void Start() {
		getGameController ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject != null && other.gameObject.name == "Clip") {
			other.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			Invoke("loadEnd", 1.0f);	
		}
	}

    public void loadEnd(){
		SceneManager.LoadScene("end", LoadSceneMode.Single);			
	}

	private void getGameController() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController>();
		if (gameController == null)
			Debug.Log ("Can not find 'Game Controller' script");
	}
}
