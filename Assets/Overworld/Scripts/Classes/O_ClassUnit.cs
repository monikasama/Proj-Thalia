using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_ClassUnit {

	public float DEFAULT_Z_POSITION = -200;

	public GameObject sprite;
	public string type, facing, nature, moveType, obstacleType,
					nextScene,				// Defines the next scene if used as location
					location;				// Defines the next map to use if this unit is a location

	/*
	 * 	MOVE TYPE - This is set from the hero control (O_ControlHero.cs)
	 * 
	 * 	TYPES OF MOVE TYPES:
	 * 	- land
	 *  - air
	 * 	
	 * 	TYPES OF OBSTACLE TYPES:
	 *  - boulder 			- Unpassable
	 * 
	 *  TYPES OF NATURE:
	 *  - location			- Points in map where the player can interact
	 *  - NONE
	 */

	public int id, posX, posY;

	public O_ClassUnit(GameObject newSprite, string newType, int newID, int newPosX, int newPosY){
		sprite = newSprite;
		sprite.transform.position = new Vector3 (newPosX, newPosY, DEFAULT_Z_POSITION);
		posX = newPosX; posY = newPosY;
		type = newType;
		id = newID;
		facing = "Right";

		O_DB_UnitValues.I._setValues (newType);
		obstacleType 			= O_DB_UnitValues.I.obstacleType;
		nature 					= O_DB_UnitValues.I.nature;
		nextScene 				= O_DB_UnitValues.I.nextScene;
		location 				= O_DB_UnitValues.I.location;

		sprite.transform.SetParent (GameObject.Find ("_O_UNITS").transform);
	}

	public void _changeSprite(string newSpriteName){
		O_ControlTerrain.I._destroyTileSprite (sprite);
		sprite = O_DB_Unit.I._getSprite (newSpriteName);
		sprite.transform.position = new Vector3 (posX, posY, DEFAULT_Z_POSITION);
	}
}
