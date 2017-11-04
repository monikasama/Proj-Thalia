using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Maps : MonoBehaviour {
	public static MG_DB_Maps I;
	public void Awake(){ I = this; }

	public void _createMap(string mapName){
		MG_Globals.I.curMap = mapName;

		switch(mapName){
			case "testTown": 				MGMAP_Test.I._createMap (); break;
			case "mg_testTown_001": 		MGMAP_TestMap001.I._createMap (); break;
		}
	}
}
