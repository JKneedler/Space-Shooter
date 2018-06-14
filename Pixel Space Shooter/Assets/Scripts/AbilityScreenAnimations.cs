using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityScreenAnimations : MonoBehaviour {
	private AbilitySystem abilitySystem;
	public int currentIndex;
	public int tempLayerNum;
	public Animator anim;
	public bool canRotate;
	public GameObject abilityButton;
	public Text abilityTitle;
	public Text abilityDesc;
	public GameObject player;
	public GameObject equipButton;
	public GameObject buyButton;
	public GameObject lockedButton;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		abilitySystem = DontDestroyVars.varsObject.GetComponent<AbilitySystem> ();
		UpdateIcons ();
		canRotate = true;
		player = PlayerHealth.player;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) {
			changeLayers (4);
		}
		if(Input.GetKeyUp(KeyCode.G)){
			changeBack();
		}
	}

	public void changeLayers(int newMiddle){
		transform.GetChild (newMiddle).SetAsLastSibling ();
		tempLayerNum = newMiddle;
	}

	public void changeBack(){
		transform.GetChild (transform.childCount - 1).SetSiblingIndex (tempLayerNum);
		UpdateIcons ();
		canRotate = true;
	}

	public void upgradeScreenRight (){
		if (canRotate) {
			if (currentIndex == abilitySystem.abilities.Length - 1) {
				currentIndex = 0;
			} else {
				currentIndex++;
			}
			anim.SetTrigger ("clicked_right");
			canRotate = false;
			ClearCurrentAbScreen ();
		}
	}

	public void upgradeScreenLeft (){
		if (canRotate) {
			if (currentIndex == 0) {
				currentIndex = abilitySystem.abilities.Length - 1;
			} else {
				currentIndex--;
			}
			anim.SetTrigger ("clicked_left");
			canRotate = false;
			ClearCurrentAbScreen ();
		}
	}

	public void ClearCurrentAbScreen(){
		abilityTitle.text = "";
		abilityDesc.text = "";
		abilityButton.transform.GetChild (2).GetComponent<Text> ().text = "";
	}

	public int back(int ogN){
		if (ogN == 0) {
			return abilitySystem.abilities.Length - 1;
		} else {
			return ogN - 1;
		}
	}

	public int forward(int ogN){
		if (ogN == abilitySystem.abilities.Length - 1) {
			return 0;
		} else {
			return ogN + 1;
		}
	}

	public void UpdateIcons(){
		int back1 = back (currentIndex);
		int back2 = back (back1);
		int back3 = back (back2);
		int forward1 = forward (currentIndex);
		int forward2 = forward (forward1);
		int forward3 = forward (forward2);
		transform.GetChild (0).GetComponent<Image> ().sprite = abilitySystem.abilities [back3].icon;
		transform.GetChild (1).GetComponent<Image> ().sprite = abilitySystem.abilities [back2].icon;
		transform.GetChild (4).GetComponent<Image> ().sprite = abilitySystem.abilities [back1].icon;
		transform.GetChild (2).GetComponent<Image> ().sprite = abilitySystem.abilities [forward3].icon;
		transform.GetChild (3).GetComponent<Image> ().sprite = abilitySystem.abilities [forward2].icon;
		transform.GetChild (5).GetComponent<Image> ().sprite = abilitySystem.abilities [forward1].icon;
		transform.GetChild (6).GetComponent<Image> ().sprite = abilitySystem.abilities [currentIndex].icon;
		UpdateCurAbScreen ();
	}

	public void UpdateCurAbScreen(){
		Ability ab = abilitySystem.abilities [currentIndex];
		abilityTitle.text = ab.abilityName;
		abilityDesc.text = ab.description;
		if (ab.reqLevel <= abilitySystem.playerLevel) {
			if (abilitySystem.boughtAbilities [currentIndex]) {
				equipButton.SetActive(true);
				buyButton.SetActive (false);
				lockedButton.SetActive (false);
			} else {
				equipButton.SetActive(false);
				buyButton.SetActive (true);
				lockedButton.SetActive (false);			}
		} else {
			equipButton.SetActive(false);
			buyButton.SetActive (false);
			lockedButton.SetActive (true);		}
	}

//	public void ClickedAbility(){
//		if (player.GetComponent<PlayerAbilities> ().playerLevel >= abilitySystem.abilities [currentIndex].reqLevel) {
//			if (abilitySystem.boughtAbilities [currentIndex] == true) {
//				player.GetComponent<PlayerAbilities> ().equippedAbility = abilitySystem.abilities [currentIndex];
//			} else if (player.GetComponent<PlayerInventory> ().credits > abilitySystem.abilities [currentIndex].price) {
//				abilitySystem.boughtAbilities [currentIndex] = true;
//				player.GetComponent<PlayerInventory> ().SpendCredits (abilitySystem.abilities [currentIndex].price);
//			}
//		}
//		UpdateCurAbScreen ();
//	}

	public void ClickedBuy(){
		if (player.GetComponent<PlayerInventory> ().credits > abilitySystem.abilities [currentIndex].price) {
			abilitySystem.boughtAbilities [currentIndex] = true;
			player.GetComponent<PlayerInventory> ().SpendCredits (abilitySystem.abilities [currentIndex].price);
		}
		UpdateCurAbScreen ();
	}

	public void ClickedEquip(){
		player.GetComponent<PlayerAbilities> ().EquipAbility (abilitySystem.abilities [currentIndex]);
	}
}
