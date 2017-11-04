using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_GetDoodad : MonoBehaviour {
	public static O_GetDoodad I;
	public void Awake(){ I = this; }

	#region "Get Last Created Unit"
	public O_ClassDoodad _getLastCreatedDood(){
		return O_Globals.I.doodadsTemp [O_Globals.I.doodadsTemp.Count - 1];
	}
	#endregion

	//Includes:
	//	- _pointHasDood()			- Returns true if point in map has a doodad
	//	- _getDoodFromPoint()		- Gets and returns the doodad in a point
	#region "Get Doodad from point"
	public bool _pointHasDood(float posX, float posY, string doodName = "Any"){
		foreach (O_ClassDoodad dood in O_Globals.I.doodads) {
			if (dood.posX == posX && dood.posY == posY) {
				if(dood.type == doodName || doodName == "Any" || (doodName == "Corner" && dood.isCorner))
					return true;
			}
		}
		foreach (O_ClassDoodad dood in O_Globals.I.doodadsTemp) {
			if (dood.posX == posX && dood.posY == posY) {
				if(dood.type == doodName || doodName == "Any" || (doodName == "Corner" && dood.isCorner))
					return true;
			}
		}

		return false;
	}

	public O_ClassDoodad _getDoodFromPoint(float posX, float posY, string doodName = "Any"){
		O_ClassDoodad retDood = O_Globals.I.doodads [0];

		foreach (O_ClassDoodad dood in O_Globals.I.doodads) {
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
