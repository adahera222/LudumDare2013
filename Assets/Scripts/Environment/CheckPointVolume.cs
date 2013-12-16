using UnityEngine;
using System.Collections;

public class CheckPointVolume : MonoBehaviour {

	public bool passed = false;

	void OnTriggerEnter (Collider other) {
		if(other.gameObject.layer != 8) return;

		passed = true;
		GameObject.Find("Player(Clone)").GetComponent<CheckpointManager>().activecheckpoint = gameObject;
	}
}
