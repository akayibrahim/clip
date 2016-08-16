using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using AppTrackerUnitySDK;
using StartApp;
using System.IO;

public class GameController : MonoBehaviour {

	public GameObject playGO;
	public GameObject shareGO;
	public GameObject bestScoreGO;
	public GameObject bestScoreTextGO;
	public GameObject clipTextGO;
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
	public GameObject iceGO;
	public GameObject sunGO;
	public GameObject sunOrIceTimeGO;
	public Text sunOrIceTimeText;
	public GameObject clipGO;
	public GameObject newGO;
	public AudioSource audioClip;
	public GameObject newHighScoreGO;

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
	private bool isGrowUpOrShrink = true;
	private bool wait = false;
	private bool showSunIceTime = false;
	private bool isGameOver = false;

	// private int score = 0;
	private int scoreCount = 0;
	private int highScore = 0;
	private int soundToggle = 1; // 1 open, 0 close
	private int soundOn = 1; 
	private int soundOff = 0;
	private int iceDeserveCount = 1;
	private int sunDeserveCount = 1;
	private int newBestScore = 0; // 1 true, 0 false
	private int gameOverCount = 0;

	private string bestScorePrefsText = "BestScore_4";
	private string newPrefsText = "NEW_BEST_SCORE";
	private string moveXSpeed = "MOVE_X_SPEED_3";
	private string soundToggleText = "SoundToggle";
	private string subject = "CLIP - READY TO CHALLENGE !!!";
	private string body = "NOW YOUR TURN. MY SCORE IS ";
	private string body2 = " Download Link : https://play.google.com/apps/testing/com.iakay/com";
	private string gameOverCountText = "GAME_OVER_COUNT";
	string destination;

	AudioClip iceClip;
	AudioClip sunClip;
	AudioClip goldClip;
	AudioClip gameOverClip;
	AudioClip successClip;

	void Start () {
		getFromSession ();
		setGOVisibleOnPlay ();
		addGoldRandomly ();
		addIceOrSunRandomly ();
		openCloseSound (false);
		LoadClips ();
		loadAds ();
	}

	private void LoadClips() {
		audioClip = gameObject.AddComponent<AudioSource> ();
		iceClip = (AudioClip) Resources.Load("Music/ice");
		sunClip = (AudioClip) Resources.Load("Music/sun");
		goldClip = (AudioClip) Resources.Load("Music/gold");
		gameOverClip = (AudioClip) Resources.Load("Music/gameover");
		successClip = (AudioClip) Resources.Load("Music/success");
		Texture2D img = (Texture2D) Resources.Load ("desc");
		destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
		byte[] bytes = img.EncodeToPNG ();
		File.WriteAllBytes(destination, bytes);
	}

