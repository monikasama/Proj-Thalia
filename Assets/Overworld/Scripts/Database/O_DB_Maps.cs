using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_DB_Maps : MonoBehaviour {
	public static O_DB_Maps I;
	public void Awake(){ I = this; }

	public void _createMap(string mapName){
		O_Globals.I.curMap = mapName;

		switch(mapName){
			case "test": 				OMAP_Test.I._createMap (); break;
			case "testTown_001": 		OMAP_TestMap001.I._createMap (); break;
		}
	}
}
