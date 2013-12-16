using UnityEngine;
using System.Collections;

public class RopeSpawner : MonoBehaviour {

	public GameObject rope;

	void Start() {
		SpawnRope();
	}

	void SpawnRope() {
		rope = new GameObject();
		CreateRope ropecomponent = rope.AddComponent<CreateRope>();
		ropecomponent.position = transform.position;
		ropecomponent.ropeLength = 20;
		ropecomponent.segmentLength = 0.25f;
		ropecomponent.ropeThickness = 0.1f;

		RopeRenderer vis = rope.AddComponent<RopeRenderer>();
		vis.RopeWidth = 0.1f;
	}
}
