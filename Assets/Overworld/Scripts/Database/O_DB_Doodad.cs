using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_DB_Doodad : MonoBehaviour {
	public static O_DB_Doodad I;
	public void Awake(){ I = this; }

	public GameObject dummy, testDood;
	/*CORNERS - Grass*/					public GameObject GrassCorner1, GrassCorner2, GrassCorner3;
	/*CORNERS - Sea*/					public GameObject SeaCorner1, SeaCorner2, SeaCorner3;
	/*CORNERS - Deep Sea*/				public GameObject DeepSeaCorner1, DeepSeaCorner2, DeepSeaCorner3;
	/*CORNERS - Roads*/					public GameObject RoadCorner1_x1, RoadCorner1_x2, RoadCorner1_x4, RoadCorner1_x8, RoadCorner2, RoadCorner3, RoadCorner4, RoadCorner5, RoadCorner6;
	/*CORNERS - Black Grass*/			public GameObject BlackGrassCorner1, BlackGrassCorner2, BlackGrassCorner3;

	/*Hills*/							public GameObject hill01;
	/*Mountains 1*/						public GameObject mountain01, mountain01_2, mountain01_3;
	/*Mountains 2*/						public GameObject mountain02, mountain02_2, mountain02_3;
	/*Mountains 3*/						public GameObject mountain03, mountain03_2, mountain03_3;
	/*Tree 1*/							public GameObject tree1, tree1_2, tree1_3, tree1_4;
	/*Tree 2*/							public GameObject tree2, tree2_2, tree2_3;

	public GameObject _getSprite(string newSpriteName){
		GameObject retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		Destroy (retVal);
		switch (newSpriteName) {
			#region "Test Sprites"
			case "testDood": retVal = GameObject.Instantiate (testDood, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "CORNERS - Grass"
			case "GrassCorner1": retVal = GameObject.Instantiate (GrassCorner1, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "GrassCorner2": retVal = GameObject.Instantiate (GrassCorner2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "GrassCorner3": retVal = GameObject.Instantiate (GrassCorner3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "CORNERS - Sea"
			case "SeaCorner1": retVal = GameObject.Instantiate (SeaCorner1, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "SeaCorner2": retVal = GameObject.Instantiate (SeaCorner2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "SeaCorner3": retVal = GameObject.Instantiate (SeaCorner3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "CORNERS - DeepSea"
			case "DeepSeaCorner1": 	retVal = GameObject.Instantiate (DeepSeaCorner1, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "DeepSeaCorner2": 	retVal = GameObject.Instantiate (DeepSeaCorner2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "DeepSeaCorner3": 	retVal = GameObject.Instantiate (DeepSeaCorner3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "CORNERS - Road"
			case "roadCorner1_x1": 	retVal = GameObject.Instantiate (RoadCorner1_x1, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "roadCorner1_x2": 	retVal = GameObject.Instantiate (RoadCorner1_x2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "roadCorner1_x4":	retVal = GameObject.Instantiate (RoadCorner1_x4, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "roadCorner1_x8": 	retVal = GameObject.Instantiate (RoadCorner1_x8, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "roadCorner2": 	retVal = GameObject.Instantiate (RoadCorner2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "roadCorner3": 	retVal = GameObject.Instantiate (RoadCorner3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "roadCorner4": 	retVal = GameObject.Instantiate (RoadCorner4, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "roadCorner5": 	retVal = GameObject.Instantiate (RoadCorner5, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "roadCorner6": 	retVal = GameObject.Instantiate (RoadCorner6, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "CORNERS - Black Grass"
			case "BlackGrassCorner1": retVal = GameObject.Instantiate (BlackGrassCorner1, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "BlackGrassCorner2": retVal = GameObject.Instantiate (BlackGrassCorner2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "BlackGrassCorner3": retVal = GameObject.Instantiate (BlackGrassCorner3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion

			#region "Hills"
			case "hill01": retVal = GameObject.Instantiate (hill01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "Mountains 1"
			case "mountain01": retVal = GameObject.Instantiate (mountain01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mountain01-2": retVal = GameObject.Instantiate (mountain01_2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mountain01-3": retVal = GameObject.Instantiate (mountain01_3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "Mountains 2"
			case "mountain02": retVal = GameObject.Instantiate (mountain02, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mountain02-2": retVal = GameObject.Instantiate (mountain02_2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "mountain02-3": retVal = GameObject.Instantiate (mountain02_3, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			#region "Mountains 2"
			case "tree1": retVal = GameObject.Instantiate (tree1, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "tree2": retVal = GameObject.Instantiate (tree2, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			#endregion
			
			default:  retVal = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}

		return retVal;
	}
}
