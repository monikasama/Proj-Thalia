using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class O_TerrainWriter : MonoBehaviour {

	[MenuItem ("Map Editor/Overworld/Write Terrain")]
	static void _writeUnits(){
		GameObject parent = GameObject.Find("_O_TERRAIN");
		Transform child;
		int childCount = parent.transform.childCount - 1;
		using(StreamWriter sw = new StreamWriter("Terrain.txt")){
			for(int lp = 0; lp <= childCount; lp++){
				child = parent.transform.GetChild(lp);

				sw.WriteLine("O_ControlTerrain.I._createTerrain(***" + child.name + "***, "
					+ child.transform.position.x.ToString() + ", " + child.transform.position.y.ToString() + ");");
			}
		}
		Debug.Log("Successfully written the new terrain!");
	}
}
