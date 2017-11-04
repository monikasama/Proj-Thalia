using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Doodad : MonoBehaviour {
	public static MG_DB_Doodad I;
	public void Awake(){ I = this; }

	public GameObject dummy, testDood;

	public GameObject _getSprite(string newSpriteName){
		GameObject retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		Destroy (retVal); Debug.Log (newSpriteName);
		switch (newSpriteName) {
			#region "Test Sprites"
			case "testDood": retVal = GameObject.Instantiate (testDood, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion

			default:  retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}

		return retVal;
	}
}
