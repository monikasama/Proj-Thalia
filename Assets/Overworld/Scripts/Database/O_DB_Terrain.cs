using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_DB_Terrain : MonoBehaviour {
	public static O_DB_Terrain I;
	public void Awake(){ I = this; }

	public GameObject dummy, testA, testB
	/*Grass*/			,grass01
	/*Beach*/			,beach01
	/*Sea*/				,sea01
	/*Deep Sea*/		,deepSea01
	/*Black Grass*/		,blackGrass01
	/*Road*/			,road01
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
			case "grass01": retVal = GameObject.Instantiate (grass01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "Beach"
			case "beach01": retVal = GameObject.Instantiate (beach01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "Sea"
			case "sea01": retVal = GameObject.Instantiate (sea01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "Deep Sea"
			case "deepSea01": retVal = GameObject.Instantiate (deepSea01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "Black Grass"
			case "blackGrass01": retVal = GameObject.Instantiate (blackGrass01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "Road"
			case "road01": retVal = GameObject.Instantiate (road01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			
			default: retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}

		return retVal;
	}
}
