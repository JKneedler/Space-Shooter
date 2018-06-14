using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour {

	public float dps;
	private GameObject player;
	private PlayerHealth ph;

	// Use this for initialization
	void Start () {
		player = PlayerHealth.player;
		ph = player.GetComponent<PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerStay2D(Collider2D collision){
		if (collision.gameObject.tag == "Player") {
			ph.TakeDamage (dps * Time.deltaTime);
		}
	}
}
