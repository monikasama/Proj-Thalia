using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_ControlTerrain : MonoBehaviour {
	public static O_ControlTerrain I;
	public void Awake(){ I = this; }

	private int terCnt;
	public List<int> toDestroy;

	public void _createTerrain(string newTerrainType, int newPosX, int newPosY){
		O_Globals.I.terrainsTemp.Add (new O_ClassTerrain(O_DB_Terrain.I._getSprite(newTerrainType), newTerrainType, terCnt, newPosX, newPosY));
		terCnt++;
	}

	//Includes
	//	- _destroyTileSprite(); for destroying GameObject sprites
	#region "External Control"
	public void _destroyTileSprite(GameObject targetSprite){
		Destroy (targetSprite);
	}
	#endregion
}
