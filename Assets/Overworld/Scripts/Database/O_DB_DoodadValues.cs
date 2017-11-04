using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_DB_DoodadValues : MonoBehaviour {
	public static O_DB_DoodadValues I;
	public void Awake(){ I = this; }

	public bool _getDoodIsCorner(string doodName){
		bool retVal = false;

		switch(doodName){
			case "GrassCorner1":
			case "GrassCorner2":
			case "SeaCorner1_x1":
			case "SeaCorner1_x2":
			case "SeaCorner1_x4":
			case "SeaCorner1_x8":
			case "SeaCorner2":
			case "SeaCorner3":
			case "DeepSeaCorner1_x1":
			case "DeepSeaCorner1_x2":
			case "DeepSeaCorner1_x4":
			case "DeepSeaCorner2":
			case "DeepSeaCorner3":
			case "roadCorner1_x1":
			case "roadCorner1_x2":
			case "roadCorner1_x4":
			case "roadCorner1_x8":
			case "roadCorner2":
			case "roadCorner3":
			case "roadCorner4":
			case "roadCorner5":
			case "roadCorner6":
			case "blackGrassCorner1_x1":
			case "blackGrassCorner1_x2":
			case "blackGrassCorner1_x4":
			case "blackGrassCorner1_x8":
			case "blackGrassCorner2":
			case "blackGrassCorner3":
				retVal = true;
			break;
		}

		return retVal;
	}
}
