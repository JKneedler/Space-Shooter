using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : SerializedMonoBehaviour {

	[TabGroup("Wave")]
	public int wave;
	[TabGroup("Wave")]
	public int maxEnemiesInWave;
	[TabGroup("Wave")]
	public int enemiesInArena;
	[TabGroup("Wave")]
	public bool waveInProgress;
	[TabGroup("Wave")]
	public List<Object> curWaveEnemies;
	[TabGroup("Wave")]
	public float releaseTimer;
	[TabGroup("Wave")]
	public float startWaveTimer;
	[TabGroup("Wave")]
	public bool preWave;
	[TabGroup("Wave")]
	public float asteroidTimer;
	[TabGroup("Entities")]
	public Object[] enemies;
	[TabGroup("Entities")]
	public Object[] asteroid;
	[TabGroup("Entities")]
	public Object[] bosses;
	[TabGroup("Info")]
	public GameObject player;
	[TabGroup("Info")]
	public float maxX;
	[TabGroup("Info")]
	public float maxY;
	[TabGroup("Info")]
	public GameObject nextWaveButton;
	[TabGroup("Info")]
	public GameObject waveText;
	[TabGroup("Info")]
	public bool paused;
	[TabGroup("Info")]
	public GameObject[] boundaries;
	[TabGroup("Info")]
	public GameObject gameOverScreen;
	[TabGroup("Info")]
	public bool playerKilled;

	// Use this for initialization
	void Start () {
		NextWave ();
		playerKilled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerKilled) {
			//Currently In Wave
			if (curWaveEnemies.Count > 0) {
				releaseTimer -= Time.deltaTime;
				waveInProgress = true;
				if (releaseTimer <= 0) {
					releaseTimer = (float)Random.Range (1, 4);
					if (enemiesInArena < maxEnemiesInWave) {
						if (maxEnemiesInWave - enemiesInArena > curWaveEnemies.Count) {
							ReleaseEnemies (curWaveEnemies.Count);
						} else {
							ReleaseEnemies (maxEnemiesInWave - enemiesInArena);
						}
					}
				}
			}

			if (enemiesInArena == 0 && nextWaveButton.activeSelf == false && !preWave) {
				nextWaveButton.SetActive (true);
				waveInProgress = false;
				paused = true;
			}

			//Asteroids
			if (paused) {
				player.GetComponent<PlayerMovement> ().ableToMove = false;
			} else {
				player.GetComponent<PlayerMovement> ().ableToMove = true;
				if (asteroidTimer > 0) {
					asteroidTimer -= Time.deltaTime;
				} else if (waveInProgress && asteroidTimer <= 0) {
					SpawnAsteroid ();
				}
			}

			//Wave About to Start
			if (startWaveTimer > 0) {
				startWaveTimer -= Time.deltaTime;
			} else if (startWaveTimer <= 0 && waveText.activeSelf && preWave) {
				waveText.SetActive (false);
				preWave = false;
				StartWave ();
			}
		}
	}

	void StartWave(){
		enemiesInArena = 0;
		curWaveEnemies = CreateWaveList ();
		releaseTimer = (float)Random.Range(1, 4);
		int set = Mathf.CeilToInt((float)wave/5);
		int minNum = set;
		int maxNum = 6 + (2 * set);
		maxEnemiesInWave = maxNum;
		int amtToRelease = Random.Range (minNum, maxNum);
		if (amtToRelease < curWaveEnemies.Count) {
			ReleaseEnemies (amtToRelease);
		} else {
			ReleaseEnemies (curWaveEnemies.Count);
		}
	}

	List<Object> CreateWaveList(){
		List<Object> waveEnemies = new List<Object>();
		int numOfEnemyTypes = Mathf.CeilToInt((float)wave/5) + 1;
		int waveNum = wave;
		if (wave % 5 == 0) {
			waveNum--;
			waveEnemies.Add (bosses [numOfEnemyTypes - 2]);
		}
		for (int i = 1; i <= numOfEnemyTypes; i++) {
			if (i == numOfEnemyTypes) {
				int amt = 2 * (waveNum % 5);
				for (int k = 0; k < amt; k++) {
					waveEnemies.Add (enemies [i - 1]);
				}
			} else {
				int amt = (4 * (numOfEnemyTypes - i)) + (2 * ((waveNum % 5) - 1));
				for (int k = 0; k < amt; k++) {
					waveEnemies.Add (enemies [i - 1]);
				}
			}
		}
		RandomizeList (waveEnemies);
		return waveEnemies;
	}

	void RandomizeList(List<Object> listToRandom){
		for (int i = 0; i < listToRandom.Count; i++) {
			Object temp = listToRandom[i];
			int randomIndex = Random.Range(i, listToRandom.Count);
			listToRandom[i] = listToRandom[randomIndex];
			listToRandom[randomIndex] = temp;
		}
	}

	void ReleaseEnemies(int amt){
		for (int i = 0; i < amt; i++) {
			float randX = (float)Random.Range (boundaries[0].transform.position.x, boundaries[2].transform.position.x);
			float randY = (float)Random.Range (boundaries[3].transform.position.y, boundaries[1].transform.position.y);
			int side = Random.Range (1, 4);
			switch (side) {
			case 1:
				Vector3 spawnPos1 = new Vector3 (boundaries[0].transform.position.x - 1, randY, 0);
				Instantiate (curWaveEnemies [0], spawnPos1, Quaternion.identity);
				curWaveEnemies.RemoveAt (0);
				break;
			case 2:
				Vector3 spawnPos2 = new Vector3 (randX, boundaries[1].transform.position.y + 1, 0);
				Instantiate (curWaveEnemies [0], spawnPos2, Quaternion.identity);
				curWaveEnemies.RemoveAt (0);
				break;
			case 3:
				Vector3 spawnPos3 = new Vector3 (boundaries[2].transform.position.x + 1, randY, 0);
				Instantiate (curWaveEnemies [0], spawnPos3, Quaternion.identity);
				curWaveEnemies.RemoveAt (0);
				break;
			case 4:
				Vector3 spawnPos4 = new Vector3 (randX, boundaries[3].transform.position.y - 1, 0);
				Instantiate (curWaveEnemies [0], spawnPos4, Quaternion.identity);
				curWaveEnemies.RemoveAt (0);
				break;
			}
			enemiesInArena++;
		}
	}

	public void EnemyDown(){
		enemiesInArena--;
	}

	public void NextWave(){
		wave++;
		waveInProgress = true;
		waveText.SetActive (true);
		waveText.GetComponent<Text> ().text = "Wave " + wave;
		preWave = true;
		nextWaveButton.SetActive (false);
		paused = false;
		startWaveTimer = 5;
	}

	public void SpawnAsteroid(){
		float randY = Random.Range (boundaries[1].transform.position.y - 2, boundaries[3].transform.position.y + 2);
		Vector3 spawnPos = new Vector3 (boundaries[0].transform.position.x, randY, 0);
		int randAst = Random.Range (0, asteroid.Length - 1);
		Instantiate (asteroid [randAst], spawnPos, Quaternion.identity);
		asteroidTimer = Random.Range (2, 4);
	}

	public void PlayAgain(){
		SceneManager.LoadScene ("Fight");
	}

	public void Exit(){
		SceneManager.LoadScene ("Start");
	}

	public void GameOver(){
		gameOverScreen.SetActive (true);
		playerKilled = true;
	}
}
