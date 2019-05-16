using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour {
	
	public GameObject playGO;
	public GameObject bestScoreGO;
	public GameObject bestScoreTextGO;
	public GameObject gameOverGO;
	public GameObject scoreGO;
	public GameObject restartGO;
	public GameObject quitGO;
	public GameObject quitBackGO;
	public Text scoreText;
	private Text bestScoreText;
	private int highScore = 0;

	private string highScorePrefsText = "HighScore";

	void Start () {
		highScore = PlayerPrefs.GetInt(highScorePrefsText);		
	}

	void Update() {
		bestScoreText = bestScoreTextGO.GetComponent<Text> ();
		bestScoreText.text = highScore.ToString();
	}

	public void restart() {	
		SceneManager.LoadScene("game", LoadSceneMode.Single);		
	}

	public void quit(){	
		Application.Quit ();
	}
}
