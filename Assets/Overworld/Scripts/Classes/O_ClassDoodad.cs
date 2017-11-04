using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_ClassDoodad {

	public GameObject sprite;
	public int id, customValue;
	public float posX, posY;
	public string type;

	public bool isCorner;

	#region "Class Constructors"
	public O_ClassDoodad(GameObject newSprite, string newObjType, int newObjId, float newPosX, float newPosY){
		sprite = newSprite;
		sprite.transform.position = new Vector3 (newPosX, newPosY, newPosY);
		posX = newPosX; posY = newPosY;

		sprite.name = newObjType;
		sprite.transform.SetParent (GameObject.Find ("_O_DOODADS").transform);

		isCorner = O_DB_DoodadValues.I._getDoodIsCorner (newObjType);

		id = newObjId;
		customValue = 0;

		type = newObjType;
	}

	public O_ClassDoodad(GameObject newSprite, string newObjType, int newObjId, float newPosX, float newPosY, float zRotation){
		sprite = newSprite;
		sprite.transform.position = new Vector3 (newPosX, newPosY, newPosY);
		posX = newPosX; posY = newPosY;

		sprite.name = newObjType;
		sprite.transform.SetParent (GameObject.Find ("_O_DOODADS").transform);
		sprite.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, zRotation));

		isCorner = O_DB_DoodadValues.I._getDoodIsCorner (newObjType);

		id = newObjId;
		customValue = 0;

		type = newObjType;
	}

	public O_ClassDoodad(GameObject newSprite, int newObjId, float newPosX, float newPosY, float scaleX, float scaleY, float zPosition, float zRotation, string newObjType){
		// Set values here
		sprite = newSprite;
		sprite.transform.position = new Vector3 (newPosX, newPosY, zPosition);
		sprite.transform.localScale = new Vector3 (scaleX, scaleY, 0);
		posX = newPosX; posY = newPosY;

		sprite.name = newObjType;
		sprite.transform.SetParent (GameObject.Find ("_O_DOODADS").transform);
		sprite.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, zRotation));

		isCorner = O_DB_DoodadValues.I._getDoodIsCorner (newObjType);

		id = newObjId;
		customValue = 0;

		type = newObjType;
	}
	#endregion
}
