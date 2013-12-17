using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotController : MonoBehaviour {
	public List<Transform> pathNodes= new List<Transform>();
	public float movespeed;
	private float sight;
	private float vel;
	private Vector3 lastPos;
	private float currentPos;
	private float stopForce;
	private float moveForce;
	private int nodeNum = 0;
	private bool sendMsg = true;
	private int angleHelper=1;
	//private float chaseTime = 0;
	
	bool animate = true;
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
//			case States.Chase:
//				ChasePlayer();
//				break;
//			case States.Return:
//				ReturnRobits();
//				break;
		}
	}

	//STATES FOR A ROBITS
	void PatrolRobit() {

		//EDGE CONDIONTION
		float distance = moveVector(pathNodes[nodeNum]).magnitude;
		if(distance<1.2f&&animate){
			animation.Play("Stop Moving");
			animate=false;
		}
		if(distance < 0.2f){
			state = States.Stop;
			animate=true;
		}
//		else if(detectPlayer()) {
//			state = States.Chase;
//		}
		else {
			//ACTION
			Vector3 moveDir = moveVector(pathNodes[nodeNum]);
			moveDir.Normalize();
			vel = rigidbody.velocity.magnitude;
			moveForce = movespeed - vel;
			rigidbody.AddRelativeForce(moveDir * moveForce);
		}
	}

	void StopRobit() {
		//EDGE CONDIONTION
		float distance = moveVector(pathNodes[nodeNum]).magnitude;
		if(distance < 0.1f) {
			//animation.Play("Start Moving");
			state = States.Turning;
			NextNode();
		}
//		else if(detectPlayer()) {
//			state = States.Chase;
//		}
		else {
			//ACTION
			Vector3 moveDir = moveVector(pathNodes[nodeNum]);
			moveDir.Normalize();
			vel = rigidbody.velocity.magnitude;
			stopForce = -Mathf.Pow(vel, 2f) / (2 * Mathf.Abs(distance));
			rigidbody.AddRelativeForce(moveDir * stopForce);
		}
	
	}
	
	void TurnRobit() {
		//EDGE CONDIONTION
		float angle = turnAngle(pathNodes[nodeNum]);
		if(angle < 2f) {
			animation.Play("Start Moving");
			state = States.Patrolling;
		}
//		else if(detectPlayer()) {
//			state = States.Chase;
//		}
		else {
			//ACTION
			transform.RotateAround(transform.position, Vector3.up, angleHelper*2f);
		}

	}

	// IGNORE FOR NOW!
//	void ChasePlayer() {
//
//		float distance = moveVector(GameObject.Find("Player(Clone)").transform).magnitude;
//		float angle = turnAngle(GameObject.Find("Player(Clone)").transform);
//
//		if(Mathf.Abs(angle) < 45 && distance < 8){
//			// Moving
//			Vector3 moveDir = moveVector(GameObject.Find("Player(Clone)").transform);
//			moveDir.Normalize();
//			vel = rigidbody.velocity.magnitude;
//			moveForce = movespeed - vel;
//			rigidbody.AddRelativeForce(moveDir * moveForce);
//			lastPos = GameObject.Find("Player(Clone)").transform.position;
//			transform.RotateAround(transform.position, Vector3.up, angleHelper*2f);
//		}
//		else{
//			Debug.Log("I GOT HERE! :(");
//			state = States.Return;
//		}
//
//		// Chasing the player
//
//
//		// If robot catches player, then it explodes
//		if (moveVector(GameObject.Find("Player(Clone)").transform).magnitude < 1f ) {
//			Explode();
//		}
//	}

//	void Explode() {
//		if (sendMsg) {
//			sendMsg=false;
//			var hitColliders = Physics.OverlapSphere(transform.position, 1f);
//			for (var i = 0; i < hitColliders.Length; i++) {
//				if (hitColliders[i] != gameObject) {
//					hitColliders[i].SendMessage("Explode");
//				}
//			}
//		}
//		Destroy(gameObject);
//	}

//	void ReturnRobits() {
//		Explode();
//	}

	//HELPING FUNCTIONS FOR THINGS
	void NextNode() {
		if (nodeNum == pathNodes.Count - 1) {
			nodeNum = 0;
		}
		else {
			nodeNum++;
		}
	}

	Vector3 moveVector(Transform node) {
		Vector3 dir = node.position - transform.position;
		dir.y = 0;
		dir = transform.InverseTransformDirection(dir);
		return dir;
	}

	void setAngleHelper(Transform node){
		Vector3 turnDir = node.position - transform.position;
		turnDir.y = 0;
		turnDir = transform.InverseTransformDirection(turnDir);
		turnDir.Normalize();

		Vector3 cross=Vector3.Cross(Vector3.left,turnDir);
		if (cross.y < 0){
			angleHelper=-1;
		}
		else{
			angleHelper=1;
		}

	}

	float turnAngle(Transform node) {
		setAngleHelper(node);
		Vector3 turnDir = node.position - transform.position;
		turnDir.y = 0;
		turnDir = transform.InverseTransformDirection(turnDir);
		float turnAngle = Vector3.Angle(Vector3.left,turnDir);
		return turnAngle;
	}

	bool detectPlayer(){
		Vector3 turnDir = GameObject.Find("Player(Clone)").transform.position - transform.position;
		turnDir.y = 0;
		sight = Vector3.Angle(Vector3.left, transform.InverseTransformDirection(turnDir));
		
		RaycastHit hit;
		if(Physics.Linecast(transform.position,GameObject.Find("Player(Clone)").transform.position, out hit))
		{
			if ((Mathf.Abs(sight) < 15) && hit.collider.gameObject.name == "Player(Clone)")
			{
				return true;
			}
		}
		return false;
	}
}
