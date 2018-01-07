using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassBuff {

	public GameObject sprite;
	public bool hasSprite;
	public float spr_offsetX, spr_offsetY;

	public int id, unitOwnerID;
	public float duration;
	public string type;

	#region "Constructors"
	public MG_ClassBuff(GameObject newSprite, string newType, int newID, int newUnitOwnerID, float newDuration){
		hasSprite = true;
		id = newID;
		type = newType;

		unitOwnerID = newUnitOwnerID;
		duration = newDuration;

		if (hasSprite) {
			sprite = newSprite;
			_updateSpritePosition ();
			sprite.transform.SetParent (GameObject.Find ("_MG_BUFFS").transform);

			spr_offsetX = MG_DB_Buff.I.spr_offsetX;
			spr_offsetY = MG_DB_Buff.I.spr_offsetY;
		}
	}

	public MG_ClassBuff(int newUnitOwnerID, string newType, int newID, float newDuration){
		hasSprite = false;
		id = newID;
		type = newType;

		unitOwnerID = newUnitOwnerID;
		duration = newDuration;
	}
	#endregion

	#region "Update"
	public void _update(){
		// Check for owner's existence
		if(!MG_GetUnit.I._doesUnitExist(unitOwnerID)) 		MG_ControlBuffs.I._addToDestroyList (this);

		/*Update sprite position*/	_updateSpritePosition ();


		// Duration
		duration -= Time.deltaTime;
		if (duration <= 0) {
			MG_ControlBuffs.I._addToDestroyList (this);
		}
	}

	public void _updateSpritePosition(){
		if (!hasSprite) 			return;
		MG_ClassUnit uOwner = MG_GetUnit.I._getUnitFromID (unitOwnerID);
		if (uOwner == null) 		return;
		if (uOwner.sprite == null) 	return;

		Debug.Log (uOwner.sprite.transform.position);
		sprite.transform.position = new Vector3 (	uOwner.sprite.transform.position.x + spr_offsetX, 
													uOwner.sprite.transform.position.y + spr_offsetY,
													uOwner.sprite.transform.position.z + spr_offsetY - 2);
	}
	#endregion
}
