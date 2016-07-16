using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	void Update () {
		float xPos = Mathf.Lerp (transform.position.x, player.transform.position.x, 30f * Time.deltaTime);
		transform.position = new Vector3(xPos, transform.position.y, -10);
	}
}
