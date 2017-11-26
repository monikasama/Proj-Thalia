using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlHero : MonoBehaviour {
	public static MG_ControlHero I;
	public void Awake(){ I = this; }

	public float MOVE_SPEED = 10f;

	public MG_ClassUnit hero;

	public void _start(){
		// Get Profile and entrance to be used
		int prof = ZPlayerPrefs.GetInt("Profile"),
			entranceUsed = PlayerPrefs.GetInt("EntranceUsed");
		MG_ClassUnit entrance = MG_Globals.I.unitsTemp[0];
		foreach (MG_ClassUnit uL in MG_Globals.I.unitsTemp) {
			if (uL.type == "entrance" && uL.entranceId == entranceUsed) {
				entrance = uL;
				break;
			}
		}

		int posX = (int)entrance.posX, posY = (int)entrance.posY;

		/////////////////////// PLAYER SPAWN /////////////////////////////////
		MG_ControlUnit.I._createUnit ("testYou", posX, posY, 1);
		hero = MG_GetUnit.I._getLastCreatedUnit();
		hero.facing = PlayerPrefs.GetString("PlayerFacing");
		MG_ControlCamera.I._reposition(hero.sprite.transform.position.x, hero.sprite.transform.position.y);
		/////////////////////// PLAYER SPAWN /////////////////////////////////

		// Hero stats
		hero.moveType = "land";

		/////// Misc ////////
		// Set the hero's gameobject name to MainHero
		hero.sprite.name = "MainHero";
	}

	#region "Hero Movement"
	public float moveAngle;

	public void _orderMoveHero(string direction){
		if(hero.state != "idle" && hero.state != "moving") return;

		//Conditions:
		if(!_orderMoveHerMG_Conditions(direction))
			return;

		switch(direction){
			case "Right": 		moveAngle = 0; break;
			case "Left": 		moveAngle = 180; break;
			case "Up": 			moveAngle = 90; break;
			case "Down": 		moveAngle = 270; break;
		}
		hero.facing = direction;
		hero.state = "moving";
	}
	// Returns true if player can move
	private bool _orderMoveHerMG_Conditions(string direction){
		if(!_mapBorderCheck(direction))			return false;

		return true;
	}

	/// <summary>
	/// Switches map when the player reaches the border of the map.
	/// </summary>
	private bool _mapBorderCheck(string direction){
		bool output = true;
		switch(direction){
			case "Right": if(hero.posX >= MG_Globals.I.map_maxX) 		output = false; break;
			case "Left": if(hero.posX <= -MG_Globals.I.map_maxX) 		output = false; break;
			case "Up": if(hero.posY >= MG_Globals.I.map_maxY) 			output = false; break;
			case "Down": if(hero.posY <= -MG_Globals.I.map_maxY) 		output = false; break;
		}

		return output;
	}

	public void _moveHero(){
		if(hero.state == "moving"){
			//Conditions:
			if(!_orderMoveHerMG_Conditions(hero.facing))
				return;
			
			hero._move_Increment (MOVE_SPEED * Mathf.Cos((moveAngle*Mathf.PI)/180), MOVE_SPEED * Mathf.Sin((moveAngle*Mathf.PI)/180));
			MG_ControlCamera.I._reposition(hero.sprite.transform.position.x, hero.sprite.transform.position.y);

			//Events
			/*Map border check*/
			if (_borderCheck (hero.facing)) 	return;
		}
	}

	public void _orderStopMoving(){
		hero._stopMoving ();
	}
	#endregion

	// Includes
	//  - _borderCheck()			- Used to check if player is in a map border, then switch to the associated map/scene
	#region "Switch Map"
	private bool _borderCheck(string facing){
		if (!_mapBorderCheck (facing)) {
			string[] nextMap = MG_ControlScene.I._getNextMap (facing);
			if (nextMap[0] == "" || nextMap[1] == "")
				return false;
			
			MG_ControlScene.I._setNextArea (nextMap[1], nextMap[0], facing);
			MG_ControlScene.I._switchScene ();

			return true;
		}

		return false;
	}
	#endregion

	// Includes
	//  - _instantMove()			- Instantly moves hero to target position
	#region "Misc"
	public void _instantMove(int tarPosX, int tarPosY){
		hero.posX = tarPosX;
		hero.posY = tarPosY;

		hero.sprite.transform.position = new Vector3 (tarPosX, tarPosY, hero.sprite.transform.position.z);
		MG_ControlCamera.I._reposition(hero.sprite.transform.position.x, hero.sprite.transform.position.y);
	}
	#endregion
}
