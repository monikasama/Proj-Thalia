using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGMAP_TestMap001 : MonoBehaviour {
	public static MGMAP_TestMap001 I;
	public void Awake(){ I = this; }

	public void _createMap(){
		#region "Test Terrain"
		bool isB = false;
		string newTer = "testA";
		for (int x = -100; x <= 100; x++) {
			for (int y = -100; y <= 100; y++) {
				newTer = (!isB) ? "testA" : "testB";
				MG_ControlTerrain.I._createTerrain (newTer, x, y);
				isB = !isB;
			}
		}
		#endregion
		#region "Units"

		// Entrances
		/*001*/MG_ControlUnit.I._createUnit("entrance", 0, 10, 4);
		#endregion

		// Map bounds
		MG_Globals.I.map_maxX = 100;
		MG_Globals.I.map_maxY = 100;
	}
}
