using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditObject : SerializedMonoBehaviour {

	public int credits;
	public GameObject player;
	public float distance;
	private GameController master;
	public bool trailOn;

	// Use this for initialization
	void Start () {
		player = PlayerHealth.player;
		master = Camera.main.GetComponent<GameController> ();
		trailOn = true;
//		if (master.paused) {
//			trailOn = false;
//			transform.GetChild (0).GetComponent<ParticleSystem> ().Pause ();
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!master.playerKilled) {
			if (!master.paused) {
				transform.position = Vector2.MoveTowards (transform.position, player.transform.position, .008f);
				if (!trailOn) {
					ChangeTrailState ();
				}
			} else {
				if (trailOn) {
					ChangeTrailState ();
				}
			}
			distance = Vector2.Distance (transform.position, player.transform.position);
			if (distance < 2) {
				transform.position = Vector2.Lerp ((Vector2)transform.position, (Vector2)player.transform.position, .1f);
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.tag == "Player") {
			collision.GetComponent<PlayerInventory> ().AddCredits (credits);
			Destroy (gameObject);
		}
	}

	public void ChangeTrailState(){
		if (trailOn) {
			trailOn = false;
			transform.GetChild (0).GetComponent<ParticleSystem> ().Pause ();
		} else {
			trailOn = true;
			transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();
		}
	}
}
