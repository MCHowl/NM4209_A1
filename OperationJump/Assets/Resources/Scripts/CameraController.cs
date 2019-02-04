using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

	public Canvas mainCanvas;
	public Canvas gameCanvas;
	public Canvas gameOverCanvas;

	public TMPro.TextMeshProUGUI scoreText;
	private int score = 0;
	private int scoreOffset = 9;
	private float spawnOffset = 2.5f;

	public GameObject PlayerPrefab;
	private PlayerController player;

	private Vector3 positionalOffset;

	public bool isGame { get; set; }

    void Start() {
		isGame = false;
		gameOverCanvas.enabled = false;
		gameCanvas.enabled = false;
    }

	void Update() {
		if (isGame) {
			if (player == null) {
				EndGame();
			}
		}

		if (!isGame && Input.anyKeyDown) {
			if (mainCanvas.enabled) {
				mainCanvas.enabled = false;
				InstantiateGame();
			}

			if (gameOverCanvas.enabled) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}

	void LateUpdate() {
		if (isGame) {
			float playerY = player.gameObject.transform.position.y;
			Vector3 newPosition;

			if (playerY > transform.position.y) {
				newPosition = new Vector3(0, playerY, 0);
				transform.position = newPosition + positionalOffset;

				if ((int)((playerY + scoreOffset) / 2.4) > score) {
					score = (int)((playerY + scoreOffset) / 2.4);
				}
				scoreText.text = "Score: " + score;
			} else {
				newPosition = transform.position + Vector3.up * 0.002f;
				transform.position = newPosition;
			}
		}
    }

	void InstantiateGame() {
		player = Instantiate(PlayerPrefab).GetComponent<PlayerController>();
		positionalOffset = transform.position - player.transform.position - new Vector3(0, spawnOffset, 0);

		isGame = true;
		gameCanvas.enabled = true;
	}

	void EndGame() {
		isGame = false;
		gameOverCanvas.enabled = true;
	}
}
