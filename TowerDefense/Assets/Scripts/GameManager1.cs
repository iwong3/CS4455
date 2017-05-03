using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour {

	int remaining;

	public GameObject player;
	public GameObject nexus;

	public Transform[] spawnPoints;

	public GameObject enemy;
	public GameObject intro;
	public GameObject hud;
	public GameObject defeat;
	public GameObject victory;

	public Text healthNumber;
	public Text waveNumber;
	public Text nexusNumber;

	//0 = start, 1 = playing, 2 = gameover
	int state;
	int wave;
	float timer;
	bool displayingIntro;
	PlayerHealth playerHealth;
	NexusStats nexusScript;

	void Start() {
		state = 0;
		wave = 1;
		displayingIntro = false;
		intro.SetActive(true);
		hud.SetActive(false);
		defeat.SetActive(false);
		victory.SetActive(false);
		playerHealth = player.GetComponent<PlayerHealth>();
		nexusScript = nexus.GetComponent<NexusStats>();
	}

	void FixedUpdate() {
		if (state == 0) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				state = 1;
				remaining = 3;
				timer = 5;
				DisplayHUD();
			}
		} else if (state == 1) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
			if (playerHealth.currentHealth <= 0) {
				GameOver();
			} else if (NexusStats.nexusHealth <= 0) {
				GameOver();
			} else if (wave == 4) {//!!! this is obviously going to have to change back to a higher number, 5 atm
				Debug.Log("trigger victory");
				Victory();
			} else {
				if (timer >= 5.5f) {
					if (enemies.Length == 0) {
						wave += 1;
						timer = 0;
						UpdateWaveNumber();
						if (wave == 2) {
							remaining = 5;
						}
						if (wave == 3) {
							remaining = 10;
						}
					}
				} else if (wave == 1) {
					if (timer >= 5 && remaining > 0) {
						SpawnEnemy(spawnPoints[0], 1);
						remaining--;
						timer = 0;
					}
				} else if (wave == 2) {
					if (timer >= 4 && remaining > 0) {
						//if (timer % 2 == 0) {
						//    SpawnEnemy(spawnPoints[0], 1);
						SpawnEnemy(spawnPoints[1], 1);
						remaining--;//
						timer = 0;//
						//} else {
						//SpawnEnemy(spawnPoints[2], 1);
						//SpawnEnemy(spawnPoints[3], 1);
						//}
					}
				} else if (wave == 3 && remaining > 0) {
					if (timer >= 4) {
						//if (timer % 2 == 0) {
						SpawnEnemy(spawnPoints[0], 1);
						SpawnEnemy(spawnPoints[1], 1);
						remaining -= 2;
						timer = 0;
						//} else {
						//    SpawnEnemy(spawnPoints[1], 1);
						//    SpawnEnemy(spawnPoints[3], 1);
						//}
					}
				} else if (wave == 0) {//!!! change back to 4 later
					if (timer % 2 == 0) {
						SpawnEnemy(spawnPoints[(int) Random.value * 3], 1);
					}
				}
				timer += Time.deltaTime;
			}
			UpdateHealth();
		} else if (state == 2) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				SceneManager.LoadScene("Menu");
			}
		}
	}

	void DisplayHUD() {
		intro.SetActive(false);
		hud.SetActive(true);
	}

	void SpawnEnemy(Transform spawnLocation, int numberEnemies) {
		for (int i = 0; i < numberEnemies; i++) {
			Instantiate(enemy, spawnLocation.position, spawnLocation.rotation);
		}
	}

	void UpdateHealth() {
		healthNumber.GetComponent<Text>().text = playerHealth.currentHealth.ToString();
		nexusNumber.GetComponent<Text>().text = NexusStats.nexusHealth.ToString();
	}

	void UpdateWaveNumber() {
		waveNumber.GetComponent<Text>().text = "Wave " + wave.ToString();
	}

	void GameOver() {
		hud.SetActive(false);
		defeat.SetActive(true);
		state = 2;
	}

	void Victory() {
		hud.SetActive(false);
		victory.SetActive(true);
		state = 2;
	}

	public int getState() {
		return state;
	}
}