using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_MissileValue : MonoBehaviour {
	public static MG_DB_MissileValue I;
	public void Awake(){ I = this; }

	public string obstacleType, nature;
	public float speed;

	public void _setValues(string newUnitType){
		switch (newUnitType) {
			case "test":
				speed 								= 20f;
			break;
		}
	}
}
