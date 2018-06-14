using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : SerializedMonoBehaviour {

	public GameObject player;
	public bool ableToMove;
	public float speed;
	public float angle;
	public int damage;
	public bool shootingLaser;
	public float slowRotateSpeed;
	private GameController master;
	public GameObject target;

	// Use this for initialization
	void Start () {
		ableToMove = true;
		player = PlayerHealth.player;
		master = Camera.main.GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ableToMove) {
			if (!master.playerKilled) {
				transform.position = Vector3.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
				if (shootingLaser) {
					SlowlyChangeAngle ();
				} else {
					ChangeAngle (player);
				}
			} else {
				//transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime);
				//ChangeAngle (target);
			}
		}
	}

	void ChangeAngle(GameObject aimTarget){
		Vector3 dir = aimTarget.transform.position - transform.position;
		angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
	}

	void SlowlyChangeAngle(){
		Vector3 dir = player.transform.position - transform.position;
		angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion newRot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		transform.rotation = Quaternion.Lerp (transform.rotation, newRot, slowRotateSpeed * Time.deltaTime);
	}

	public void RunIntoPlayer(){
		player.GetComponent<PlayerHealth> ().TakeDamage (damage);
		gameObject.GetComponent<EnemyHealth> ().Explode ();
	}
}
