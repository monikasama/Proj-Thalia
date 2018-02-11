using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_MissileValue : MonoBehaviour {
	public static MG_DB_MissileValue I;
	public void Awake(){ I = this; }

	public string obstacleType, nature;
	public float speed;
	public bool collideToWalls;

	// missile to missile collision / missile block
	public bool mB_blockMissile;
	public int mB_blockValue;

	public void _setValues(string newUnitType){
		switch (newUnitType) {
			case "test":
				speed 									= 20f;
				collideToWalls 							= true;
				mB_blockMissile 						= true;
				mB_blockValue 							= 1;
			break;
			default:
				speed 									= 20f;
				collideToWalls 							= false;
				mB_blockMissile 						= false;
				mB_blockValue 							= 0;
			break;
		}
	}
}
