using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	Transform cameraref;

	void Start () {
		cameraref = transform.GetChild(0);
	}

	void Update () {
		transform.Rotate(new Vector3( 0, Input.GetAxis("Mouse X"), 0));
		cameraref.Rotate(new Vector3( -Input.GetAxis("Mouse Y"), 0, 0));
		if(Input.GetKey(KeyCode.LeftControl))
			cameraref.localPosition = new Vector3(0f, 0.2f, 0f);
		else
			cameraref.localPosition = new Vector3(0f, 0.5f, 0f);
	}
}
