﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

	private Transform currentPatrolPoint;
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

	void FixedUpdate () {
		currentPatrolPoint = (Transform) patrolPointsList[indexforPoints]; 
		Ai_movement guardMovement = gameObject.GetComponent<Ai_movement>();
		float offsetX = (transform.position.x - currentPatrolPoint.position.x);
		float offsetY = (transform.position.y - currentPatrolPoint.position.y);
		float distance = Mathf.Sqrt(Mathf.Pow(offsetX, 2) + Mathf.Pow(offsetY, 2));

//		print(script.chase);
		if (guardMovement.chase == false) { //guard does not chase the player --need to be changed
			DefaultMovement (offsetX, offsetY, distance);
		} else if (guardMovement.chase == true){ 
			guardMovement.fullChaseMethodForEZUse();
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

	//Guard follows the path to the current patrolPoint
	void DefaultMovement(float x, float y, float d) {
		Vector2 unitVector = new Vector2 (x / d, y / d);
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


}
