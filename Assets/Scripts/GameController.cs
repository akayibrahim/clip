using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

	public GameObject playGO;
	public GameObject shareGO;
	public GameObject bestScoreGO;
	public GameObject bestScoreTextGO;
	public GameObject clipGO;
	public GameObject arrowUp;
	public GameObject arrowDown;
	public GameObject tapToStart;
	public GameObject gameOverGO;
	public GameObject scoreGO;
	public GameObject player;
	public GameObject restartGO;
	public GameObject quitGO;
	public GameObject quitBackGO;
	public Text scoreText;
	private Text bestScoreText;
	public GameObject goldOneGO;
	public GameObject goldTwoGO;
	public GameObject goldThreeGO;
	public GameObject pauseGO;
	public GameObject soundGO;
	public GameObject noSoundGO;
	public AudioSource audioSource;
	public GameObject descGO;
	public GameObject pointGO;
	public Text pointText;

	private float moveSpeed = 2f;
	public float upDownSpeed = 2f;
	private float goldYRange = 0.7f;
	private float goldXStartRange = 5f;
	private float goldXEndRange = 9f;
	private float fingerTraslateClipSpeed = 200f;
	private float waitTime = 0.5f;
	private float zero = 0f;
	private float one = 1f;

	private bool pressingArrowDown;
	private bool pressingArrowUp;
	private bool isStart;
	private bool wait = false;

	// private int score = 0;
	private int scoreCount = 0;
	private int highScore = 0;
	private int soundToggle = 1; // 1 open, 0 close
	private int soundOn = 1; 
	private int soundOff = 0;

	private string bestScorePrefsText = "BestScore";
	private string soundToggleText = "SoundToggle";
	private string subject = "CLIP - Are u ready to CHALLENGE !!!";
	private string body = "PLAY THIS AWESOME GAME. GET IT ON THE PLAYSTORE AT LINK";

	void Start () {
		highScore = PlayerPrefs.GetInt(bestScorePrefsText);
		soundToggle = PlayerPrefs.GetInt (soundToggleText);
		setGOVisibleOnPlay ();
		addGoldRandomly ();
		openCloseSound (false);
	}

	void Update() {
		if (isStart) {
			float moveMultiple = scoreCount / 75f;
			player.transform.position += Vector3.right * (moveSpeed + moveMultiple) * Time.deltaTime;
			addScore ();
			if (Input.GetKey (KeyCode.UpArrow) || pressingArrowUp)
				movePlayerUp ();
			if (Input.GetKey(KeyCode.DownArrow) || pressingArrowDown)
				movePlayerDown ();
			moveByFinger ();
		}

		if(scoreCount > highScore) {
			highScore = scoreCount;
			PlayerPrefs.SetInt (bestScorePrefsText, highScore);
		}
		bestScoreText = bestScoreTextGO.GetComponent<Text> ();
		bestScoreText.text = highScore.ToString();
	}

	public void addScore() {
		scoreText.text = scoreCount.ToString() ;
		// ((score++) % 50 == 0 ? (scoreCount++).ToString() : scoreCount.ToString()) ;
		pointGO.SetActive (wait);
	}

	public void addScoreForCollectGold(int point) {
		scoreCount += point;
		pointText.text = "+" + point;
		StartCoroutine (waitOff());
	}
		
	public void startGame() {
		isStart = true;
	}

	public void GameOver() {
		isStart = false;

		List<GameObject> objectList = new List<GameObject> ();
		objectList.Add (gameOverGO);
		objectList.Add (restartGO);
		objectList.Add (quitGO);
		objectList.Add (quitBackGO);
		setVisibility (objectList, true);

		List<GameObject> objectListActive = new List<GameObject> ();
		objectListActive.Add (arrowUp);
		objectListActive.Add (arrowDown);
		objectListActive.Add (pauseGO);
		setVisibility (objectListActive, false);
	}


	public void restart() {	
		Application.LoadLevel (Application.loadedLevel); 
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

	public void setGOVisibleOnPlay() {
		List<GameObject> objectList = new List<GameObject> ();
		objectList.Add (gameOverGO);
		objectList.Add (restartGO);
		objectList.Add (quitGO);
		objectList.Add (scoreGO);
		objectList.Add (arrowUp);
		objectList.Add (arrowDown);
		objectList.Add (pauseGO);
		objectList.Add (pointGO);
		setVisibility (objectList, false);
	}

	public void setGOVisibleOnStart() {
		List<GameObject> objectList = new List<GameObject> ();
		objectList.Add (gameOverGO);
		objectList.Add (playGO);
		objectList.Add (shareGO);
		objectList.Add (bestScoreGO);
		objectList.Add (bestScoreTextGO);
		objectList.Add (clipGO);
		objectList.Add (tapToStart);
		objectList.Add (quitBackGO);
		objectList.Add (soundGO);
		objectList.Add (noSoundGO);
		objectList.Add (descGO);
		setVisibility (objectList, false);

		List<GameObject> objectListActive = new List<GameObject> ();
		objectListActive.Add (scoreGO);
		objectListActive.Add (pauseGO);
		objectList.Add (pointGO);
		// objectListActive.Add (arrowUp);
		// objectListActive.Add (arrowDown);
		setVisibility (objectListActive, true);
	}

	private void setVisibility(List<GameObject> objectList, bool visibility) {
		foreach (GameObject obj in objectList) {
			obj.SetActive (visibility);
		}
	}

	void addGoldRandomly() {
		InvokeRepeating("CreateGold", one, 1.7f);
	}

	public void CreateGold() {
		if (isStart) {
			float goldY = Random.Range (-goldYRange, goldYRange);
			float goldX = Random.Range (goldXStartRange, goldXEndRange);
			float newGoldX = player.transform.position.x + goldX;
			Vector3 pos = new Vector3 (newGoldX, goldY, zero);
			int randNum = Random.Range (0, 9);
			if (scoreCount > 100 && randNum % 7 == 0)
				Instantiate(goldThreeGO, pos, Quaternion.identity);
			else if(scoreCount > 50 && (randNum % 5 == 0 || randNum % 6 == 0))
				Instantiate (goldTwoGO, pos, Quaternion.identity);
			else
				Instantiate (goldOneGO, pos, Quaternion.identity);
		}
	}

	public void quit(){	
		Application.Quit ();
	}

	public void pause(){
		isStart = (isStart == true) ? false : true;
	}

	private Vector2 leftFingerPos = Vector2.zero;
	// private Vector2 leftFingerLastPos = Vector2.zero;
	private Vector2 leftFingerMovedBy = Vector2.zero;

	private void moveByFinger() {
		float slideMagnitudeX  = zero;
		float slideMagnitudeY = zero;

		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began) {
				leftFingerPos = Vector2.zero;
				// leftFingerLastPos = Vector2.zero;
				leftFingerMovedBy = Vector2.zero;

				slideMagnitudeX = zero;
				slideMagnitudeY = zero;

				// record start position
				leftFingerPos = touch.position;

			} else if (touch.phase == TouchPhase.Moved) {
				leftFingerMovedBy = touch.position - leftFingerPos; // or Touch.deltaPosition : Vector2
				// The position delta since last change.
				//  leftFingerLastPos = leftFingerPos;
				leftFingerPos = touch.position;

				// slide horz
				slideMagnitudeX = leftFingerMovedBy.x / Screen.width;

				// slide vert
				slideMagnitudeY = leftFingerMovedBy.y / Screen.height;

			} else if (touch.phase == TouchPhase.Stationary) {
				// leftFingerLastPos = leftFingerPos;
				leftFingerPos = touch.position;

				slideMagnitudeX = zero;
				slideMagnitudeY = zero;
			} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
				slideMagnitudeX = zero;
				slideMagnitudeY = zero;
			}
			player.transform.Translate (zero, slideMagnitudeY * fingerTraslateClipSpeed * Time.deltaTime, zero);
		}
	}

	public void openCloseSound(bool isClick){
		if ((audioSource.mute && isClick) || (soundToggle == soundOn && !isClick)) {
			PlayerPrefs.SetInt (soundToggleText, 1);
			audioSource.mute = false;
			audioSource.volume = one;
			soundGO.SetActive (true);
			noSoundGO.SetActive (false);
		} else if ((!audioSource.mute && isClick) || (soundToggle == soundOff && !isClick)) {
			PlayerPrefs.SetInt (soundToggleText, 0);
			audioSource.mute = true;
			audioSource.volume = zero;
			soundGO.SetActive (false);
			noSoundGO.SetActive (true);
		}
	}

	public void share() {
		#if UNITY_ANDROID
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("startActivity", intentObject);
		#endif

		#if UNITY_IOS
		#endif
	}
		
	IEnumerator waitOff () {
		wait = true;
		yield return new WaitForSeconds (waitTime);
		wait = false;
	}
}
