using UnityEngine;
using System.Collections;

public class CreateRope : MonoBehaviour {

	public Vector3 position = new Vector3(0, 0, 0);
	public int ropeLength = 24;
	public float segmentLength = .25f;
	public float ropeThickness = .1f;

	private bool frontAttached;
	private bool backAttached;

	[HideInInspector]
	public GameObject[] ropePieces;


	void Start() {
		Transform debug = new GameObject("Rope Container").transform;
		ropePieces = new GameObject[ropeLength];
		//GameObject start = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		GameObject start = new GameObject("Rope Segment");
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
			newRope = new GameObject("Rope Segment");
			segment = newRope.AddComponent<CapsuleCollider>();
			segment.radius = ropeThickness;
			segment.direction = 2;
			segment.height = segmentLength;
			newRope.transform.position = new Vector3(position.x, position.y, position.z+i*(segmentLength)); //tiny spawn


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
			limit.spring = 100;

			j.linearLimit = limit;
			j.xMotion = ConfigurableJointMotion.Limited;
			j.yMotion = ConfigurableJointMotion.Limited;
			j.zMotion = ConfigurableJointMotion.Limited;

			ropePieces[i] = newRope;
		}
		for(int i = 0; i < ropeLength; i++) {
			ropePieces[i].transform.parent = debug;
			GameObject child = new GameObject("Grab Collider");
			child.transform.parent = ropePieces[i].transform;
			child.transform.localPosition = Vector3.zero;
			CapsuleCollider cap = child.AddComponent<CapsuleCollider>();
			cap.center = Vector3.zero;
			cap.radius = ropeThickness * 2;
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

	//only for use when attaching, or things might break
	public GameObject getRopeSegment(GameObject inRope)
	{
		Debug.Log ("front " + frontAttached);
		Debug.Log ("back " + backAttached);
		float distEnd = (inRope.transform.position - ropePieces[ropePieces.Length - 1].transform.position).magnitude;
		float distBeg = (inRope.transform.position - ropePieces[0].transform.position).magnitude;
		if(distEnd > 4 && distBeg > 4)
			return null;
		if(distEnd >distBeg )
		{
			if(frontAttached == false)
			{
				frontAttached = true;
				return ropePieces[0];
			}
			else
				return null;
		}
		if(backAttached == false)
		{
			backAttached = true;
			return ropePieces[ropePieces.Length - 1];
		}
		return null;
	}

	public void release(GameObject o)
	{
		if(o.Equals(ropePieces[0]))
		   frontAttached = false;
		if(o.Equals (ropePieces[ropePieces.Length - 1]))
		   backAttached = false;
	}
}
