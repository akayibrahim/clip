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
	public GameObject level;
	private int score = 0;
	private int scoreCount = 0;
	private int highScore = 0;
	
	private string highScorePrefsText = "HighScore";
	private string levelPrefsText = "Level";
	void Start () {
		highScore = PlayerPrefs.GetInt(highScorePrefsText);			
		bestScoreText = bestScoreTextGO.GetComponent<Text> ();
		bestScoreText.text = highScore.ToString();		
		level.GetComponent<Text>().text = PlayerPrefs.GetInt(levelPrefsText).ToString();	
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
