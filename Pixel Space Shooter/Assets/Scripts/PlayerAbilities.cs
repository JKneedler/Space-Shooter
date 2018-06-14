using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour {

	public int playerLevel;
	public Ability equippedAbility;
	public Transform laserSpawn;
	private float abilityTimer;
	public GameObject abilityIcon;
	public int healthLevel;
	public Text healthPrice;
	public int speedLevel;
	public Text speedPrice;
	public int damageLevel;
	public Text damagePrice;

	// Use this for initialization
	void Start () {
		healthLevel = 1;
		speedLevel = 1;
		damageLevel = 1;
		abilityIcon.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
//		if (equippedAbility != null) {
//			abilityIcon.GetComponent<Image> ().sprite = equippedAbility.icon;
//		} else {
//			
//		}
		if (abilityTimer <= 0) {
			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				Debug.Log ("Used ability");
				abilityTimer = 5;
			}
		}
		if (abilityTimer > 0) {
			abilityTimer -= Time.deltaTime;
		}
	}

	public void UpgradeHealthLevel(){
		int healthUpgradePrice = (200 * healthLevel);
		if (gameObject.GetComponent<PlayerInventory> ().credits > healthUpgradePrice) {
			healthLevel++;
			gameObject.GetComponent<PlayerHealth> ().maxHealth = 50 + (50 * (healthLevel - 1));
			gameObject.GetComponent<PlayerInventory> ().SpendCredits(healthUpgradePrice);
			healthUpgradePrice = (200 * healthLevel);
			healthPrice.text = "" + healthUpgradePrice;
			gameObject.GetComponent<PlayerHealth> ().UpdateHealthBar ();
		}
	}

	public void EquipAbility(Ability ab){
		if (equippedAbility == null) {
			abilityIcon.SetActive (true);
		}
		equippedAbility = ab;
		abilityIcon.GetComponent<Image> ().sprite = ab.icon;
	}

	public void UpgradeSpeedLevel(){
		//speedLevel++;
		//gameObject.GetComponent<PlayerMovement> ().speed = 2 + (.5f * (speedLevel-1));
	}

	public void UpgradeDamageLevel(){
		//damageLevel++;
		//gameObject.GetComponent<PlayerShoot>().shotFrequency = .4f - (.03f * (damageLevel-1));
	}
}
