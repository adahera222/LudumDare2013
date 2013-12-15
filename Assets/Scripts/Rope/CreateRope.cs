using UnityEngine;
using System.Collections;

public class CreateRope : MonoBehaviour {

	public Vector3 position = new Vector3(0, 0, 0);
	public int ropeLength = 24;
	public float segmentLengthScale = .25f;
	public float ropeThicknessScale = .05f;
	public float segmentSeperation = 0f;


	private GameObject[] ropePieces;

	void Start() {
		ropePieces = new GameObject[ropeLength];
		GameObject start = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		start.transform.position = position;
		start.transform.localScale = new Vector3(ropeThicknessScale,ropeThicknessScale,segmentLengthScale);
		Rigidbody body = start.AddComponent<Rigidbody>();
		body.mass = Mathf.Pow(ropeLength,-1);
		body.collisionDetectionMode = CollisionDetectionMode.Continuous;
		start.collider.material = (PhysicMaterial) Resources.Load("Physics/Rope");
		start.layer = 8;

		ropePieces[0] = start;
		GameObject newRope;
		for(int i = 1; i < ropeLength; i++) {
			newRope = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			newRope.transform.localScale = new Vector3(ropeThicknessScale,ropeThicknessScale,segmentLengthScale);
			newRope.transform.position = new Vector3(position.x, position.y, position.z+i*(segmentLengthScale));

			//incoming magic layer number
			newRope.layer = 8;
			body = newRope.AddComponent<Rigidbody>();
		    body.mass = Mathf.Pow(ropeLength,-1);
			body.collisionDetectionMode = CollisionDetectionMode.Continuous;
			newRope.collider.material = (PhysicMaterial) Resources.Load("Physics/Rope");

			ConfigurableJoint j = newRope.AddComponent<ConfigurableJoint>();
			j.connectedBody = start.rigidbody;
			j.connectedAnchor = new Vector3(0,0, -(segmentLengthScale)/2);
			j.anchor = new Vector3(0,0, (segmentLengthScale)/2);

			SoftJointLimit limit = new SoftJointLimit();
			limit.damper = 50;
			limit.limit = segmentSeperation;
			limit.spring = 100;

			j.linearLimit = limit;
			j.xMotion = ConfigurableJointMotion.Limited;
			j.yMotion = ConfigurableJointMotion.Limited;
			j.zMotion = ConfigurableJointMotion.Limited;
			start = newRope;

			ropePieces[i] = newRope;
		}
		for(int i = 0; i < ropeLength; i++) {
			GameObject child = new GameObject();
			child.transform.parent = ropePieces[i].transform;
			SphereCollider cap = child.AddComponent<SphereCollider>();
			cap.center = ropePieces[i].transform.position;
			cap.radius = segmentLengthScale * 1.1f;
			child.layer = 9;
		}
	}

	void FixedUpdate() {
		GameObject  a;
		GameObject b;

		for(int i = 1; i < ropePieces.Length; i++) {
			a = ropePieces[i-1];
			b = ropePieces[i];
			Vector3 distance = a.transform.TransformPoint(
				new Vector3(0,0, (segmentLengthScale)/2)) - 
			    b.transform.TransformPoint(
				new Vector3(0,0, -(segmentLengthScale)/2));

			a.rigidbody.AddForceAtPosition(
				-distance.normalized * Mathf.Pow(distance.magnitude, 3)*30,
			    a.transform.TransformPoint(new Vector3(0,0, (segmentLengthScale)/2)));

			b.rigidbody.AddForceAtPosition(
				distance.normalized * Mathf.Pow(distance.magnitude, 3)*30,
			    b.transform.TransformPoint(new Vector3(0,0, -(segmentLengthScale)/2)));
		}
	}
}
