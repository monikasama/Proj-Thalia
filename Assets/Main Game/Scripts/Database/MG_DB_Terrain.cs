using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Terrain : MonoBehaviour {
	public static MG_DB_Terrain I;
	public void Awake(){ I = this; }

	public GameObject 			dummy, testA, testB
	/*Grass*/					,mg_grass01
	/*Cliff*/					,mg_cliff01
	;

	// DO NOT FORGET TO PUT THESE VALUES IN EditorOverworld / EditorMainGame

	public GameObject _getSprite(string newSpriteName){
		GameObject retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		Destroy (retVal);
		switch (newSpriteName) {
				#region "Test Sprites"
					case "testA": retVal = GameObject.Instantiate (testA, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
					case "testB": retVal = GameObject.Instantiate (testB, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
  				#endregion
				#region "Grass"
					case "mg_grass01": retVal = GameObject.Instantiate (mg_grass01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
				#endregion
				#region "Cliff"
					case "mg_cliff01": retVal = GameObject.Instantiate (mg_cliff01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
				#endregion

			default: retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}

		return retVal;
	}
}
