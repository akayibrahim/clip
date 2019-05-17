using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour {
	
	public GameObject playGO;
	public GameObject shareGO;
	public GameObject bestScoreGO;
	public GameObject bestScoreTextGO;
	public GameObject clipGO;
	public GameObject tapToStart;
	public Text scoreText;
	private Text bestScoreText;

	private int score = 0;
	private int scoreCount = 0;
	private int highScore = 0;

	private string highScorePrefsText = "HighScore";

	void Start () {
		highScore = PlayerPrefs.GetInt(highScorePrefsText);			
		bestScoreText.text = highScore.ToString();			
	}

	void Update() {
		
	}

	public void addScoreForCollectGold() {
		scoreCount += 1;
	}
		
	public void startGame() {
		SceneManager.LoadScene("game", LoadSceneMode.Single);		
	}
}
