/*
 * 		MAIN GAME
 * 
 * 
 * 		Go to <Project Root> / Documentation / _MG_TUTORIAL.txt for tutorials
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

	public GameObject blackBorder;

	void Start () {
		// Z-Player prefs
		ZPlayerPrefs.Initialize("sqPrefEncrypt29845", "09164667352sss");

		// Initialize other scripts
		MG_Globals.I._start();
		MG_Inputs.I._start();
		MG_ControlPlayer.I._setupPlayers ();
		MG_ControlBuffs.I._start ();
		MG_HeroWeapons.I._setupWeapons ();
		MG_ControlWeapon.I._start ();
		MG_ControlCollision.I._start ();
		/*UI*/MG_UI_HeroWeapons.I._start ();
		/*UI - hero HP bar UI*/MG_UI_HeroBars.I._start ();
		/*UI - dialog*/MG_UI_Dialog.I._start ();

		// Create the map
		MG_DB_Maps.I._createMap(PlayerPrefs.GetString("NextMap"));

		// Create the black borders
		_createBorders();

		// Initialize controllers
		MG_ControlHero.I._start();						// Also spawns the hero

		MG_ControlUnit.I._createUnit ("testEnemy", 0, -4, 2);

		MG_DB_Dialog.I._setupDialogText (1);
		MG_UI_Dialog.I._initDialog (1);
	}

	private void _createBorders(){
		// Create the black borders
		float maxX = MG_Globals.I.map_maxX, maxY = MG_Globals.I.map_maxY, borWidth = 6, widthAdjust = 2.4f;
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
		/*Check input*/								MG_Inputs.I._checkPress();
		/*Move hero*/								MG_ControlHero.I._moveHero();
		/*Temp to main list*/						_tempToMainList();
		/*Update objects (position, etc...)*/		_updateObjects ();		
		/*Destroy update*/							_destroyUpdate();
		/*Clear collisions*/						MG_ControlCollision.I._clearHandledCollisions ();
		/*Reload update*/							MG_HeroWeapons.I._reloadUpdate ();
		/*Update hero HP bar UI*/					MG_UI_HeroBars.I._update ();
		/*Dialog update*/							MG_UI_Dialog.I._drawDialogFrame ();
		/*Dialog input*/							MG_UI_Dialog.I._pressConfirm ();
	}

	#region "Update Objects"
	private void _updateObjects(){
		// Units
		for (int i = 0; i < MG_Globals.I.units.Count; i++) {
			MG_Globals.I.units [i]._update ();
		}

		// Missiles
		for (int i = 0; i < MG_Globals.I.missiles.Count; i++) {
			MG_Globals.I.missiles [i]._update ();
		}

		// Buffs
		for (int i = 0; i < MG_ControlBuffs.I.buffList.Count; i++) {
			MG_ControlBuffs.I.buffList [i]._update ();
		}
	}
	#endregion
	#region "Temp to Main List"
	private void _tempToMainList(){
		// Units
		if (MG_Globals.I.unitsTemp.Count > 0) {
			MG_Globals.I.units.AddRange (MG_Globals.I.unitsTemp);
			MG_Globals.I.unitsTemp.Clear ();
		}

		// Terrain
		if (MG_Globals.I.terrainsTemp.Count > 0) {
			MG_Globals.I.terrains.AddRange (MG_Globals.I.terrainsTemp);
			MG_Globals.I.terrainsTemp.Clear ();
		}

		// Dooadads
		if (MG_Globals.I.doodadsTemp.Count > 0) {
			MG_Globals.I.doodads.AddRange (MG_Globals.I.doodadsTemp);
			MG_Globals.I.doodadsTemp.Clear ();
		}

		// Missiles
		if (MG_Globals.I.missilesTemp.Count > 0) {
			MG_Globals.I.missiles.AddRange (MG_Globals.I.missilesTemp);
			MG_Globals.I.missilesTemp.Clear ();
		}
	}
	#endregion
	#region "Destroy Update"
	void _destroyUpdate(){
		/*Units*/					MG_ControlUnit.I._destroyListed ();
		/*Doodads*/					MG_ControlDoodad.I._destroyListed ();
		/*Missiles*/				MG_ControlMissile.I._destroyListed ();
		/*Buffs*/					MG_ControlBuffs.I._destroyListed ();
	}
	#endregion
}
