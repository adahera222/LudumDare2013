using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

	public RopeSpawner activerope;

	void Start () {
		activerope = GameObject.Find("Spawner").GetComponent<RopeSpawner>();
	}
	
	void Update () {
	
	}
}
