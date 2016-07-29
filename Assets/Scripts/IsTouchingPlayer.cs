using UnityEngine;
using System.Collections;

public class IsTouchingPlayer : MonoBehaviour {

	private GameController gameController;

	void Start() {
		getGameController ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject != null && other.gameObject.name == "goldOne(Clone)") {
			gameController.addScoreForCollectGold (1);
			Destroy (other.gameObject);
		} else if (other.gameObject != null && other.gameObject.name == "goldTwo(Clone)") {
			gameController.addScoreForCollectGold (2);
			Destroy (other.gameObject);
		} else if (other.gameObject != null && other.gameObject.name == "goldThree(Clone)") {
			gameController.addScoreForCollectGold (3);
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
