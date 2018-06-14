using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : SerializedMonoBehaviour {

	public int credits;
	public GameObject creditDisplay;

	// Use this for initialization
	void Start () {
		UpdateCredits ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddCredits(int credits){
		this.credits += credits;
		UpdateCredits ();
	}

	public void SpendCredits(int credits){
		this.credits -= credits;
		UpdateCredits ();
	}

	public void UpdateCredits(){
		creditDisplay.GetComponent<Text> ().text = "" + credits;
	}
}