	private void getFromSession() {
		highScore = PlayerPrefs.GetInt(bestScorePrefsText);
		soundToggle = PlayerPrefs.GetInt (soundToggleText);
		moveSpeed = (PlayerPrefs.GetFloat (moveXSpeed) != 0) ? PlayerPrefs.GetFloat (moveXSpeed) : moveSpeed;
		newBestScore = (PlayerPrefs.GetInt (newPrefsText) != 0) ? PlayerPrefs.GetInt (newPrefsText) : 0;
		gameOverCount = (PlayerPrefs.GetInt (gameOverCountText) != 0) ? PlayerPrefs.GetInt (gameOverCountText) : 0;
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
			sunOrIceTimeGO.SetActive (showSunIceTime);
			growingAndShring ();
		}
		setBestScore ();
		addDeserveToIceAndSun ();
		if (Input.GetKey (KeyCode.Escape))
			quit ();
	}

	private void addDeserveToIceAndSun() {
		if ((scoreCount + 1) % 25 == 0) {
			iceDeserveCount = iceDeserveCount != 0 ? iceDeserveCount++ : iceDeserveCount;
			sunDeserveCount = sunDeserveCount != 0 ? sunDeserveCount++ : sunDeserveCount;
		}
	}

	private int bestScoreCount = 0;
	private void playBestScoreClip() {
		if (bestScoreCount == 0) {
			playClip (successClip);
			bestScoreCount++;
		}
	}

	private void setBestScore() {
		if(scoreCount > highScore) {
			playBestScoreClip ();
			highScore = scoreCount;
			PlayerPrefs.SetInt (bestScorePrefsText, highScore);
			PlayerPrefs.SetInt (newPrefsText, 1);
			newHighScoreGO.SetActive (wait);
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
		playClip (goldClip);
	}
		
	public void startGame() {
		isStart = true;
	}

	public void GameOver() {
		if (!isGameOver) {
			playClip (gameOverClip);
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
			objectListActive.Add (sunOrIceTimeGO);
			setVisibility (objectListActive, false);

			PlayerPrefs.SetFloat (moveXSpeed, moveSpeed + float.Parse(scoreText.text) / 3000);

			PlayerPrefs.SetInt (gameOverCountText, ++gameOverCount);
			isGameOver = true;
			if (gameOverCount % 6 == 0)
				showAds (true);
			else if (gameOverCount % 3 == 0)
				showAds (false);
		}
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
		if (newBestScore == 1) {
			newGO.SetActive (true);
			PlayerPrefs.SetInt (newPrefsText, 0);
		}
	}

	public void setGOVisibleOnStart() {
		List<GameObject> objectList = new List<GameObject> ();
		objectList.Add (gameOverGO);
		objectList.Add (playGO);
		objectList.Add (shareGO);
		objectList.Add (bestScoreGO);
		objectList.Add (bestScoreTextGO);
		objectList.Add (clipTextGO);
		objectList.Add (tapToStart);
		objectList.Add (quitBackGO);
		objectList.Add (soundGO);
		objectList.Add (noSoundGO);
		objectList.Add (descGO);
		objectList.Add (newGO);
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
		InvokeRepeating("instantiateGold", one, 1.5f);
	}

	void addIceOrSunRandomly() {
		InvokeRepeating("instantiateIceAndSun", 5f, 5f);
	}

	private Vector3 getPos() {
		float goldY = Random.Range (-goldYRange, goldYRange);
		float goldX = Random.Range (goldXStartRange, goldXEndRange);
		float newGoldX = player.transform.position.x + goldX;
		Vector3 pos = new Vector3 (newGoldX, goldY, zero);
		return pos;
	}

	private void instantiateIceAndSun() {
		if (isGrowUpOrShrink && isStart) {
			int randNumSunIce = Random.Range (0, 99);
			if (randNumSunIce <= 50 && sunDeserveCount > 0) {
				Instantiate (sunGO, getPos(), Quaternion.identity);
				--sunDeserveCount;
			} else if (randNumSunIce > 50 && iceDeserveCount > 0) {
				Instantiate (iceGO, getPos(), Quaternion.identity);
				--iceDeserveCount;
			}
			isGrowUpOrShrink = false;
		}
	}

	public void addDeserveToIce() {
		iceDeserveCount++;
		isGrowUpOrShrink = true;
	}

	public void addDeserveToSun() {
		sunDeserveCount++;
		isGrowUpOrShrink = true;
	}

	public void sunIceEffect(bool grow) {
		if (grow)
			playClip (sunClip);
		else
			playClip (iceClip);
		StartCoroutine (waitSunOrIce (grow));
	}

	private void playClip(AudioClip clip) {
		audioClip.PlayOneShot (clip);
	}
		
	IEnumerator waitSunOrIce (bool grow) {
		growing = grow;
		shrinking = !grow;
		yield return new WaitForSeconds (3f);
		shrinking = false;
		growing = false;
		showSunIceTime = true;
		for(int i= 10; i > 0; i--) {
			sunOrIceTimeText.text = i.ToString();
			yield return new WaitForSeconds (1f);
		}
		growing = !grow;
		shrinking = grow;
		showSunIceTime = false;
		yield return new WaitForSeconds (3f);
		shrinking = false;
		growing = false;
		isGrowUpOrShrink = true;
	}

	bool shrinking = false;
	bool growing = false;

	public void growingAndShring() {
		if (shrinking)
			clipGO.transform.localScale -= Vector3.one * Time.deltaTime * 0.05f;
		else if(growing)
			clipGO.transform.localScale += Vector3.one * Time.deltaTime * 0.05f;
	}

	public void instantiateGold() {
		if (!isStart)
			return;
		int randNum = Random.Range (0, 9);
		if (scoreCount > 100 && randNum % 7 == 0)
			Instantiate(goldThreeGO, getPos(), Quaternion.identity);
		else if(scoreCount > 50 && (randNum % 5 == 0 || randNum % 6 == 0))
			Instantiate (goldTwoGO, getPos(), Quaternion.identity);
		else
			Instantiate (goldOneGO, getPos(), Quaternion.identity);
	}

	public void quit() {	
		Application.Quit ();
	}

	public void pause(){
		isStart = (isStart == true) ? false : true;
	}

	private Vector2 leftFingerPos = Vector2.zero;
	// private Vector2 leftFingerLastPos = Vector2.zero;
	private Vector2 leftFingerMovedBy = Vector2.zero;

	private void moveByFinger() {
		// float slideMagnitudeX  = zero;
		float slideMagnitudeY = zero;

		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began) {
				leftFingerPos = Vector2.zero;
				// leftFingerLastPos = Vector2.zero;
				leftFingerMovedBy = Vector2.zero;

				// slideMagnitudeX = zero;
				slideMagnitudeY = zero;

				// record start position
				leftFingerPos = touch.position;

			} else if (touch.phase == TouchPhase.Moved) {
				leftFingerMovedBy = touch.position - leftFingerPos; // or Touch.deltaPosition : Vector2
				// The position delta since last change.
				//  leftFingerLastPos = leftFingerPos;
				leftFingerPos = touch.position;

				// slide horz
				// slideMagnitudeX = leftFingerMovedBy.x / Screen.width;

				// slide vert
				slideMagnitudeY = leftFingerMovedBy.y / Screen.height;

			} else if (touch.phase == TouchPhase.Stationary) {
				// leftFingerLastPos = leftFingerPos;
				leftFingerPos = touch.position;

				// slideMagnitudeX = zero;
				slideMagnitudeY = zero;
			} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
				// slideMagnitudeX = zero;
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
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		intentObject.Call<AndroidJavaObject>("setType", "image/png");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body  + highScore.ToString() 
			+ body2);
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

	private string leadBoltAdsType = "inapp"; // video
	private void loadAds() {
		#if UNITY_IOS
		#endif

		#if UNITY_ANDROID
			StartAppWrapper.init();
			StartAppWrapper.loadAd();
			AppTrackerAndroid.startSession("I4PadYNJDmW7y6cYMNO3Qrzgb8qvCspT", true);
			AppTrackerAndroid.loadModuleToCache(leadBoltAdsType); // 
		#endif
	}

	private void showAds(bool isLeadBoltAds) {
		#if UNITY_IOS
		#endif

		#if UNITY_ANDROID
			if (isLeadBoltAds) {
				if(AppTrackerAndroid.isAdReady(leadBoltAdsType))
					AppTrackerAndroid.loadModule(leadBoltAdsType);
			} else {
				StartAppWrapper.showAd();	
				StartAppWrapper.loadAd();
			}
		#endif
	}
}
