using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

	Vector3 movedir;
	public float movespeed;
	bool jumping;

	void Start() {
		jumping = false;
	}

	void FixedUpdate () {
		movedir = new Vector3(0,0,0);

		if(Input.GetKey(KeyCode.W))
			movedir += Vector3.forward;

		if(Input.GetKey(KeyCode.S))
			movedir += Vector3.back;

		if(Input.GetKey(KeyCode.A))
			movedir += Vector3.left;

		if(Input.GetKey(KeyCode.D))
			movedir += Vector3.right;

		ongroundi();

		if(Input.GetKey(KeyCode.Space) && !jumping)
		{
			jumping = true;
			rigidbody.AddRelativeForce(Vector3.up * 80);
		}

		movedir.Normalize();

		rigidbody.AddRelativeForce(movedir * movespeed);
	}

	void ongroundi() {
		if(!jumping) return;

		if(Physics.Raycast(transform.position, Vector3.down, 1.2f)) {
			jumping = false;
		}
	}
}
