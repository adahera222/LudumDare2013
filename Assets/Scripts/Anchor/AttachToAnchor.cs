using UnityEngine;
using System.Collections;

public class AttachToAnchor : MonoBehaviour {
	public void Attach(GameObject toAttach)
	{
		toAttach.rigidbody.MovePosition(transform.GetChild(0).position);
		FixedJoint f = toAttach.AddComponent<FixedJoint>();
		f.anchor = transform.GetChild(0).position;
	}
}
