﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : SerializedMonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartPressed(){
		SceneManager.LoadScene ("Fight");
	}

	public void ExitPressed(){
		
	}
}
