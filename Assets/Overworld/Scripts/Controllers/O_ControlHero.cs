using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_ControlHero : MonoBehaviour {
	public static O_ControlHero I;
	public void Awake(){ I = this; }

	public float MOVE_SPEED = 0.05f;							// 0.05f
																// 0.1f for main gameplay

	public O_ClassUnit hero;

	public void _start(){
		int prof = MG_Globals.I.prof;

		int posX = PlayerPrefs.GetInt ("O_LastX_" + prof.ToString ()),
			posY = PlayerPrefs.GetInt ("O_LastY_" + prof.ToString ());
		string facing = PlayerPrefs.GetString("O_LastFacing_" + prof.ToString());

		// Spawns the hero
		O_ControlUnit.I._createUnit ("testYou", posX, posY);
		hero = O_GetUnit.I._getLastCreatedUnit();
		hero.facing = facing;
		O_ControlCamera.I._reposition(hero.sprite.transform.position.x, hero.sprite.transform.position.y);

		// Hero stats
		hero.moveType = "land";

		/////// Misc ////////
		// Set the hero's gameobject name to MainHero
		hero.sprite.name = "MainHero";
	}

	#region "Hero Movement"
	public bool isMoving, isPaused;
	public float moveAngle, movesLeft;

	public void _orderMoveHero(string direction){
		if(isMoving || isPaused) return;

		//Conditions:
		if(!_orderMoveHero_Conditions(direction))
			return;
		
		switch(direction){
			case "Right": moveAngle = 0; break;
			case "Left": moveAngle = 180; break;
			case "Up": moveAngle = 90; break;
			case "Down": moveAngle = 270; break;
		}
		hero.facing = direction;

		movesLeft = 1f;
		isMoving = true;
	}
	// Returns true if player can move
	private bool _orderMoveHero_Conditions(string direction){
		if(!_heroWalkCheck(direction)) 			return false;
		if(!_mapBorderCheck(direction))			return false;

		return true;
	}

	/// <summary>
	/// Switches map when the player reaches the border of the map.
	/// </summary>
	private bool _mapBorderCheck(string direction){
		bool output = true;
		switch(direction){
			case "Right": if(hero.posX >= O_Globals.I.map_maxX) 		output = false; break;
			case "Left": if(hero.posX <= -O_Globals.I.map_maxX) 		output = false; break;
			case "Up": if(hero.posY >= O_Globals.I.map_maxY) 			output = false; break;
			case "Down": if(hero.posY <= -O_Globals.I.map_maxY) 		output = false; break;
		}

		return output;
	}

	/// <summary>
	/// Returns true if hero can move to the target point.
	/// </summary>
	private bool _heroWalkCheck(string direction){
		int xOff = 0, yOff = 0;
		switch(direction){
			case "Right":					xOff = 1; yOff = 0; break;
			case "Left": 					xOff = -1; yOff = 0; break;
			case "Up": 						xOff = 0; yOff = 1; break;
			case "Down": 					xOff = 0; yOff = -1; break;
		}
		int xLoc = hero.posX + xOff, yLoc = hero.posY + yOff;

		if (!O_GetUnit.I._pointHasUnit (xLoc, yLoc))
			return true;

		O_ClassUnit obstacle = O_GetUnit.I._getUnitFromPoint (xLoc, yLoc);
		string moveType = hero.moveType, obsType = obstacle.obstacleType;

		// Checks
		if(moveType == "land" && (obsType == "boulder") )
			return false;

		return true;
	}

	public void _moveHero(){
		if(isMoving){
			hero.sprite.transform.position += new Vector3(MOVE_SPEED * Mathf.Cos((moveAngle*Mathf.PI)/180), 
				MOVE_SPEED * Mathf.Sin((moveAngle*Mathf.PI)/180), 0);
			O_ControlCamera.I._reposition(hero.sprite.transform.position.x, hero.sprite.transform.position.y);
			movesLeft -= MOVE_SPEED;

			//Hero arrived to destination
			if(movesLeft <= 0){
				switch(hero.facing){
					case "Right": hero.posX += 1; break;
					case "Left": hero.posX -= 1; break;
					case "Up": hero.posY += 1; break;
					case "Down": hero.posY -= 1; break;
				}
				hero.sprite.transform.position = new Vector3(hero.posX, hero.posY, hero.DEFAULT_Z_POSITION);
				isMoving = false;
				movesLeft = 0;

				//Events
				/*Enter town / region*/
				if (_heroEnterRegionCheck (hero)) 	return;
				/*Map border check*/
				if (_borderCheck (hero.facing)) 	return;
			}

		}
	}
	#endregion

	#region "Hero enters region"
	//Returns true if hero is entering a region
	private bool _heroEnterRegionCheck(O_ClassUnit hero){
		int prof = PlayerPrefs.GetInt("CurProfile");

		foreach(O_ClassUnit cL in O_Globals.I.units){ 
			if(cL.nature == "location"){
				if(cL.posX == hero.posX && cL.posY == hero.posY){
					O_ControlScene.I._setNextArea (cL.nextScene, cL.location, hero.facing);
					O_ControlScene.I._switchScene ();

					//Pause controls and moves the curtain
					isPaused = true;
					//O_ControlUI_Move.I._moveUI(1);
					return true;
				}
			}
		}

		return false;
	}
	#endregion

	// Includes
	//  - _borderCheck()			- Used to check if player is in a map border
	#region "Switch Overworld Map"
	private bool _borderCheck(string facing){
		if (!_mapBorderCheck (facing)) {
			string nextMap = O_ControlScene.I._getNextOverworldMap (facing);
			if (nextMap == "")
				return false;

			O_ControlScene.I._setNextArea ("Overworld", nextMap, facing);
			O_ControlScene.I._switchScene ();

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
		O_ControlCamera.I._reposition(hero.sprite.transform.position.x, hero.sprite.transform.position.y);
	}
	#endregion
}
