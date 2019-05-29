using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IsTouchingPlayer : MonoBehaviour {

	private GameController gameController;
	private CameraShaker cameraShaker;
	void Start() {
		getGameController ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject != null && other.gameObject.name == "gold(Clone)") {
			cameraShaker.setShake(0.04f, 0.1f);
			gameController.addScoreForCollectGold();
			Destroy (other.gameObject);			
		}
	}

	private void getGameController() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController>();
		if (gameController == null)
			Debug.Log ("Can not find 'Game Controller' script");
		if (cameraShaker == null) {
            GameObject cameraGM = GameObject.FindWithTag("MainCamera");
			cameraShaker = cameraGM.GetComponent<CameraShaker>();
        }
	}
}
