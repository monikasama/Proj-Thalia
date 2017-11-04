using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_DoodadValues : MonoBehaviour {
	public static MG_DB_DoodadValues I;
	public void Awake(){ I = this; }

	public bool _getDoodIsCorner(string doodName){
		bool retVal = false;

		switch(doodName){
			case "GrassCorner1":
				retVal = true;
			break;
		}

		return retVal;
	}
}
