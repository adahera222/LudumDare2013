using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public bool spawnAtStart;
	public Transform prefab;

	void Start () {
		if(spawnAtStart) SpawnPlayer();
	}

	void SpawnPlayer() {
		Instantiate(prefab, transform.position, transform.rotation);
	}

	void setActive() {

	}
}
