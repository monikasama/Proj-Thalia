using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlUnit : MonoBehaviour {
	public static MG_ControlUnit I;
	public void Awake(){ I = this; }

	private int unitCnt, entrances;
	public List<int> toDestroy;

	#region "Creation Codes"
	public void _createUnit(string newUnitType, float newPosX, float newPosY, int newOwner){
		MG_Globals.I.unitsTemp.Add (new MG_ClassUnit (MG_DB_Unit.I._getSprite (newUnitType), newUnitType, unitCnt, newPosX, newPosY, newOwner));
		unitCnt++;

		// SPECIALS
		/*Get created unit*/			MG_ClassUnit lastCreatedUnit = MG_GetUnit.I._getLastCreatedUnit();
		/*Assigning entrance ID*/		if (newUnitType == "entrance") 			_assignEntranceID (lastCreatedUnit);
	}

	private void _assignEntranceID(MG_ClassUnit targetEnt){
		entrances++;
		targetEnt.entranceId = entrances;
	}
	#endregion

	#region "Destroy Codes"
	public void _addToDestroyList(MG_ClassUnit targetUnit){
		toDestroy.Add (targetUnit.id);
	}

	public void _destroyListed(){
		if (toDestroy.Count > 0) {
			for (int i = 0; i < toDestroy.Count; i++) {
				_destroyUnit (i);
			}
		}
		toDestroy.Clear ();
	}

	public void _destroyUnit(int targetUnitIndex){
		int indexToRemove = -1;
		for (int i = MG_Globals.I.units.Count - 1; i >= 0; i--) {
			if (toDestroy [targetUnitIndex] == MG_Globals.I.units [i].id) {
				indexToRemove = i;
				break;
			}
		}
		if (indexToRemove > -1) {
			if(MG_Globals.I.units [indexToRemove].sprite)
				Destroy (MG_Globals.I.units [indexToRemove].sprite);
			MG_Globals.I.units.RemoveAt (indexToRemove);
		}
	}

	public void _destroyGameObject(GameObject targetObj){
		Destroy (targetObj);
	}
	#endregion
}
