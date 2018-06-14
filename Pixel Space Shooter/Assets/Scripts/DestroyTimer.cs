using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour {

	public float lifeTime;
	private float timer;
	public bool functionDestroy;

	void Start () {
		timer = lifeTime;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0 && !functionDestroy) {
			Destroy (gameObject);
		}
	}

	public void Destory(){
		Destroy (gameObject);
	}
}
