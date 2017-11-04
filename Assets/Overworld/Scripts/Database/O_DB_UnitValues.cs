using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_DB_UnitValues : MonoBehaviour {
	public static O_DB_UnitValues I;
	public void Awake(){ I = this; }

	public string obstacleType, nature, nextScene, location;

	public void _setValues(string newUnitType){
		switch (newUnitType) {
			case "pathBlocker":
				obstacleType 						= "boulder";
				nature 								= " ";
				location 							= " ";
				nextScene 							= " ";
			break;
			case "testTown":
				obstacleType 						= " ";
				nature 								= "location";
				location 							= "testTown";
				nextScene 							= "MainGame";
			break;
			default:
				obstacleType 						= " ";
				nature 								= " ";
				location 							= " ";
				nextScene 							= " ";
			break;
		}
	}
}
