using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : SerializedMonoBehaviour {

	public float curHealth;
	public float maxHealth;
	public Object creditDrop;
	public int minDrop;
	public int maxDrop;


	// Use this for initialization
	void Start () {
		curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (curHealth <= 0) {
			Explode ();
			curHealth = 1;
		}
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
	}

	public void TakeDamage(float damage){
		curHealth -= damage;
	}

	public void Explode(){
		gameObject.GetComponent<EnemyAI> ().ableToMove = false;
		if (gameObject.GetComponent<Animator> ()) {
			gameObject.GetComponent<Animator> ().SetTrigger ("Explode");
			gameObject.GetComponent<Collider2D> ().enabled = false;
		} else {
			Die ();
		}
	}

	public void Die(){
		GameObject credit = (GameObject)Instantiate (creditDrop, transform.position, Quaternion.identity);
		CreditObject creditObj = credit.GetComponent<CreditObject> ();
		creditObj.credits = Random.Range (minDrop, maxDrop);
		Camera.main.GetComponent<GameController> ().EnemyDown ();
		Destroy (gameObject);
	}
}
