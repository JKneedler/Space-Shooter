using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : SerializedMonoBehaviour {

	public GameObject spawnPoint;
	public Object standardShot;
	public bool shoot;
	private float standardShootTimer;
	public float shotSpeed;
	public float shotFrequency;
	private GameController master;

	// Use this for initialization
	void Start () {
		master = Camera.main.GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (standardShootTimer > 0) {
			standardShootTimer -= Time.deltaTime;
		}
		if (!master.paused) {
			if (Input.GetKey (KeyCode.Space) || Input.GetButton("Fire1")) {
				shoot = true;
			} else {
				shoot = false;
			}
			if (shoot) {
				ShootStandard ();
			} else {

			}
		} else {
			
		}
	}

	void ShootStandard(){
		if (standardShootTimer <= 0) {
			GameObject shot = (GameObject)Instantiate (standardShot, spawnPoint.transform.position, transform.rotation);
			float angle = gameObject.GetComponent<PlayerMovement> ().angle;
			float dirX = Mathf.Cos (angle * Mathf.Deg2Rad) * shotSpeed;
			float dirY = Mathf.Sin (angle * Mathf.Deg2Rad) * shotSpeed;
			shot.GetComponent<Rigidbody2D> ().velocity = new Vector2(dirX, dirY);
			standardShootTimer = shotFrequency;
		}
	}
}
