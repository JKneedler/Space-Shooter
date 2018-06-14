using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDamage : SerializedMonoBehaviour {

	public float damage;
	public Object explosion;
	public bool bossAttack;
	public bool tracker;
	private float playerDistance;
	private GameObject player;
	private float angle;
	public float speed;
	private bool homing;
	private GameController master;

	// Use this for initialization
	void Start () {
		player = PlayerHealth.player;
		master = Camera.main.GetComponent<GameController> ();
		homing = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!master.playerKilled) {
			if (tracker && homing) {
				playerDistance = Vector2.Distance (player.transform.position, transform.position);
				if (playerDistance > 2) {
					Track ();
				} else {
					homing = false;
				}
			}
		}
	}

	private void Track(){
		Vector3 dir = player.transform.position - transform.position;
		angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		float dirX = Mathf.Cos (angle * Mathf.Deg2Rad) * speed;
		float dirY = Mathf.Sin (angle * Mathf.Deg2Rad) * speed;
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(dirX, dirY);

	}

	public void OnTriggerEnter2D(Collider2D collision){
		if ((collision.GetComponent<EnemyHealth> () || collision.GetComponent<Asteroid>()) && !bossAttack) {
			Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D> ();
			if (collision.gameObject.GetComponent<EnemyHealth> ()) {
				collision.GetComponent<EnemyHealth> ().TakeDamage (damage);
			}
			if (collision.gameObject.GetComponent<Asteroid> ()) {
				collision.gameObject.GetComponent<Asteroid> ().TakeDamage (damage);
			}
			float dirX = transform.position.x + (rigid.velocity.x * .05f);
			float dirY = transform.position.y + (rigid.velocity.y * .05f);
			Vector3 expPos = new Vector3 (dirX, dirY, transform.position.z);
			Instantiate (explosion, expPos, Quaternion.identity);
			Destroy (gameObject);
		}
		if (bossAttack) {
			if (collision.gameObject.tag == "Player") {
				Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D> ();
				collision.gameObject.GetComponent<PlayerHealth> ().TakeDamage ((int)damage);
				float dirX = transform.position.x + (rigid.velocity.x * .05f);
				float dirY = transform.position.y + (rigid.velocity.y * .05f);
				Vector3 expPos = new Vector3 (dirX, dirY, transform.position.z);
				Instantiate (explosion, expPos, Quaternion.identity);
				Destroy (gameObject);
			}
		}
	}

	public void OnTriggerExit2D(Collider2D collision){
		if (collision.gameObject.tag == "Boundary") {
			Destroy (gameObject);
		}
	}
}
