using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotController : MonoBehaviour {
	public List<Transform> pathNodes= new List<Transform>();
	public float movespeed;
	private float stopSpeed;
	private int nodeNum = 0;
	bool playerDetected= false;
	enum States {Patrolling,Stop,Turning,Chase,Return};
	States state = States.Turning;

	// START AND FIXED FUNCTIONS
	void Start () {
		stopSpeed = movespeed;
	}

	void FixedUpdate(){

		//PatrolRobit();
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
	void PatrolRobit(){
		//Debug.Log("Made it to Patrol");
		//EDGE CONDIONTION
		float distance = moveVector(pathNodes[nodeNum]).magnitude;
		if(distance < 1f){
			state = States.Stop;
		}
		else if(playerDetected){
			state = States.Chase;
		}
		else{
			//ACTION
			Vector3 moveDir = moveVector(pathNodes[nodeNum]);
			moveDir.Normalize();
			stopSpeed = movespeed;
			rigidbody.AddRelativeForce(moveDir * movespeed);
		}
	}

	void StopRobit(){
		//Debug.Log("Made it to Stop");
		//EDGE CONDIONTION
		float distance = moveVector(pathNodes[nodeNum]).magnitude;
		if(distance < 0.5f){
			state = States.Turning;
			NextNode();
		}
		else if(playerDetected){
			state = States.Chase;
		}
		else{
			//ACTION
			Vector3 moveDir = moveVector(pathNodes[nodeNum]);
			moveDir.Normalize();
			stopSpeed = stopSpeed / 1.05f;
			rigidbody.AddRelativeForce(moveDir * stopSpeed);
		}
	
	}
	
	void TurnRobit(){
		//Debug.Log("Made it to Turn");
		//EDGE CONDIONTION
		float angle= turnAngle(pathNodes[nodeNum]);
		//Debug.Log("angle is " + angle);
		if(angle < 2f){
			state = States.Patrolling;
		}
		else if(playerDetected){
			state = States.Chase;
		}
		else{
			//ACTION
			transform.RotateAround(transform.position, Vector3.up, 2f);
		}

	}

	// IGNORE FOR NOW!
	void ChasePlayer(){
	}

	void ReturnRobits(){
	}

	//HELPING FUNCTIONS FOR THINGS
	void NextNode(){
		if (nodeNum == pathNodes.Count-1){
			nodeNum = 0;
		}
		else{
			nodeNum++;
		}
	}

	Vector3 moveVector(Transform node){
		Vector3 moveDir=node.position-transform.position;
		moveDir.y = 0;
		moveDir = transform.InverseTransformDirection(moveDir);
		return moveDir;
	}

	float turnAngle(Transform node){
		Vector3 turnDir=node.position-transform.position;
		turnDir.y = 0;
		turnDir = transform.InverseTransformDirection(turnDir);
		float turnAngle = Vector3.Angle(Vector3.forward,turnDir);
		return turnAngle;
	}
}
