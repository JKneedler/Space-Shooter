using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Ability")]
public class Ability : ScriptableObject {
	public string abilityName;
	public string description;
	public int reqLevel;
	public Sprite icon;
	public float coolDown;
	public int price;
	[HideIf("passive")]
	public bool attack;
	[ShowIf("attack")]
	public Object attackItem;
	[ShowIf("attack")]
	public float damage;
	[ShowIf("attack")]
	public bool singleShot;
	[ShowIf("attack")]
	public bool spreadShot;
	[ShowIf("attack")]
	public bool laser;
	[HideIf("attack")]
	public bool passive;
	[ShowIf("passive")]
	public bool slowTime;
	[ShowIf("passive")]
	public bool ghost;
	[ShowIf("hasDuration")]
	public float duration;


	private bool hasDuration(){
		return laser || slowTime || ghost;
	}
}
