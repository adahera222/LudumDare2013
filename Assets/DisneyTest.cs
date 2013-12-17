using UnityEngine;
using UnityEditor;
using System.Collections;

public class DisneyTest : MonoBehaviour {

	public bool thisterriblegameisover = false;

	void Start () {
	
	}

	void Update () {
	
	}

	void OnTriggerExit(Collider other) {
		if(other.name == "disney") thisterriblegameisover = true;
	}
}
