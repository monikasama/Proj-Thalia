using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_Globals : MonoBehaviour {
	public static O_Globals I;
	public void Awake(){ I = this; }

	public List<O_ClassTerrain> terrains, terrainsTemp;
	public List<O_ClassUnit> units, unitsTemp;
	public List<O_ClassDoodad> doodads, doodadsTemp;

	// Map data
	public int map_maxX, map_maxY;
	public string curMap;

	public int prof;
	
	public void _start(){
		// Initialize lists
		terrains 							= new List<O_ClassTerrain>();
		terrainsTemp 						= new List<O_ClassTerrain>();
		units								= new List<O_ClassUnit>();
		unitsTemp							= new List<O_ClassUnit>();
		doodads 							= new List<O_ClassDoodad> ();
		doodadsTemp							= new List<O_ClassDoodad> ();

		// Variables
		prof								= ZPlayerPrefs.GetInt("Profile");
	}
}
