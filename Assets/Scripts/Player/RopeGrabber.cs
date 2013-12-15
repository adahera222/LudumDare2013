using UnityEngine;
using System.Collections;

public class RopeGrabber : MonoBehaviour {

	public Vector3 positionToAfix = new Vector3(1f, 0, 1f);
	public float arbitraryDistanceLimit = 4;
	public Camera thisCamera;

	private GameObject cursorBullshit;
	private SpringJoint jointDicks;

	// Use this for initialization
	void Awake () {
		Screen.lockCursor = true;
		cursorBullshit = new GameObject();
		cursorBullshit.name = "cursorBullshit";
		Rigidbody b = cursorBullshit.AddComponent<Rigidbody>();
		b.isKinematic = true;
		cursorBullshit.layer = 10;
		GameObject cursorRender = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cursorRender.collider.enabled=false;
		cursorRender.transform.parent = cursorBullshit.transform;
		cursorRender.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
		cursorRender.transform.localPosition = Vector3.zero;
		cursorBullshit.transform.parent = thisCamera.transform;
		cursorBullshit.transform.localPosition = positionToAfix;
		jointDicks = null;
	}
	
	// Update is called once per frame
	void Update () {
		Screen.showCursor = false;
		if(Input.GetMouseButtonDown(0) && jointDicks == null) {
			Ray ray = thisCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast (ray,out hit))
			{
				if(hit.collider.gameObject.layer == 9 &&
				   (hit.collider.gameObject.transform.position - thisCamera.transform.position).magnitude < arbitraryDistanceLimit)
				{
					Debug.Log ("object connected");
					jointDicks = hit.collider.transform.parent.gameObject.AddComponent<SpringJoint>();
					jointDicks.connectedBody = cursorBullshit.rigidbody;
					jointDicks.anchor = new Vector3 (0,0,0);
					jointDicks.spring = 9;
					jointDicks.damper = .1f;
					jointDicks.minDistance = 0;
					jointDicks.maxDistance = .3f;
					jointDicks.breakForce = 10000f;
					jointDicks.breakTorque = 10000f;
				}
			}
			if(jointDicks != null)
				Debug.DrawLine(jointDicks.transform.position, jointDicks.connectedBody.transform.position);
		}
	}
}
