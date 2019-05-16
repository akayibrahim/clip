using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IsTouchingPlayer : MonoBehaviour {

	private GameController gameController;

	void Start() {
		getGameController ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject != null && other.gameObject.name == "gold(Clone)") {
			gameController.addScoreForCollectGold ();
			Destroy (other.gameObject);			
		}
	}

	private void getGameController() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController>();
		if (gameController == null)
			Debug.Log ("Can not find 'Game Controller' script");
	}
}
