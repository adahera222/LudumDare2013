using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class RobotManager : MonoBehaviour {

	public Transform prefab;
	public Transform instance;
	public List<Transform> nodes;

	void Update () {
		if(instance) return;

		instance = Instantiate(prefab, transform.position, transform.rotation) as Transform;
		instance.transform.parent = transform;
		instance.GetComponent<RobotController>().pathNodes = nodes;
	}
}
