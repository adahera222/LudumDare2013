using UnityEngine;
using System.Collections;

public class DeathVolume : MonoBehaviour {

	void OnTriggerEnter(Collider victim) {
		if(victim.gameObject.layer == 8) {
			Debug.Log ("caught a rope");
			GameObject.Find("Player(Clone)").GetComponent<CheckpointManager>().ResetRope();
		} else {
			GameObject.Find("Player(Clone)").GetComponent<CheckpointManager>().ResetPlayer();
			Debug.Log("caught something");
		}
	}
}
