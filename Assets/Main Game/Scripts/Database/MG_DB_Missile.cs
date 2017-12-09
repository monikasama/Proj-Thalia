using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Missile : MonoBehaviour {
	public static MG_DB_Missile I;
	public void Awake(){ I = this; }

	public GameObject dummy 
	/*Test*/	, testMissile
	;

	// DO NOT FORGET TO PUT THESE VALUES IN EditorOverworld / EditorMainGame

	public GameObject _getSprite(string newSpriteName){
		GameObject retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		Destroy (retVal);
		switch (newSpriteName) {
			#region "Test Sprites"
			case "test": retVal = GameObject.Instantiate (testMissile, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
				#endregion

			default: retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}

		return retVal;
	}
}
