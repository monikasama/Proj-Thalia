using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlPlayer : MonoBehaviour {
	public static MG_ControlPlayer I;
	public void Awake(){ I = this; }

	public void _setupPlayers(){
		// Setup Players
		MG_Globals.I.players.Add(new MG_ClassPlayer(1, new int[]{2}));				// Main Player
		MG_Globals.I.players.Add(new MG_ClassPlayer(2, new int[]{1}));				// Enemy
		MG_Globals.I.players.Add(new MG_ClassPlayer(3, new int[]{0}));				// NPC
		MG_Globals.I.players.Add(new MG_ClassPlayer(4, new int[]{0}));				// Path Blockers
	}

	// Includes
	//  - _getIsEnemy()							- Returns true if inputted players are hostile to each other
	#region "Get Relationships"
	public bool _getIsEnemy(int firstPlayerID, int secondPlayerID){Debug.Log(firstPlayerID + ", " + secondPlayerID);
		bool retVal = false;
		foreach (MG_ClassPlayer pL in MG_Globals.I.players) {
			if (pL.id == firstPlayerID) {
				if (pL.rel_enemies.Contains (secondPlayerID))     	retVal = true;
				else 												retVal = false;

				break;
			}
		}

		return retVal;
	}
	#endregion
}
