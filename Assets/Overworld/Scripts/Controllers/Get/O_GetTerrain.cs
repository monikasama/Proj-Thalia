using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_GetTerrain : MonoBehaviour {
	public static O_GetTerrain I;
	public void Awake(){ I = this; }

	public bool hasPicked;
	public O_ClassTerrain _getTerrain(int posX, int posY){
		hasPicked = false;
		O_ClassTerrain output = O_Globals.I.terrains[0];
		foreach(O_ClassTerrain tL in O_Globals.I.terrains){
			if(tL.posX == posX && tL.posY == posY){
				output = tL;
				hasPicked = true;
				break;
			}
		}
		if(!hasPicked) Debug.Log("Picking unsuccessful.");
		return output;
	}
}
