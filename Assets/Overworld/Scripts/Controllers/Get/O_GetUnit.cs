using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_GetUnit : MonoBehaviour {
	public static O_GetUnit I;
	public void Awake(){ I = this; }

	#region "Get Last Created Unit"
	public O_ClassUnit _getLastCreatedUnit(){
		return O_Globals.I.unitsTemp [O_Globals.I.unitsTemp.Count - 1];
	}
	#endregion

	//Includes:
	//	- _pointHasUnit()			- Returns true if point in map has a unit
	//	- _getUnitFromPoint()		- Gets and returns the unit in a point
	#region "Get Unit from point"
	public bool _pointHasUnit(int posX, int posY){
		foreach (O_ClassUnit unit in O_Globals.I.units) {
			if (unit.posX == posX && unit.posY == posY)
				return true;
		}

		return false;
	}

	public O_ClassUnit _getUnitFromPoint(int posX, int posY){
		O_ClassUnit retUnit = O_Globals.I.units [0];

		foreach (O_ClassUnit unit in O_Globals.I.units) {
			if (unit.posX == posX && unit.posY == posY) {
				retUnit = unit;
				break;
			}
		}

		return retUnit;
	}
	#endregion
}
