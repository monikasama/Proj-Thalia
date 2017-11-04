using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_GetDoodad : MonoBehaviour {
	public static MG_GetDoodad I;
	public void Awake(){ I = this; }

	#region "Get Last Created Unit"
	public MG_ClassDoodad _getLastCreatedDood(){
		return MG_Globals.I.doodadsTemp [MG_Globals.I.doodadsTemp.Count - 1];
	}
	#endregion

	//Includes:
	//	- _pointHasDood()			- Returns true if point in map has a doodad
	//	- _getDoodFromPoint()		- Gets and returns the doodad in a point
	#region "Get Doodad from point"
	public bool _pointHasDood(float posX, float posY, string doodName = "Any"){
		foreach (MG_ClassDoodad dood in MG_Globals.I.doodads) {
			if (dood.posX == posX && dood.posY == posY) {
				if(dood.type == doodName || doodName == "Any" || (doodName == "Corner" && dood.isCorner))
					return true;
			}
		}
		foreach (MG_ClassDoodad dood in MG_Globals.I.doodadsTemp) {
			if (dood.posX == posX && dood.posY == posY) {
				if(dood.type == doodName || doodName == "Any" || (doodName == "Corner" && dood.isCorner))
					return true;
			}
		}

		return false;
	}

	public MG_ClassDoodad _getDoodFromPoint(float posX, float posY, string doodName = "Any"){
		MG_ClassDoodad retDood = MG_Globals.I.doodads [0];

		foreach (MG_ClassDoodad dood in MG_Globals.I.doodads) {
			if (dood.posX == posX && dood.posY == posY) {
				if(dood.type == doodName || doodName == "Any" || (doodName == "Corner" && dood.isCorner))
					retDood = dood;
				break;
			}
		}
		return retDood;
	}
	#endregion
}
