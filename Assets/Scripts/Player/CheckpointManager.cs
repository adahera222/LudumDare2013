using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

	[HideInInspector]
	public RopeSpawner activerope;

	[HideInInspector]
	public PlayerSpawner activeplayer;

	[HideInInspector]
	public GameObject activecheckpoint;

	void Awake () {
		setCheckpoint(GameObject.Find("TestSpawner"));
	}

	public void setCheckpoint(GameObject obj) {
		activecheckpoint = obj;
		activerope = activecheckpoint.GetComponentInChildren<RopeSpawner>();
		activeplayer = activecheckpoint.GetComponentInChildren<PlayerSpawner>();
	}

	public void ResetRope() {
		StartCoroutine("TimedResetRope");
	}

	IEnumerator TimedResetRope() {
		Destroy(GameObject.Find("Rope Manager"));
		Destroy(GameObject.Find("Rope Container"));

		yield return new WaitForSeconds(1f);

		activerope.SpawnRope();
	}

	public void ResetPlayer() {
		gameObject.transform.position = activeplayer.transform.position;
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.R)) ResetRope();
	}
}
