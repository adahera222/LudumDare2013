using UnityEngine;
using System.Collections;


public class AttachToAnchor : MonoBehaviour {

	[HideInInspector]
	public bool attached = false;
	public GameObject attachedObject;
	public FixedJoint f;

	public bool breakjoint = false;

	public void Attach(GameObject toAttach)
	{
		if(!attached)
		{
			toAttach.rigidbody.MovePosition(transform.FindChild("target").position);
			attachedObject = toAttach;
			StartCoroutine(waitforattach());
		}
	}

	IEnumerator waitforattach() {
		yield return new WaitForSeconds(0.1f);
		f = gameObject.AddComponent<FixedJoint>();
		f.connectedBody = attachedObject.rigidbody;
		attached = true;

		if(breakjoint) f.breakForce=2000f;
	}

	public void Detach()
	{
		attached = false;
		Destroy (f);
		f = null;
		GameObject.Find("Rope Manager").GetComponent<CreateRope>().release(attachedObject);
		attachedObject = null;
	}
}
