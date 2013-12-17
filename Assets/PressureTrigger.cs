using UnityEngine;
using System.Collections;

public class PressureTrigger : MonoBehaviour {

	int incount;
	public GameObject door;

	void FixedUpdate() {
		incount = 0;
	}

	void OnTriggerStay(Collider other) {
		if(other.name == "explode_robit") incount++;

		if(incount >= 2) openthedoor();
	}

	void openthedoor() { 
		door.rigidbody.constraints = RigidbodyConstraints.None;
	}
}
