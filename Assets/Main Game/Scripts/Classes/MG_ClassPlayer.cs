using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MG_ClassPlayer {

	// Relationship between other players
	public List<int> rel_enemies;			// Players not in rel_enemies are friends
	public int id;

	public MG_ClassPlayer(int thisPlayerID, int[] enemyPlayers){
		rel_enemies = new List<int> ();
		rel_enemies.AddRange (enemyPlayers.ToList());
		id = thisPlayerID;
	}
}
