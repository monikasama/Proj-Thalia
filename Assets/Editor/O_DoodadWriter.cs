using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class O_DoodadWriter : MonoBehaviour {

	[MenuItem ("Map Editor/Overworld/Write Doodads")]
	static void _writeDoodads(){
		GameObject parent = GameObject.Find("_O_DOODADS");
		Transform child;
		int childCount = parent.transform.childCount - 1;
		using(StreamWriter sw = new StreamWriter("Doodads.txt")){
			for(int lp = 0; lp <= childCount; lp++){
				child = parent.transform.GetChild(lp);
				sw.WriteLine("O_ControlDoodad.I._createDoodad(***" + child.name + "***, "
					+ child.transform.position.x.ToString() + "f, " + child.transform.position.y.ToString() + "f, "
					+ child.transform.localScale.x.ToString() + "f, " + child.transform.localScale.y.ToString() + "f, "
					+ child.transform.position.z.ToString() + "f, " + child.transform.localEulerAngles.z + "f);");
			}
		}
		Debug.Log("Successfully written all overworld doodads!");
	}
}
