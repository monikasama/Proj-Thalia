using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_DB_Unit : MonoBehaviour {
	public static O_DB_Unit I;
	public void Awake(){ I = this; }

	public GameObject dummy 
	/*Test*/	,testYou, testTown
	/*Editor*/	,pathBlokEditor
	;

	// DO NOT FORGET TO PUT THESE VALUES IN EditorOverworld / EditorMainGame

	public GameObject _getSprite(string newSpriteName){
		GameObject retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		Destroy (retVal);
		switch (newSpriteName) {
			#region "Test Sprites"
			case "testYou": retVal = GameObject.Instantiate (testYou, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "testTown": retVal = GameObject.Instantiate (testTown, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "Editor"
			case "pathBlocker_Editor": retVal = GameObject.Instantiate (pathBlokEditor, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion

			default: retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}

		return retVal;
	}
}
