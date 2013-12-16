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

	void setCheckpoint(GameObject obj) {
		activecheckpoint = obj;
		activerope = activecheckpoint.GetComponentInChildren<RopeSpawner>();
		Debug.Log(activerope.ToString());
		activeplayer = activecheckpoint.GetComponentInChildren<PlayerSpawner>();
		Debug.Log(activeplayer.ToString());
	}
}
