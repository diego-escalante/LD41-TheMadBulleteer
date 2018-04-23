using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject gameUI;
	public GameObject menuUI;
	public GameObject howToPlay;
	public GameObject highScores;
	public GameObject titleScreen;
	public GameObject endGame;
	public GameObject NameInput;
	public GameObject highScoreStuff;

	private int finalScore = 1;

	public void StartGame() {
		menuUI.SetActive(false);
		gameUI.SetActive(true);
		howToPlay.SetActive(false);
		highScores.SetActive(false);
		titleScreen.SetActive(false);
		endGame.SetActive(false);
		highScoreStuff.SetActive(false);
		GameObject player = GameObject.FindWithTag("Player");
		player.GetComponent<ShieldBehavior>().enabled = true;
		player.GetComponent<PlayerShip>().enabled = true;
		player.GetComponent<SpriteRenderer>().enabled = true;
		player.GetComponent<Collider2D>().enabled = true;
		player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
		player.transform.position = new Vector3(0, -4, 0);
		GetComponent<BulletStockpile>().SetBulletCount(0f);
		GetComponent<EnemySpawner>().enabled = true;
	}

	public void EndGame(int score) {
		GameObject player = GameObject.FindWithTag("Player");
		player.GetComponent<ShieldBehavior>().enabled = false;
		player.GetComponent<PlayerShip>().enabled = false;
		player.GetComponent<SpriteRenderer>().enabled = false;
		player.GetComponent<Collider2D>().enabled = false;
		player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;


		gameUI.SetActive(false);
		menuUI.SetActive(true);
		howToPlay.SetActive(false);
		highScores.SetActive(false);
		titleScreen.SetActive(false);
		endGame.SetActive(true);
		GetComponent<EnemySpawner>().enabled = false;

		endGame.GetComponent<Text>().text = "Final Score:\n" + score.ToString();
		finalScore = score;
		highScoreStuff.SetActive(GetComponent<HighScoreManager>().IsHighScore(finalScore));
	}

	public void HowToPlay() {
		howToPlay.SetActive(true);
		highScores.SetActive(false);
		titleScreen.SetActive(false);
		endGame.SetActive(false);
		highScoreStuff.SetActive(false);
	}

	public void HighScores() {
		howToPlay.SetActive(false);
		highScores.SetActive(true);
		titleScreen.SetActive(false);
		endGame.SetActive(false);
		highScoreStuff.SetActive(false);
	}

	public void TitleScreen() {
		howToPlay.SetActive(false);
		highScores.SetActive(false);
		titleScreen.SetActive(true);
		endGame.SetActive(false);
		highScoreStuff.SetActive(false);
	}

	public void SubmitScore() {
		string name = NameInput.GetComponent<Text>().text;
		if (name == "") {
			return;
		}

		GetComponent<HighScoreManager>().SubmitHighScore(name, finalScore);
		HighScores();
	}

	public void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
