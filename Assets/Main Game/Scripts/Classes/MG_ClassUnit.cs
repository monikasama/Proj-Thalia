﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassUnit {

	public GameObject sprite;
	public string type, facing, moveType, obstacleType;
	public float facingAngle;

	public int owner;
	public Rigidbody2D rigidBody;

	// MAIN STATS
	public int HP, MP, HPmax, MPmax;
	public float moveSpeed;
	public float posX, posY;
	public int id, entranceId;

	// DEATH
	public bool isDead = false;
	public float removalTime = 25f;

	////////////////// STATUS VARIABLES //////////////////
	public string state;		// Different list of states can be seen at Documentation / MG - Unit States.txt
	////////////////// STATUS VARIABLES //////////////////

	public MG_ClassUnit(GameObject newSprite, string newType, int newID, float newPosX, float newPosY, int newOwner){
		sprite = newSprite;
		sprite.transform.position = new Vector3 (newPosX, newPosY, newPosY - 3);
		posX = newPosX; posY = newPosY;
		type = newType;
		id = newID;
		facing = "Right";
		owner = newOwner;

		MG_DB_UnitValues.I._setValues (newType);
		HPmax = MG_DB_UnitValues.I.HP;
		MPmax = MG_DB_UnitValues.I.MP;
		HP = HPmax; MP = MPmax;
		moveSpeed = MG_DB_UnitValues.I.moveSpeed;

		sprite.transform.SetParent (GameObject.Find ("_MG_UNITS").transform);
		rigidBody = sprite.GetComponent<Rigidbody2D>();

		// Default status:
		state 					= "idle";
	}

	public void _changeSprite(string newSpriteName){
		MG_ControlTerrain.I._destroyTileSprite (sprite);
		sprite = MG_DB_Unit.I._getSprite (newSpriteName);
		sprite.transform.position = new Vector3 (posX, posY, posY - 3);
	}

	// Only use this update to define this unit's position
	#region "Update"
	public void _update(){
		// Dead update
		if (isDead) {
			removalTime -= Time.deltaTime;
			if (removalTime <= 0) {
				MG_ControlUnit.I._addToDestroyList (this);
			}
		}
		// Not dead update
		else {
			posX = sprite.transform.position.x;
			posY = sprite.transform.position.y;
			sprite.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y, sprite.transform.position.y - 1);
		}
	}
	#endregion

	#region "COMMANDS - Move"
	/// <summary>
	/// Moves by CURRENT_POSITION + MOVE_VECTOR (Calculated in this function based from inputted angle)
	/// </summary>
	public void _move_Increment(float moveAngle){
		if(state != "idle" && state != "moving") return;
		float 	moveX = moveSpeed * Mathf.Cos ((moveAngle * Mathf.PI) / 180),
				moveY = moveSpeed * Mathf.Sin ((moveAngle * Mathf.PI) / 180);

		rigidBody.velocity = new Vector3 (moveX, moveY);
		facingAngle = moveAngle;
	}

	public void _stopMoving(){
		if (state == "moving") {
			state = "idle";
			rigidBody.velocity = new Vector3 (0, 0);
		}
	}
	#endregion
	#region "COMMANDS - Use Weapon/Spell"

	#endregion

	#region "CONTROL - Kill this unit"
	public void _kill(){
		isDead = true;
		MG_ControlUnit.I._destroyGameObject (sprite);
	}
	#endregion

	#region "Physics"
	public void _applyForce(float angle, float force){
		Vector2 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector2.right;
		rigidBody.AddForce(dir * force);
	}
	public void _applyVelocity(float angle, float force){
		rigidBody.velocity = new Vector3(force * Mathf.Cos((angle*Mathf.PI)/180), 
			force * Mathf.Sin((angle*Mathf.PI)/180));
	}
	public void _moveInstantly(Vector3 newPos){
		sprite.transform.position = new Vector3(newPos.x, newPos.y, sprite.transform.position.z);
	}
	#endregion
}
