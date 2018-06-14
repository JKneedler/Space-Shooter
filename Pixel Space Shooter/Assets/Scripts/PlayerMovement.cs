using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : SerializedMonoBehaviour {

	public Vector2 moveDirection;
	public bool ableToMove;
	public bool moving;
	public float speed;
	private Rigidbody2D myRigid;
	public float angle;

	// Use this for initialization
	void Start () {
		myRigid = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Bound player to the camera bounds
//		Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
//		pos.x = Mathf.Clamp01(pos.x);
//		pos.y = Mathf.Clamp01(pos.y);
//		transform.position = Camera.main.ViewportToWorldPoint(pos);
		ChangePlayerAngle();

		//Find Movement direction
		if (Input.GetButton ("D")) {
			moveDirection.x = 1;
		} else if (Input.GetButton ("A")) {
			moveDirection.x = -1;
		} else {
			moveDirection.x = 0;
		}
		if (Input.GetButton ("W")) {
			moveDirection.y = 1;
		} else if (Input.GetButton ("S")) {
			moveDirection.y = -1;
		} else {
			moveDirection.y = 0;
		}
		if ((moveDirection.x != 0 || moveDirection.y != 0) && ableToMove) {
			moving = true;
		} else {
			moving = false;
		}
		if (moving) {
			Move ();
		} else {
			myRigid.velocity = new Vector2 (0, 0);
		}
	}

	void Move(){
		myRigid.velocity = moveDirection * speed;
	}

	void ChangePlayerAngle(){
		var mouse = Input.mousePosition;
		var screenPoint = Camera.main.WorldToScreenPoint (transform.localPosition);
		var offset = new Vector2 (mouse.x - screenPoint.x, mouse.y - screenPoint.y);
		angle = Mathf.Atan2 (offset.y, offset.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0, 0, angle - 90);
	}
}
