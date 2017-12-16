using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Unit : MonoBehaviour {
	public static MG_DB_Unit I;
	public void Awake(){ I = this; }

	public GameObject dummy 
	/*Test*/	, testYou, testEnemy
	/*Editor*/	, pathBlokEditor, entranceEditor
	;

	// DO NOT FORGET TO PUT THESE VALUES IN EditorOverworld / EditorMainGame

	public GameObject _getSprite(string newSpriteName){
		GameObject retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		Destroy (retVal);
		switch (newSpriteName) {
			#region "Test Sprites"
			case "testYou": retVal = GameObject.Instantiate (testYou, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "testEnemy": retVal = GameObject.Instantiate (testEnemy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "Editor"
			case "pathBlocker_Editor": 	retVal = GameObject.Instantiate (pathBlokEditor, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "entrance_Editor": 	retVal = GameObject.Instantiate (entranceEditor, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			
			default: retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}

		return retVal;
	}
}
