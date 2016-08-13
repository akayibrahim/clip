using UnityEngine;
using System.Collections;

public class IsTouchingLine : MonoBehaviour {

	private GameController gameController;

	void Start() {
		getGameController ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject != null && other.gameObject.name == "Clip") {
			other.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			gameController.GameOver ();
		}
	}

	void onGUI () {
		GUI.Box (new Rect ((Screen.width / 2) - 90, Screen.height / 2, 210, 70), "Do u want watch ads for continue?");
		GUI.Button (new Rect ((Screen.width / 2) - 60, (Screen.height / 2) + 30, 70, 30), "Yes");
		GUI.Button (new Rect ((Screen.width / 2) + 20, (Screen.height / 2) + 30, 70, 30), "No");
 	}

	private void getGameController() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController>();
		if (gameController == null)
			Debug.Log ("Can not find 'Game Controller' script");
	}
}
