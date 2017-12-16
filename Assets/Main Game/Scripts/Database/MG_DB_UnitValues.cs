using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_UnitValues : MonoBehaviour {
	public static MG_DB_UnitValues I;
	public void Awake(){ I = this; }

	public string obstacleType, nature;

	public int HP, MP;
	public float moveSpeed;

	public void _setValues(string newUnitType){
		switch (newUnitType) {
			#region "Game dummies"
			case "pathBlocker":
				HP = 1000; MP = 1000;
				moveSpeed = 10f;
			break;
			#endregion
			#region "Test Units"
			case "testYou":
				HP = 10; MP = 10;
				moveSpeed = 10f;
			break;
			case "testEnemy":
				HP = 1; MP = 1;
				moveSpeed = 10f;
			break;
			#endregion
			default:
				obstacleType 						= " ";
				nature 								= " ";
			break;
		}
	}
}
