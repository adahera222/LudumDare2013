﻿using UnityEngine;
using System.Collections;

public class RopeGrabber : MonoBehaviour {

	public Vector3 positionToAfix = new Vector3(1f, 0, 1f);
	public float arbitraryDistanceLimit = 6;
	public float arbitraryAttachDistance = 6;
	public Camera thisCamera;

	[HideInInspector]
	public bool isGrabbing;

	private GameObject cursorBullshit;
	private SpringJoint jointDicks;
	private FixedJoint frontJoint;

	void Awake () {
		Screen.lockCursor = true;
		cursorBullshit = new GameObject();
		cursorBullshit.name = "cursorBullshit";
		Rigidbody b = cursorBullshit.AddComponent<Rigidbody>();
		b.useGravity=false;
		b.isKinematic=true;
		cursorBullshit.layer = 10;
		cursorBullshit.transform.parent = thisCamera.transform;
		cursorBullshit.transform.localPosition = positionToAfix;
		jointDicks = null;

		isGrabbing = false;
	}

	void Update () {
		Screen.showCursor = false;
		if(Input.GetMouseButtonDown(0) && jointDicks == null)
		{
			Ray ray = thisCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2,0 ));
			RaycastHit hit;
			if(Physics.Raycast (ray,out hit))
			{
				if(hit.collider.gameObject.layer == 9 && hit.distance < arbitraryDistanceLimit)
				{
					jointDicks = hit.collider.transform.parent.gameObject.AddComponent<SpringJoint>();
					jointDicks.connectedBody = cursorBullshit.rigidbody;
					jointDicks.anchor = new Vector3 (0,0,0);
					jointDicks.spring = 9;
					jointDicks.damper = .1f;
					jointDicks.minDistance = 0;
					jointDicks.maxDistance = .3f;
					jointDicks.breakForce = 10000f;
					jointDicks.breakTorque = 10000f;

					isGrabbing = true;
				}
			}
		}
		if(jointDicks != null && Input.GetMouseButtonUp (0))
		{
			Destroy(jointDicks);
			jointDicks = null;

			isGrabbing = false;
		}
		if(jointDicks != null && Input.GetMouseButtonDown(1))
		{
			Ray ray = thisCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
			RaycastHit hit;
			if(Physics.Raycast (ray,out hit))
			{
				if(hit.collider.gameObject.layer == 13 && hit.distance < arbitraryAttachDistance)
				{
					jointDicks.connectedBody = null;
					GameObject g = GameObject.Find("Rope Manager").GetComponent<CreateRope>().getRopeSegment(jointDicks.gameObject);
					if(g != null)
					{
						hit.collider.gameObject.SendMessage("Attach",g);
						Destroy (jointDicks);
						jointDicks = null;
					}
				}
			}
		} else
		if(Input.GetMouseButtonDown(1))
		{
			Ray ray = thisCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
			RaycastHit hit;
			if(Physics.Raycast (ray,out hit))
			{
				if(hit.collider.gameObject.layer == 13 && hit.distance < arbitraryAttachDistance)
				{
					hit.collider.gameObject.SendMessage("Detach");
				}
			}
		}
	}
}
