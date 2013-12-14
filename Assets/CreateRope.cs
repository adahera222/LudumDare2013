using UnityEngine;
using System.Collections;

public class CreateRope : MonoBehaviour {

	public Vector3 position = new Vector3(0, 0, 0);
	public int ropeLength = 5;
	public float segmentLengthScale = .4f;
	public float ropeThicknessScale = .2f;
	public float elasticity = 1;
	public float segmentSeperation = .05f;

	private GameObject[] ropePieces;

	// Use this for initialization
	/*void Start () {
		GameObject start = GameObject.CreatePrimitive(PrimitiveType.Cube);
		start.transform.position = position;
		start.transform.localScale = new Vector3(ropeThicknessScale,ropeThicknessScale,segmentLengthScale);
		start.AddComponent<Rigidbody>().isKinematic = true;
		//magic layer number ahead
		start.layer = 8;
		GameObject newRope;
		for(int i = 1; i < ropeLength; i++)
		{
			newRope = GameObject.CreatePrimitive(PrimitiveType.Cube);
			newRope.transform.localScale = new Vector3(ropeThicknessScale,ropeThicknessScale,segmentLengthScale);
			newRope.transform.position = new Vector3(position.x, position.y, position.z+i*(segmentLengthScale+segmentSeperation/2));
			//incoming magic layer number
			newRope.layer = 8;
			Rigidbody newRigidBody = newRope.AddComponent<Rigidbody>();
			CharacterJoint joint = start.AddComponent<CharacterJoint>();
			joint.connectedBody = newRigidBody;
			joint.anchor = new Vector3(0,0,segmentLengthScale+segmentSeperation);
			joint.connectedAnchor = new Vector3(0,0, -segmentLengthScale-segmentSeperation/2);
			start = newRope;
		}
	}*/

	void Start() {
		ropePieces = new GameObject[ropeLength];
		GameObject start = GameObject.CreatePrimitive(PrimitiveType.Cube);
		start.transform.position = position;
		start.transform.localScale = new Vector3(ropeThicknessScale,ropeThicknessScale,segmentLengthScale);
		start.AddComponent<Rigidbody>().isKinematic = true;
		start.layer = 8;
		ropePieces[0] = start;
		GameObject newRope;
		for(int i = 1; i < ropeLength; i++) {
			newRope = GameObject.CreatePrimitive(PrimitiveType.Cube);
			newRope.transform.localScale = new Vector3(ropeThicknessScale,ropeThicknessScale,segmentLengthScale);
			newRope.transform.position = new Vector3(position.x, position.y, position.z+i*(segmentLengthScale+segmentSeperation));
			//incoming magic layer number
			newRope.layer = 8;
			newRope.AddComponent<Rigidbody>();
			ropePieces[i] = newRope;
		}
	}

	void FixedUpdate() {
		GameObject  a;
		GameObject b;
		for(int i = 1; i < ropePieces.Length; i++) {
			a = ropePieces[i-1];
			b = ropePieces[i];
			Vector3 distance = a.transform.TransformPoint(
				new Vector3(0,0, (segmentLengthScale+segmentSeperation)/2)) - 
			    b.transform.TransformPoint(
				new Vector3(0,0, -(segmentLengthScale+segmentSeperation)/2));
			Debug.Log (distance.normalized * Mathf.Sqrt((distance.magnitude/2)));
			a.rigidbody.AddForceAtPosition(
				-distance.normalized * Mathf.Pow(distance.magnitude, 2),
			    new Vector3(0,0, (segmentLengthScale+segmentSeperation)/2));
			b.rigidbody.AddForceAtPosition(
				distance.normalized * Mathf.Pow(distance.magnitude,2),
			    new Vector3(0,0, -(segmentLengthScale+segmentSeperation)/2));
		}
	}
}
