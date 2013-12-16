using UnityEngine;
using UnityEditor;
using System.Collections;

public class RopeSpawner : MonoBehaviour {

	public bool spawnAtStart;
	public GameObject rope;
	public Material mat;

	void Start() {
		if (spawnAtStart) SpawnRope();
	}

	public void SpawnRope() {
		if(GameObject.Find("Rope Manager")) {
			Debug.Log("Rope already exists");
			return;
		}
		
		rope = new GameObject("Rope Manager");
		CreateRope ropecomponent = rope.AddComponent<CreateRope>();
		ropecomponent.position = transform.position;
		ropecomponent.ropeLength = 20;
		ropecomponent.segmentLength = 0.2f;
		ropecomponent.ropeThickness = 0.1f;

		RopeRenderer vis = rope.AddComponent<RopeRenderer>();
		vis.RopeWidth = 0.1f;
		vis.mat = mat;
	}
}
