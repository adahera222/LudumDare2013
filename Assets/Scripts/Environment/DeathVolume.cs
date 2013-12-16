using UnityEngine;
using System.Collections;

public class DeathVolume : MonoBehaviour {

	void OnTriggerEnter(Collider victim) {
		if(victim.gameObject.layer == 8) {
			Debug.Log ("caught a rope");
			GameObject.Find("Player(Clone)").GetComponent<CheckpointManager>().activerope.rope.GetComponent<CreateRope>().Die();
		} else {
			victim.SendMessage("Die");
			Debug.Log("caught something");
		}
	}
}
