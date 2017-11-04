using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_ControlUnit : MonoBehaviour {
	public static O_ControlUnit I;
	public void Awake(){ I = this; }

	private int unitCnt;
	public List<int> toDestroy;

	public void _createUnit(string newUnitType, int newPosX, int newPosY){
		O_Globals.I.unitsTemp.Add (new O_ClassUnit (O_DB_Unit.I._getSprite (newUnitType), newUnitType, unitCnt, newPosX, newPosY));
		unitCnt++;
	}

	#region "Destroy Codes"
	public void _addToDestroyList(O_ClassUnit targetUnit){
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
		for (int i = O_Globals.I.units.Count - 1; i >= 0; i--) {
			if (toDestroy [targetUnitIndex] == O_Globals.I.units [i].id) {
				indexToRemove = i;
				break;
			}
		}
		if (indexToRemove > -1) {
			Destroy (O_Globals.I.units [indexToRemove].sprite);
			O_Globals.I.units.RemoveAt (indexToRemove);
		}
	}
	#endregion
}
