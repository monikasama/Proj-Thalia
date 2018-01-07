using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Doodad : MonoBehaviour {
	public static MG_DB_Doodad I;
	public void Awake(){ I = this; }

	public GameObject 				dummy, testDood
	/*CORNERS - Cliff*/				, mg_cliffCorner1, mg_cliffCorner2, mg_cliffCorner3, mg_cliffCorner4, mg_cliffCorner5, mg_cliffCorner6, mg_cliffCorner7, mg_cliffCorner8
	/*CORNERS - Grass*/				, mg_grassCorner1, mg_grassCorner2, mg_grassCorner3
	/*CORNERS - Water Plain*/		, mg_waterPlainCorner1, mg_waterPlainCorner2, mg_waterPlainCorner3
	/*CORNERS - Dirt*/				, mg_dirtCorner1, mg_dirtCorner2, mg_dirtCorner3
	/*CORNERS - ROAD - Rock*/		, mg_roadRockCorner1, mg_roadRockCorner2, mg_roadRockCorner3, mg_roadRockCorner4, mg_roadRockCorner5, mg_roadRockCorner6, mg_roadRockCorner7

	/*TREES - Summer*/				, mg_treeSummer_01, mg_treeSummer_02
	;

	public GameObject _getSprite(string newSpriteName){
		GameObject retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		Destroy (retVal);
		switch (newSpriteName) {
			#region "Test Sprites"
			case "testDood": retVal = GameObject.Instantiate (testDood, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "CORNERS - Cliff"
			case "mg_cliffCorner1": retVal = GameObject.Instantiate (mg_cliffCorner1, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_cliffCorner2": retVal = GameObject.Instantiate (mg_cliffCorner2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_cliffCorner3": retVal = GameObject.Instantiate (mg_cliffCorner3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_cliffCorner4": retVal = GameObject.Instantiate (mg_cliffCorner4, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_cliffCorner5": retVal = GameObject.Instantiate (mg_cliffCorner5, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_cliffCorner6": retVal = GameObject.Instantiate (mg_cliffCorner6, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_cliffCorner7": retVal = GameObject.Instantiate (mg_cliffCorner7, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_cliffCorner8": retVal = GameObject.Instantiate (mg_cliffCorner8, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "CORNERS - Grass"
			case "mg_grassCorner1": retVal = GameObject.Instantiate (mg_grassCorner1, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_grassCorner2": retVal = GameObject.Instantiate (mg_grassCorner2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_grassCorner3": retVal = GameObject.Instantiate (mg_grassCorner3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "CORNERS - Water Plain"
			case "mg_waterPlainCorner1": retVal = GameObject.Instantiate (mg_waterPlainCorner1, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_waterPlainCorner2": retVal = GameObject.Instantiate (mg_waterPlainCorner2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_waterPlainCorner3": retVal = GameObject.Instantiate (mg_waterPlainCorner3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "ROAD - Dirt"
			case "mg_dirtCorner1": retVal = GameObject.Instantiate (mg_dirtCorner1, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_dirtCorner2": retVal = GameObject.Instantiate (mg_dirtCorner2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_dirtCorner3": retVal = GameObject.Instantiate (mg_dirtCorner3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "ROAD - Rock"
			case "mg_roadRockCorner1": retVal = GameObject.Instantiate (mg_roadRockCorner1, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_roadRockCorner2": retVal = GameObject.Instantiate (mg_roadRockCorner2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_roadRockCorner3": retVal = GameObject.Instantiate (mg_roadRockCorner3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_roadRockCorner4": retVal = GameObject.Instantiate (mg_roadRockCorner4, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_roadRockCorner5": retVal = GameObject.Instantiate (mg_roadRockCorner5, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_roadRockCorner6": retVal = GameObject.Instantiate (mg_roadRockCorner6, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_roadRockCorner7": retVal = GameObject.Instantiate (mg_roadRockCorner7, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion

			#region "TREES - Summer"
			case "mg_treeSummer_01": retVal = GameObject.Instantiate (mg_treeSummer_01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mg_treeSummer_02": retVal = GameObject.Instantiate (mg_treeSummer_02, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			default:  retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}

		return retVal;
	}
}
