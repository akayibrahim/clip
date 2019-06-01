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
	private int scoreCount = 0;
	private int highScore = 0;
	void Start () {
		PlayerPrefs.SetInt(Constants.scorePrefsText, 0);
		highScore = PlayerPrefs.GetInt(Constants.highScorePrefsText);			
		bestScoreText = bestScoreTextGO.GetComponent<Text> ();
		bestScoreText.text = highScore.ToString();		
		level.GetComponent<Text>().text = PlayerPrefs.GetInt(Constants.levelPrefsText).ToString();	
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
