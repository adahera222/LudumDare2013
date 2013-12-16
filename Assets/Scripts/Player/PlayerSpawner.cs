using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public bool spawnAtStart;
	public Transform prefab;

	void Start () {
		if(spawnAtStart) SpawnPlayer();
	}

	public void SpawnPlayer() {
		Instantiate(prefab, transform.position, transform.rotation);
	}
}
