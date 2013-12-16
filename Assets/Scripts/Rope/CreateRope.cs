using UnityEngine;
using System.Collections;

public class CreateRope : MonoBehaviour {

	public Vector3 position = new Vector3(0, 0, 0);
	public int ropeLength = 24;
	public float segmentLength = .25f;
	public float ropeThickness = .1f;

	[HideInInspector]
	public GameObject[] ropePieces;

	void Start() {
		Transform debug = new GameObject().transform;
		ropePieces = new GameObject[ropeLength];
		//GameObject start = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		GameObject start = new GameObject();
		CapsuleCollider segment = start.AddComponent<CapsuleCollider>();
		segment.radius = ropeThickness;
		segment.direction = 2;
		segment.height = segmentLength;
		start.transform.position = position;
		Rigidbody body = start.AddComponent<Rigidbody>();
		body.mass = Mathf.Pow(ropeLength,-1);
		body.collisionDetectionMode = CollisionDetectionMode.Continuous;
		start.collider.material = (PhysicMaterial) Resources.Load("Physics/Rope");
		start.layer = 8;

		ropePieces[0] = start;
		GameObject newRope;
		for(int i = 1; i < ropeLength; i++) {
			start = ropePieces[i-1];
			//newRope = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			newRope = new GameObject();
			segment = newRope.AddComponent<CapsuleCollider>();
			segment.radius = ropeThickness;
			segment.direction = 2;
			segment.height = segmentLength;
			newRope.transform.position = new Vector3(position.x, position.y, position.z+i*(segmentLength));


			//incoming magic layer number
			newRope.layer = 8;
			body = newRope.AddComponent<Rigidbody>();
		    body.mass = Mathf.Pow(ropeLength,-1);
			body.collisionDetectionMode = CollisionDetectionMode.Continuous;
			newRope.collider.material = (PhysicMaterial) Resources.Load("Physics/Rope");

			ConfigurableJoint j = newRope.AddComponent<ConfigurableJoint>();
			j.connectedBody = start.rigidbody;
			j.connectedAnchor = new Vector3(0,0, segment.height/2);
			j.anchor = new Vector3(0,0, -segment.height/2);

			SoftJointLimit limit = new SoftJointLimit();
			limit.damper = 50;
			limit.limit = segmentSeperation;
			limit.spring = 100;

			j.linearLimit = limit;
			j.xMotion = ConfigurableJointMotion.Limited;
			j.yMotion = ConfigurableJointMotion.Limited;
			j.zMotion = ConfigurableJointMotion.Limited;

			ropePieces[i] = newRope;
		}
		for(int i = 0; i < ropeLength; i++) {
			ropePieces[i].transform.parent = debug;
			GameObject child = new GameObject();
			child.transform.parent = ropePieces[i].transform;
			child.transform.localPosition = Vector3.zero;
			CapsuleCollider cap = child.AddComponent<CapsuleCollider>();
			cap.center = Vector3.zero;
			cap.radius = ropeThickness;
			cap.height = segmentLength * 1.25f;
			cap.direction = 2;
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
				new Vector3(0,0, (segmentLength)/2)) - 
			    b.transform.TransformPoint(
				new Vector3(0,0, -(segmentLength)/2));

			a.rigidbody.AddForceAtPosition(
				-distance.normalized * Mathf.Pow(distance.magnitude, 3)*30,
			    a.transform.TransformPoint(new Vector3(0,0, (segmentLength)/2)));

			b.rigidbody.AddForceAtPosition(
				distance.normalized * Mathf.Pow(distance.magnitude, 3)*30,
			    b.transform.TransformPoint(new Vector3(0,0, -(segmentLength)/2)));
		}
	}

	public GameObject getRopeSegment(GameObject inRope)
	{
		for(int i = 1; i < ropePieces.Length/2; i++)
		{
			if(ropePieces[i].Equals (inRope))
			{
				Debug.Log ("returning front");
					return ropePieces[0];
			}

		}
		Debug.Log("returning end");
		return ropePieces[ropePieces.Length-1];
	}
}
