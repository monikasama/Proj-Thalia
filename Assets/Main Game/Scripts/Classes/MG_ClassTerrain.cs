using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassTerrain {

	public GameObject sprite;
	public string type;
	public int id, posX, posY;

	public MG_ClassTerrain(GameObject newSprite, string newType, int newID, int newPosX, int newPosY){
		sprite = newSprite;
		sprite.name = newType;
		sprite.transform.SetParent (GameObject.Find ("_MG_TERRAIN").transform);
		sprite.transform.position = new Vector3 (newPosX, newPosY, 200);

		posX = newPosX; posY = newPosY;
		type = newType;
		id = newID;
	}

	public void _changeTerrain(GameObject newSprite, string newType){
		MG_ControlTerrain.I._destroyTileSprite (sprite);
		sprite = newSprite;
		sprite.transform.position = new Vector3 (posX, posY, 100);
		type = newType;
	}
}
