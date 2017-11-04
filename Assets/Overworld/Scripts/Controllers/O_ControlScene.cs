using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class O_ControlScene : MonoBehaviour {
	public static O_ControlScene I;
	public void Awake(){ I = this; }

	public string nextScene;
	private int prof;

	public void _start(){
		prof = ZPlayerPrefs.GetInt ("Profile");
	}

	/// <summary>
	/// Call _setNextArea() to set details first before calling this function.
	/// </summary>
	public void _switchScene(){
		SceneManager.LoadScene ("Transistor");
	}

	// Inludes
	//  - _setNextArea()					- Prepares the switching of scenes
	//	- _setNextArea_Overworld()			- If Overworld -> Overworld
	//	- _setNextArea_MainGame()			- If Overworld -> MainGame
	#region "Next Map Database"
	public void _setNextArea(string newScene, string locationName, string heroFacing){
		nextScene = newScene;
		ZPlayerPrefs.SetString ("LastScene_" + prof.ToString (), newScene);
		PlayerPrefs.SetString("NextScene", newScene);

		if (nextScene == "Overworld")
			_setNextArea_Overworld (newScene, locationName, heroFacing);
		else
			_setNextArea_MainGame (newScene, locationName, heroFacing);
	}

	private void _setNextArea_Overworld(string newScene, string nextMap, string heroFacing){
		// Switch to overworld map
		PlayerPrefs.SetString ("NextMap", nextMap);
		ZPlayerPrefs.SetString ("LastMap_" + prof.ToString (), nextMap);

		_polarizeHeroPosition (heroFacing);
	}

	private void _setNextArea_MainGame(string newScene, string nextMap, string heroFacing){
		switch (nextMap) {
			case "testTown":
				switch (heroFacing) {
					case "Up":
					case "Down":
					case "Left":
					case "Right":
						// Template
						PlayerPrefs.SetString ("NextMap", nextMap);
						ZPlayerPrefs.SetString ("LastMap_" + prof.ToString (), nextMap);

						PlayerPrefs.SetInt ("EntranceUsed", 1);
						PlayerPrefs.GetString("PlayerFacing", "Right");
					break;
				}
			break;
		}
	}
	#endregion

	#region "Get Next Overworld Map"
	public string _getNextOverworldMap(string facing){
		string curMap = O_Globals.I.curMap, nextMap = "";

		switch (curMap) {
			case "test":
				if (facing == "Up") 				nextMap = "testTown_001";
			break;
		}
		Debug.Log (curMap + ", " + nextMap);
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
		O_ClassUnit hero = O_ControlHero.I.hero;

		if (facing == "Up" || facing == "Down") {
			PlayerPrefs.SetInt ("O_LastX_" + profile.ToString (), hero.posX);
			PlayerPrefs.SetInt ("O_LastY_" + profile.ToString (), hero.posY * -1);
		}
		else if (facing == "Left" || facing == "Right") {
			PlayerPrefs.SetInt ("O_LastX_" + profile.ToString (), hero.posX * -1);
			PlayerPrefs.SetInt ("O_LastY_" + profile.ToString (), hero.posY);
		}
	}
	#endregion
}
