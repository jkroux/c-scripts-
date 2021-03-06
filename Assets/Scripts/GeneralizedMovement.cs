﻿using System.Collections;
using UnityEngine;

public class GeneralizedMovement : MonoBehaviour {
	
	public float regularSpeed = 0.05f;
	public Transform Pos1;
	public Transform Pos2;
	public Transform Pos3;
	public Transform Pos4;
	public Transform Pos5;
	public Transform Pos6;
	public Transform Pos7;
	public int numOfPatrolPoints;

	private ArrayList patrolPointsList = new ArrayList();
	private int indexforPoints = 0;



	// Use this for initialization
	void Start () {
		patrolPointsList.Add (Pos1);
		patrolPointsList.Add (Pos2);
		patrolPointsList.Add (Pos3);
		patrolPointsList.Add (Pos4);
		patrolPointsList.Add (Pos5);
		patrolPointsList.Add (Pos6);
		patrolPointsList.Add (Pos7);
	}

	void Update(){
	}

	void FixedUpdate () {
		Sight vision = gameObject.GetComponent<Sight>();
		bool chase = vision.GetChase ();
		if (!chase) { 
			DefaultMovement ();
			} 
		}


	//Guard follows the path to the current patrolPoint

	void DefaultMovement() {	
		Transform currentPatrolPoint = (Transform) patrolPointsList[indexforPoints]; 
		float offsetX = (transform.position.x - currentPatrolPoint.position.x);
		float offsetY = (transform.position.y - currentPatrolPoint.position.y);
		float distance = Mathf.Sqrt(Mathf.Pow(offsetX, 2) + Mathf.Pow(offsetY, 2));

		Vector2 unitVector = new Vector2 (offsetX / distance, offsetY / distance);
		transform.Translate(unitVector * -regularSpeed);

		bool arriveX = Arrive(transform.position.x, currentPatrolPoint.position.x);
		bool arriveY = Arrive (transform.position.y, currentPatrolPoint.position.y);
		if (arriveX && arriveY) {
			if (indexforPoints <= (numOfPatrolPoints-1)) {
				indexforPoints++;
			} 
			else {indexforPoints = 0;}
		}
	}

	//See if the guard get to the current patrolPoint
	bool Arrive(float x, float pointX) {
		if ((pointX - .5) < x && x < (pointX + .5)) {
			return true;
		} else {
			return false;
		}
	}


}
