using UnityEngine;
using System.Collections;

public class DestroyTouchGold : MonoBehaviour {

	private GameController gameController;

	void Start() {
		getGameController ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "line") {
			Destroy(gameObject);
			if (gameObject.name == "ice(Clone)")
				gameController.addDeserveToIce ();
			if (gameObject.name == "sun(Clone)")
				gameController.addDeserveToSun ();
			gameController.instantiateGold ();
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
