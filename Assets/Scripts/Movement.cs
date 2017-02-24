﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public static float playerHp;
    public float speed;
    public int artInRoom;
    private int artCollected;
	static public bool cardObtained;
	public GameObject door;
	public GameObject player;

	// Use this for initialization
	void Start () {
		playerHp=1;
        artCollected = 0;
		cardObtained=false;
		player = GameObject.FindWithTag("Player");
		door = GameObject.FindWithTag("secdoor");
		print ("press E to pick up objects");
	}
	public bool cardObtainedget(){
		return(cardObtained);
	}
	void Update(){
		if(cardObtained){
				// code taken in part from unity 3d https://forum.unity3d.com/threads/how-do-you-change-a-color-in-spriterenderer.211003/
				SpriteRenderer renderer = (SpriteRenderer) door.GetComponent<Renderer>();
				renderer.color = new Color(1f,0f,1f,1f);
			BoxCollider2D comp = door.GetComponent("BoxCollider2D") as BoxCollider2D;
			DestroyImmediate(comp);
		}
		if (playerHp==0){
			SpriteRenderer render2 = (SpriteRenderer) player.GetComponent<Renderer>();
			render2.color = new Color(.5f, .2f, 1f, 1f);
			Destroy(gameObject,3);
			print("you have been caught");
		}
	}

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate () {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Actually move the player's icon
        transform.Translate(movement * speed);
    }

    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerStay2D(Collider2D other) {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag("Pickup") && Input.GetKeyDown(KeyCode.E)) {
            other.gameObject.SetActive(false);
            artCollected++;
        }
        if (other.gameObject.CompareTag("Door")) {
            if (artCollected == artInRoom) {
                gameObject.SetActive(false);
			}
		}
		if (other.gameObject.CompareTag("KeyCard") && Input.GetKeyDown(KeyCode.E)){
			cardObtained=true;
			print("card obtained");
			other.gameObject.SetActive(false);
		}
			
    }
}
