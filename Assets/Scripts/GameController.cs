using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	
	public GameObject scoreGO;
	public GameObject player;
	public Text scoreText;
	private Text bestScoreText;
	public GameObject goldGO;
	public GameObject pauseGO;
	public GameObject levelText;
	public GameObject level;
	private float moveSpeed = 0.3f;
	public float upDownSpeed = 2f;
	float oldGoldX;

	private bool pressingArrowDown;
	private bool pressingArrowUp;
	private bool gameOver = false;
	private bool isStart;
	private int score = 0;
	private int scoreCount = 0;
	private int highScore = 0;
	private string highScorePrefsText = "HighScore";
	private string scorePrefsText = "Score";
	private int levelOfGame = 1;
	private string levelPrefsText = "Level";
	private DrawLine drawline;
	void Start () {
		isStart = true;
		highScore = PlayerPrefs.GetInt(highScorePrefsText);
		//PlayerPrefs.SetInt(levelPrefsText, 1);//remove
		if (!PlayerPrefs.HasKey(levelPrefsText)) {
			PlayerPrefs.SetInt(levelPrefsText, 1);
		}
		PlayerPrefs.SetInt(scorePrefsText, scoreCount);
		addGoldRandomly ();
	}

	void Update() {
		if (isStart) {
			player.transform.position += Vector3.right * (moveSpeed + levelOfGame) * Time.deltaTime;
			addScore ();

			if (Input.GetKey (KeyCode.UpArrow) || pressingArrowUp)
				movePlayerUp ();
			if (Input.GetKey(KeyCode.DownArrow) || pressingArrowDown)
				movePlayerDown ();
			moveByFinger ();
		}

		if(scoreCount > highScore) {
			highScore = scoreCount;
			PlayerPrefs.SetInt (highScorePrefsText, highScore);
		}
		levelOfGame = PlayerPrefs.GetInt(levelPrefsText);
		level.GetComponent<Text> ().text = levelOfGame.ToString();
		
	}

	public void addScore() {
		scoreText.text = scoreCount.ToString() ;
		PlayerPrefs.SetInt(scorePrefsText, scoreCount);
		// ((score++) % 50 == 0 ? (scoreCount++).ToString() : scoreCount.ToString()) ;
	}

	public void addScoreForCollectGold() {
		scoreCount += 1;
		if ( scoreCount % 30 == 0 ) {
			levelOfGame++;
			PlayerPrefs.SetInt(levelPrefsText, levelOfGame);
			GameObject gameControllerObject = GameObject.FindWithTag ("DrawLine");
			if (gameControllerObject != null) {
				drawline = gameControllerObject.GetComponent<DrawLine>();
				drawline.rangeOfY += 0.2f;
			}
		}
	}
		
	public void startGame() {
		SceneManager.LoadScene("game", LoadSceneMode.Single);
		isStart = true;
	}

	public void arrowDownUp() {
		pressingArrowDown = false;
	}

	public void arrowDownDown() {
		pressingArrowDown = true;
	}

	public void arrowUpUp() {
		pressingArrowUp = false;
	}

	public void arrowUpDown() {
		pressingArrowUp = true;
	}

	public void movePlayerUp () {
		player.transform.position += Vector3.up * upDownSpeed * Time.deltaTime;
	}

	public void movePlayerDown () {
		player.transform.position += Vector3.down * upDownSpeed * Time.deltaTime;
	}

	void addGoldRandomly() {
		InvokeRepeating("CreateGold", 1f, 1.7f);
	}

	public void CreateGold()
	{
		if (isStart) {
			float goldY = Random.Range (-0.7f, 0.7f);
			float goldX = Random.Range (5f, 9f);
			float newGoldX = player.transform.position.x + goldX;
			Instantiate(goldGO, new Vector3(newGoldX, goldY, 0), Quaternion.identity);
			oldGoldX = player.transform.position.x + goldX;

		}
	}

	public void pause(){
		isStart = (isStart == true) ? false : true;
	}

	private Vector2 leftFingerPos = Vector2.zero;
	private Vector2 leftFingerLastPos = Vector2.zero;
	private Vector2 leftFingerMovedBy = Vector2.zero;

	public float slideMagnitudeX  = 0.0f;
	public float slideMagnitudeY = 0.0f;

	private void moveByFinger() {
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				leftFingerPos = Vector2.zero;
				leftFingerLastPos = Vector2.zero;
				leftFingerMovedBy = Vector2.zero;

				slideMagnitudeX = 0f;
				slideMagnitudeY = 0f;

				// record start position
				leftFingerPos = touch.position;

			}

			else if (touch.phase == TouchPhase.Moved)
			{
				leftFingerMovedBy = touch.position - leftFingerPos; // or Touch.deltaPosition : Vector2
				// The position delta since last change.
				leftFingerLastPos = leftFingerPos;
				leftFingerPos = touch.position;

				// slide horz
				slideMagnitudeX = leftFingerMovedBy.x / Screen.width;

				// slide vert
				slideMagnitudeY = leftFingerMovedBy.y / Screen.height;

			}

			else if (touch.phase == TouchPhase.Stationary)
			{
				leftFingerLastPos = leftFingerPos;
				leftFingerPos = touch.position;

				slideMagnitudeX = 0.0f;
				slideMagnitudeY = 0.0f;
			}

			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
				slideMagnitudeX = 0.0f;
				slideMagnitudeY = 0.0f;
			}
			player.transform.Translate (0, slideMagnitudeY * 200f * Time.deltaTime, 0);
		}
	}
}
