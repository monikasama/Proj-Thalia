using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassMissile {

	public GameObject sprite;
	public Rigidbody2D rigidBody;

	public float posX, posY, angle, speed;
	public string type;
	public int id, ownerID, playerOwner;

	public MG_ClassMissile(GameObject newSprite, string newType, int newID, float newPosX, float newPosY, int newOwnerID, float newAngle){
		sprite = newSprite;
		sprite.transform.position = new Vector3 (newPosX, newPosY, newPosY - 4);
		posX = newPosX; posY = newPosY;
		type = newType;
		id = newID;
		ownerID = newOwnerID;

		// Get player owner
		#region "Get Player Owner"
		bool hasOwner = false;
		foreach(MG_ClassUnit uL in MG_Globals.I.units){
			if (uL.id == ownerID) {
				playerOwner = uL.owner;
				hasOwner = true;
				break;
			}
		}
		if(!hasOwner){
			foreach(MG_ClassUnit uL in MG_Globals.I.unitsTemp){
				if (uL.id == ownerID) {
					playerOwner = uL.owner;
					hasOwner = true;
					break;
				}
			}
		}
		if(!hasOwner)		playerOwner = 3;
		#endregion
		
		angle = newAngle;
		sprite.transform.SetParent (GameObject.Find ("_MG_MISSILES").transform);
		rigidBody = sprite.GetComponent<Rigidbody2D>();

		// Default values
		MG_DB_MissileValue.I._setValues(newType);
		speed 								= MG_DB_MissileValue.I.speed;
	}

	// Includes
	// 	_update() - 								Moves the missile and define this missile's position
	//	_changeSprite() -							
	#region "Update"
	public void _update(){
		float speedX = speed * Mathf.Cos ((angle * Mathf.PI) / 180);
		float speedY = speed * Mathf.Sin ((angle * Mathf.PI) / 180);
		rigidBody.velocity = new Vector3 (speedX, speedY);

		posX = sprite.transform.position.x;
		posY = sprite.transform.position.y;
		sprite.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y, sprite.transform.position.y - 4);
	}

	public void _changeSprite(string newSpriteName){
		MG_ControlTerrain.I._destroyTileSprite (sprite);
		sprite = MG_DB_Unit.I._getSprite (newSpriteName);
		sprite.transform.position = new Vector3 (posX, posY, posY - 3);
	}
	#endregion
}
