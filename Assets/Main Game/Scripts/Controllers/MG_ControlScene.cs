using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MG_ControlScene : MonoBehaviour {
	public static MG_ControlScene I;
	public void Awake(){ I = this; }

	public string nextScene;

	/// <summary>
	/// Call _setNextArea() to set details first before calling this function.
	/// </summary>
	public void _switchScene(){
		PlayerPrefs.SetString("NextScene", nextScene);
		SceneManager.LoadScene ("Transistor");
	}

	// Inludes
	//  - _setNextArea()					- Prepares the switching of scenes
	//	- _setNextArea_Overworld()			- If Overworld -> Overworld
	//	- _setNextArea_MainGame()			- If Overworld -> MainGame
	#region "Next Map Database"
	public void _setNextArea(string newScene, string locationName, string heroFacing){
		nextScene = newScene; Debug.Log ("Switching map");

		if (nextScene == "Overworld")
			_setNextArea_Overworld (locationName, heroFacing);
		else
			_setNextArea_MainGame (locationName, heroFacing);
	}

	private void _setNextArea_Overworld(string nextMap, string heroFacing){
		// Switch to overworld map
		PlayerPrefs.SetString ("NextMap", nextMap);
		_polarizeHeroPosition (heroFacing);
	}

	private void _setNextArea_MainGame(string locationName, string heroFacing){
		// Template
		PlayerPrefs.SetString ("NextMap", locationName);
		PlayerPrefs.SetString("PlayerFacing", heroFacing);
	}
	#endregion

	// Gets the next map when player is going through a border (from main game to main game/overworld)
	#region "Get Next Map"
	public bool isSwitching = false;
	public string[] _getNextMap(string facing){
		if (isSwitching) {
			string[] nullVal = new string[2];
			return nullVal;
		}

		int prof = MG_Globals.I.prof;

		string curMap = MG_Globals.I.curMap;
		string[] nextMap = new string[2];
		nextMap [0] = ""; nextMap [1] = "";

		switch (curMap) {
			case "testTown":
				if (facing == "Right"){
					nextMap[0] = "mg_testTown_001";
					nextMap[1] = "MainGame";
					PlayerPrefs.SetInt("EntranceUsed", 1);
					PlayerPrefs.SetString("PlayerFacing", "Down");
				}
				else if (facing == "Up"){
					nextMap[0] = "test";
					nextMap[1] = "Overworld";
					PlayerPrefs.SetInt ("O_LastX_" + prof.ToString (), 5);
					PlayerPrefs.SetInt ("O_LastY_" + prof.ToString (), 7);
					PlayerPrefs.SetString("O_LastFacing_" + prof.ToString (), "Down");
				}
			break;
		}
		Debug.Log (curMap + ", going to =" + nextMap[0] + ", at scene =" + nextMap[1]);
		isSwitching = true;

		return nextMap;
	}
	#endregion

	// Includes
	//  - _polarizeHeroPosition				- Switch hero position when switching maps
	//										- (10, 0) becomes (-10, 0) 
	#region "Misc"
	/// <summary>
	/// Switch hero position when switching maps, i.e. (10, 0) becomes (-10, 0).
	/// Only use this when the player reaches the edge of an overworld map.
	/// </summary>
	private void _polarizeHeroPosition(string facing){
		int profile = ZPlayerPrefs.GetInt ("Profile");
		MG_ClassUnit hero = MG_ControlHero.I.hero;
	}
	#endregion
}
