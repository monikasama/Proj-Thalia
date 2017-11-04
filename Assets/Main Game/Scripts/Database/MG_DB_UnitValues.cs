using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_UnitValues : MonoBehaviour {
	public static MG_DB_UnitValues I;
	public void Awake(){ I = this; }

	public string obstacleType, nature;

	public void _setValues(string newUnitType){
		switch (newUnitType) {
			case "pathBlocker":
				obstacleType 						= "boulder";
				nature 								= " ";
			break;
			case "testTown":
				obstacleType 						= " ";
				nature 								= "location";
			break;
			default:
				obstacleType 						= " ";
				nature 								= " ";
			break;
		}
	}
}
