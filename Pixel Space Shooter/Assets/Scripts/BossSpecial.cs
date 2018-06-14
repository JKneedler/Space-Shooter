using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecial : MonoBehaviour {

	public bool singleShot;
	public bool spreadShot;
	public int spreadAmt;
	public Vector2[] spreadVectors;
	public bool laser;
	public GameObject[] shotSpawners;
	public Object shotPrefab;
	float onScreenTimer;
	float attackTimer;
	GameObject player;
	public float shotSpeed;
	public float attackSpeed;
	private GameController master;

	// Use this for initialization
	void Start () {
		player = PlayerHealth.player;
		master = Camera.main.GetComponent<GameController> ();
		onScreenTimer = 2;
		if (spreadShot) {
			spreadVectors = CreateSpreads ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!master.playerKilled) {
			if (onScreenTimer <= 0) {
				if (attackTimer <= 0) {
					Attack ();
					attackTimer = attackSpeed;
				} else {
					attackTimer -= Time.deltaTime;
				}
			} else {
				onScreenTimer -= Time.deltaTime;
			}
		}
	}

	public Vector2[] CreateSpreads(){
		Vector2[] vects = new Vector2[spreadAmt];
		for (int i = 0; i < spreadAmt; i++) {
			float angle = ((float)i / (float)spreadAmt) * 360;
			if (angle > 180) {
				angle = 0 - (angle - 180);
			}
			float dirX = Mathf.Cos (angle * Mathf.Deg2Rad);
			float dirY = Mathf.Sin (angle * Mathf.Deg2Rad);
			vects [i] = new Vector2 (dirX, dirY);
		}
		return vects;
	}

	void Attack(){
		if (singleShot) {
			foreach (GameObject spawner in shotSpawners) {
				GameObject shot = (GameObject)Instantiate (shotPrefab, spawner.transform.position, this.transform.rotation);
				float angle = this.GetComponent<EnemyAI> ().angle;
				float dirX = Mathf.Cos (angle * Mathf.Deg2Rad) * shotSpeed;
				float dirY = Mathf.Sin (angle * Mathf.Deg2Rad) * shotSpeed;
				shot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (dirX, dirY);
				shot.GetComponent<ShotDamage> ().speed = shotSpeed;
			}
		} else if (spreadShot) {
			for (int i = 0; i < spreadVectors.Length; i++) {
				GameObject shot = (GameObject)Instantiate (shotPrefab, shotSpawners [0].transform.position, this.transform.rotation);
				shot.GetComponent<Rigidbody2D> ().velocity = spreadVectors [i] * shotSpeed;
				shot.GetComponent<ShotDamage> ().speed = shotSpeed;
			}
		} else if (laser) {
			gameObject.GetComponent<EnemyAI> ().shootingLaser = true;
			StartCoroutine (LaserWait ());
		}
	}

	IEnumerator LaserWait(){
		yield return new WaitForSeconds (2);
		GameObject laser = (GameObject)Instantiate (shotPrefab, shotSpawners [0].transform.position, this.transform.rotation);
		laser.transform.parent = gameObject.transform;
		yield return new WaitForSeconds (6);
		Destroy (laser);
		gameObject.GetComponent<EnemyAI> ().shootingLaser = false;
	}
}
