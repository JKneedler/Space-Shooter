using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : SerializedMonoBehaviour {

	public static GameObject player;
	public float curHealth;
	public float maxHealth;
	//public GameObject healthBar;
	//private Vector2 barOGRect;
	public SpriteRenderer healthBar;
	public Sprite[] healthSprites;

	void Awake(){
		player = this.gameObject;
	}

	// Use this for initialization
	void Start () {
		//barOGRect = healthBar.GetComponent<RectTransform> ().sizeDelta;
		UpdateHealthBar ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void UpdateHealthBar(){
		if (curHealth > maxHealth) curHealth = maxHealth;
		if (curHealth >= 0) {
			StartCoroutine("ShowHealth");
		}
		if (curHealth <= 0) {
			Destroy (gameObject);
			Camera.main.GetComponent<GameController> ().GameOver ();
		}
	}

	public void TakeDamage(float damage){
		curHealth -= (float)damage;
		UpdateHealthBar ();
	}

	public void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.GetComponent<EnemyAI> ()) {
			collision.gameObject.GetComponent<EnemyAI> ().RunIntoPlayer ();
		} else if (collision.gameObject.GetComponent<Asteroid>()) {
			collision.gameObject.GetComponent<Asteroid> ().HitPlayer ();
		}
	}

	IEnumerator ShowHealth(){
		int num = Mathf.Abs(Mathf.FloorToInt(curHealth/maxHealth * (healthSprites.Length-1)) - (healthSprites.Length-1));
		healthBar.sprite = healthSprites[num];
		yield return new WaitForSeconds(5);
		healthBar.sprite = null;
	}
}
