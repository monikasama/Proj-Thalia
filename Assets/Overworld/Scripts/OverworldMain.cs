/*
 * 		OVERWORLD
 * 
 * 
 * 		Go to <Project Root> / Documentation / _O_TUTORIAL.txt for tutorials
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMain : MonoBehaviour {

	public int prof;
	public GameObject blackBorder;
	
	void Start () {
		// Z-Player prefs
		ZPlayerPrefs.Initialize("sqPrefEncrypt29845", "09164667352sss");

		// Initialize other scripts
		O_Globals.I._start();
		O_Inputs.I._start();
		O_ControlScene.I._start ();

		// Variables
		prof 				= ZPlayerPrefs.GetInt("Profile");

		// Create the map
		if(PlayerPrefs.GetInt("FirstMap") == 1)
			O_DB_Maps.I._createMap(ZPlayerPrefs.GetString("LastMap_" + prof.ToString()));
		else 
			O_DB_Maps.I._createMap(PlayerPrefs.GetString("NextMap"));

		// Create the black borders
		_createBorders();

		// Initialize controllers
		O_ControlHero.I._start();						// Also spawns the hero
	}

	private void _createBorders(){
		// Create the black borders
		float maxX = O_Globals.I.map_maxX, maxY = O_Globals.I.map_maxY, borWidth = 6, widthAdjust = 2.4f;
		GameObject borTop = GameObject.Instantiate(	blackBorder, Vector3.zero, Quaternion.identity) as GameObject;
		borTop.transform.localScale 	= new Vector3 (maxX * 5, borWidth, 0);
		borTop.transform.position 		= new Vector3 (0, maxY + widthAdjust, -200);
		GameObject borBot = GameObject.Instantiate(	blackBorder, Vector3.zero, Quaternion.identity) as GameObject;
		borBot.transform.localScale 	= new Vector3 (maxX * 5, borWidth, 0);
		borBot.transform.position 		= new Vector3 (0, -maxY - widthAdjust, -200);
		GameObject borLeft = GameObject.Instantiate(blackBorder, Vector3.zero, Quaternion.identity) as GameObject;
		borLeft.transform.localScale 	= new Vector3 (borWidth, maxY * 5, 0);
		borLeft.transform.position 		= new Vector3 (-maxX - widthAdjust, 0, -200);
		GameObject borRight = GameObject.Instantiate(blackBorder, Vector3.zero, Quaternion.identity) as GameObject;
		borRight.transform.localScale 	= new Vector3 (borWidth, maxY * 5, 0);
		borRight.transform.position 	= new Vector3 (maxX + widthAdjust, 0, -200);
	}

	void Update(){
		// Check input
		O_Inputs.I._checkPress();

		// Move hero
		O_ControlHero.I._moveHero();

		// Temp to main list
		_tempToMainList();

		// Destroy update
		_destroyUpdate();
	}

	#region "Temp to Main List"
	private void _tempToMainList(){
		// Units
		if (O_Globals.I.unitsTemp.Count > 0) {
			O_Globals.I.units.AddRange (O_Globals.I.unitsTemp);
			O_Globals.I.unitsTemp.Clear ();
		}

		// Terrain
		if (O_Globals.I.terrainsTemp.Count > 0) {
			O_Globals.I.terrains.AddRange (O_Globals.I.terrainsTemp);
			O_Globals.I.terrainsTemp.Clear ();
		}

		// Dooadads
		if (O_Globals.I.doodadsTemp.Count > 0) {
			O_Globals.I.doodads.AddRange (O_Globals.I.doodadsTemp);
			O_Globals.I.doodadsTemp.Clear ();
		}
	}
	#endregion
	#region "Destroy Update"
	void _destroyUpdate(){
		// Units
		if (O_ControlUnit.I.toDestroy.Count > 0)
			O_ControlUnit.I._destroyListed ();

		// Doodads
		if (O_ControlDoodad.I.toDestroy.Count > 0)
			O_ControlDoodad.I._destroyListed ();
	}
	#endregion
}
