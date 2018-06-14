using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : SerializedMonoBehaviour {

	public Ability[] abilities;
	public bool[] boughtAbilities;
	public int playerLevel;

	// Use this for initialization
	void Start () {
		boughtAbilities = new bool[abilities.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
