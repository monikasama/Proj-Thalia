using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_GetTerrain : MonoBehaviour {
	public static MG_GetTerrain I;
	public void Awake(){ I = this; }

	public bool hasPicked;
	public MG_ClassTerrain _getTerrain(int posX, int posY){
		hasPicked = false;
		MG_ClassTerrain output = MG_Globals.I.terrains[0];
		foreach(MG_ClassTerrain tL in MG_Globals.I.terrains){
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
