using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_ClassTerrain {

	public GameObject sprite;
	public string type;
	public int id, posX, posY;

	public O_ClassTerrain(GameObject newSprite, string newType, int newID, int newPosX, int newPosY){
		sprite = newSprite;
		sprite.name = newType;
		sprite.transform.SetParent (GameObject.Find ("_O_TERRAIN").transform);
		sprite.transform.position = new Vector3 (newPosX, newPosY, 200);

		posX = newPosX; posY = newPosY;
		type = newType;
		id = newID;
	}

	public void _changeTerrain(GameObject newSprite, string newType){
		O_ControlTerrain.I._destroyTileSprite (sprite);
		sprite = newSprite;
		sprite.transform.position = new Vector3 (posX, posY, 100);
		type = newType;
	}
}
