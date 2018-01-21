using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Globals : MonoBehaviour {
	public static MG_Globals I;
	public void Awake(){ I = this; }

	public List<MG_ClassTerrain> terrains, terrainsTemp;
	public List<MG_ClassUnit> units, unitsTemp;
	public List<MG_ClassDoodad> doodads, doodadsTemp;
	public List<MG_ClassMissile> missiles, missilesTemp;
	public List<MG_ClassPlayer> players;

	public GameObject editorParent;

	// Map data
	public int map_maxX, map_maxY;
	public string curMap;
	public int prof;

	public void _start(){
		// Initialize lists
		terrains 							= new List<MG_ClassTerrain>();
		terrainsTemp 						= new List<MG_ClassTerrain>();
		units								= new List<MG_ClassUnit>();
		unitsTemp							= new List<MG_ClassUnit>();
		doodads 							= new List<MG_ClassDoodad> ();
		doodadsTemp							= new List<MG_ClassDoodad> ();
		missiles 							= new List<MG_ClassMissile> ();
		missilesTemp 						= new List<MG_ClassMissile> ();

		players 							= new List<MG_ClassPlayer> ();	// Setted-up at MG_ControlPlayer.cs

		// Variables
		prof 								= ZPlayerPrefs.GetInt("Profile");
	}
}
