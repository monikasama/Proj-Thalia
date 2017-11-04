using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class O_UnitWriter : MonoBehaviour {

	[MenuItem ("Map Editor/Overworld/Write Units")]
	static void _writeUnits(){
		GameObject parent = GameObject.Find("_O_UNITS");
		Transform child;
		int childCount = parent.transform.childCount - 1;
		using(StreamWriter sw = new StreamWriter("Units.txt")){
			for(int lp = 0; lp <= childCount; lp++){
				child = parent.transform.GetChild(lp);
				if (child.name == "MainHero")
					continue;

				sw.WriteLine("O_ControlUnit.I._createUnit(***" + child.name + "***, "
					+ child.transform.position.x.ToString() + ", " + child.transform.position.y.ToString() + ");");
			}
		}
		Debug.Log("Successfully written all overworld units!");
	}
}
