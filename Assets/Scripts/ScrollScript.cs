using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour {

	public GameObject player;
	public float scrollSpeed = 0.5f;
	
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2(Time.time * scrollSpeed, 0);		 
		 GetComponent<Renderer>().material.mainTextureOffset = offset;

		float xPos = Mathf.Lerp (transform.position.x, player.transform.position.x, 30f * Time.deltaTime);
		transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
	}
}
