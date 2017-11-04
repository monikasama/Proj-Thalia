using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassUnit {

	public GameObject sprite;
	public string type, facing, nature, moveType, obstacleType;

	public int owner;

	/*
	 * 	MOVE TYPE - This is set from the hero control (MG_ControlHero.cs)
	 * 
	 * 	TYPES OF MOVE TYPES:
	 * 	- land
	 *  - air
	 * 	
	 * 	TYPES OF OBSTACLE TYPES:
	 *  - boulder 			- Unpassable
	 */

	public float posX, posY;
	public int id, entranceId;

	////////////////// STATUS VARIABLES //////////////////
	public string state;		// Different list of states can be seen at Documentation / MG - Unit States.txt
	////////////////// STATUS VARIABLES //////////////////

	public MG_ClassUnit(GameObject newSprite, string newType, int newID, float newPosX, float newPosY, int newOwner){
		sprite = newSprite;
		sprite.transform.position = new Vector3 (newPosX, newPosY, newPosY);
		posX = newPosX; posY = newPosY;
		type = newType;
		id = newID;
		facing = "Right";

		MG_DB_UnitValues.I._setValues (newType);
		obstacleType 			= MG_DB_UnitValues.I.obstacleType;
		nature 					= MG_DB_UnitValues.I.nature;

		sprite.transform.SetParent (GameObject.Find ("_MG_UNITS").transform);

		// Default status:
		state 					= "idle";
	}

	public void _changeSprite(string newSpriteName){
		MG_ControlTerrain.I._destroyTileSprite (sprite);
		sprite = MG_DB_Unit.I._getSprite (newSpriteName);
		sprite.transform.position = new Vector3 (posX, posY, posY);
	}

	#region "Move"
	/// <summary>
	/// Moves by CURRENT_POSITION + MOVE_VECTOR
	/// </summary>
	public void _move_Increment(float moveX, float moveY){
		sprite.transform.position += new Vector3(moveX, moveY, moveY);

		posX = sprite.transform.position.x;
		posY = sprite.transform.position.y;
	}
	#endregion
}
