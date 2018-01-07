using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassDoodad {

	public GameObject sprite;
	public int id, customValue;
	public float posX, posY;
	public string type;

	public bool isCorner;

	#region "Class Constructors"
	public MG_ClassDoodad(GameObject newSprite, string newObjType, int newObjId, float newPosX, float newPosY){
		sprite = newSprite;
		sprite.transform.position = new Vector3 (newPosX, newPosY, newPosY);
		posX = newPosX; posY = newPosY;

		sprite.name = newObjType;
		sprite.transform.SetParent (GameObject.Find ("_MG_DOODADS").transform);

		isCorner = MG_DB_DoodadValues.I._getDoodIsCorner (newObjType);

		id = newObjId;
		customValue = 0;

		type = newObjType;
	}

	public MG_ClassDoodad(GameObject newSprite, string newObjType, int newObjId, float newPosX, float newPosY, float zRotation){
		sprite = newSprite;
		sprite.transform.position = new Vector3 (newPosX, newPosY, newPosY);
		posX = newPosX; posY = newPosY;

		sprite.name = newObjType;
		sprite.transform.SetParent (GameObject.Find ("_MG_DOODADS").transform);
		sprite.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, zRotation));

		isCorner = MG_DB_DoodadValues.I._getDoodIsCorner (newObjType);

		id = newObjId;
		customValue = 0;

		type = newObjType;
	}

	public MG_ClassDoodad(GameObject newSprite, int newObjId, float newPosX, float newPosY, float scaleX, float scaleY, float zPosition, float zRotation, string newObjType){
		// Set values here
		sprite = newSprite;
		sprite.transform.position = new Vector3 (newPosX, newPosY, zPosition);
		sprite.transform.localScale = new Vector3 (scaleX, scaleY, 0);
		posX = newPosX; posY = newPosY;

		sprite.name = newObjType;
		sprite.transform.SetParent (GameObject.Find ("_MG_DOODADS").transform);
		sprite.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, zRotation));

		isCorner = MG_DB_DoodadValues.I._getDoodIsCorner (newObjType);

		id = newObjId;
		customValue = 0;

		type = newObjType;
	}
	#endregion

	//Includes
	//	- _adjustAnchorPointForEditor()													- Used to reposition anchor point for editor use
	#region "Misc"
	// Used to reposition anchor point for editor use
	public void _adjustAnchorPointForEditor(){
		switch (type) {
			case "mg_treeSummer_01":
				sprite.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y + 0.7f, sprite.transform.position.z);
			break;
			case "mg_treeSummer_02":
				sprite.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y + 0.7f, sprite.transform.position.z);
			break;
		}
	}
	#endregion
}
