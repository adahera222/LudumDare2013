using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotController : MonoBehaviour {
	public List<Transform> pathNodes= new List<Transform>();
	public float movespeed;
	public float sight;
	private float vel;
	private float lastPos;
	private float currentPos;
	private float stopForce;
	private float moveForce;
	private int nodeNum = 0;
	enum States {Patrolling,Stop,Turning,Chase,Return};
	States state = States.Turning;

	// START AND FIXED FUNCTIONS
	void Start () {
	}

	void FixedUpdate() {

		switch(state)
		{
			case States.Patrolling:
				PatrolRobit();
				break;
			case States.Stop:
				StopRobit();
				break;
			case States.Turning:
				TurnRobit();
				break;
			case States.Chase:
				ChasePlayer();
				break;
			case States.Return:
				ReturnRobits();
				break;
		}
	}

	//STATES FOR A ROBITS
	void PatrolRobit() {
		//EDGE CONDIONTION
		float distance = moveVector(pathNodes[nodeNum]).magnitude;
		//float vel = rigidbody.velocity.magnitude;
		//float stopDistance= Mathf.Pow(vel,2f)/(2*movespeed);
		if(distance < 0.35f){
			state = States.Stop;
			//Debug.Log("velocity of stop is :"+rigidbody.velocity);
		}
		else if(seePlayer()) {
			state = States.Chase;
		}
		else {
			//ACTION
			Vector3 moveDir = moveVector(pathNodes[nodeNum]);
			moveDir.Normalize();
			vel = rigidbody.velocity.magnitude;
			//moveForce=Mathf.Pow(movespeed,2f)-Mathf.Pow(velocity,2f)/moveddistance ;
			moveForce = movespeed - vel;
			rigidbody.AddRelativeForce(moveDir * movespeed);
		}
	}

	void StopRobit() {
		//EDGE CONDIONTION
		float distance = moveVector(pathNodes[nodeNum]).magnitude;
		if(distance < 0.1f) {
			state = States.Turning;
			NextNode();
		}
		else if(seePlayer()) {
			state = States.Chase;
		}
		else {
			//ACTION
			Vector3 moveDir = moveVector(pathNodes[nodeNum]);
			moveDir.Normalize();
			vel = rigidbody.velocity.magnitude;
			stopForce = -Mathf.Pow(vel,2f)/(2*Mathf.Abs(distance));
			rigidbody.AddRelativeForce(moveDir * stopForce);
		}
	
	}
	
	void TurnRobit() {
		//EDGE CONDIONTION
		float angle = turnAngle(pathNodes[nodeNum]);
		if(angle < 2f) {
			state = States.Patrolling;
		}
		else if(seePlayer()) {
			state = States.Chase;
		}
		else {
			//ACTION
			transform.RotateAround(transform.position, Vector3.up, 2f);
		}

	}

	// IGNORE FOR NOW!
	void ChasePlayer() {
		Debug.Log("I SEE YOU! :D");
	}

	void ReturnRobits() {
	}

	//HELPING FUNCTIONS FOR THINGS
	void NextNode() {
		if (nodeNum == pathNodes.Count - 1){
			nodeNum = 0;
		}
		else {
			nodeNum++;
		}
	}

	Vector3 moveVector(Transform node) {
		Vector3 moveDir = node.position - transform.position;
		moveDir.y = 0;
		moveDir = transform.InverseTransformDirection(moveDir);
		return moveDir;
	}

	float turnAngle(Transform node) {
		Vector3 turnDir = node.position - transform.position;
		turnDir.y = 0;
		turnDir = transform.InverseTransformDirection(turnDir);
		float turnAngle = Vector3.Angle(Vector3.forward,turnDir);
		return turnAngle;
	}

	bool seePlayer() {
		Vector3 turnDir = GameObject.Find("Player").transform.position - transform.position;
		turnDir.y = 0;
		turnDir = transform.InverseTransformDirection(turnDir);
		sight = Vector3.Angle(Vector3.forward,turnDir);

		if (sight < 15)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
