using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyVars : MonoBehaviour {
	public static GameObject varsObject;

	// Use this for initialization
	void Awake () {
		if (varsObject == null) {
			varsObject = this.gameObject;
			DontDestroyOnLoad (this.gameObject);
		} else if (varsObject != this) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
