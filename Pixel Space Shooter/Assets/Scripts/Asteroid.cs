using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public float speedMin;
	public float speedMax;
	private float finalSpeed;
	public Vector2 direction;
	public GameController con;
	public float damage;
	public float curHealth;
	public float maxHealth;
	public Object creditDrop;
	public int minDrop;
	public int maxDrop;
	private float rotateSpeed;
	private int rotateDir;
	public bool trailOn;

	// Use this for initialization
	void Start () {
		curHealth = maxHealth;
		con = Camera.main.GetComponent<GameController> ();
		finalSpeed = Random.Range (speedMin, speedMax) * .5f;
		rotateSpeed = Random.Range (10, 50);
		rotateDir = Random.Range (0, 2);
		if (rotateDir == 0) rotateDir = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if (curHealth <= 0) {
			Die ();
		}
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (con.paused) {
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(0, 0);
			if (trailOn) {
				ChangeTrailState ();
			}
		} else {
			transform.Rotate (new Vector3 (0, 0, rotateDir * rotateSpeed * Time.deltaTime));
			gameObject.GetComponent<Rigidbody2D> ().velocity = direction * finalSpeed;
			if (!trailOn) {
				ChangeTrailState ();
			}
		}
	}

	public void HitPlayer(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ().TakeDamage ((int)damage);
		Die ();
	}

	public void TakeDamage(float damage){
		curHealth -= damage;
	}

	public void Die(){
		GameObject credit = (GameObject)Instantiate (creditDrop, transform.position, Quaternion.identity);
		CreditObject creditObj = credit.GetComponent<CreditObject> ();
		creditObj.credits = Random.Range (minDrop, maxDrop);
		Destroy (gameObject);
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
