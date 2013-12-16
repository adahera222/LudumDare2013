using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public bool spawnAtStart;
	public Transform prefab;

	void Start () {
		//dont need spawn points rn
	}

	void SpawnPlayer() {
		Instantiate(prefab, Vector3.zero, Quaternion.identity);
	}
}
