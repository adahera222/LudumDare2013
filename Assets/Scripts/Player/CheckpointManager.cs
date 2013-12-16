using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

	[HideInInspector]
	public RopeSpawner activerope;

	void Start () {
		activerope = GameObject.Find("Spawner").GetComponent<RopeSpawner>();
	}
	
	void Update () {
	
	}
}
