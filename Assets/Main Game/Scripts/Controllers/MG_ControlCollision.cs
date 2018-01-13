using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlCollision : MonoBehaviour {
	public static MG_ControlCollision I;
	public void Awake(){ I = this; }
	public List<string[]> handledCollisions;

	private bool debug;

	public void _start(){
		handledCollisions = new List<string[]> ();			// index 0 is hitted, index 1 is hitted
		debug = false;
	}

	public void _clearHandledCollisions(){ if(debug && handledCollisions.Count > 0) Debug.Log ("Clearing collisions...");
		handledCollisions.Clear ();
	}

	// When collision triggers from a "MG_OnTriggerEnter" component,
	// the gameObject owner of the component and the gameObject of
	// the colliding component is passed to a _collisionHandler function,
	// depending on what the tags between the gameObjets are
	#region "Collision Handler"
	public void _collisionHandler(GameObject hitter, GameObject hitted){
		if(debug) Debug.Log ("Handling collision...");
		// Check if this collision is already handled for this frame
		bool isHandled = false;
		foreach(string[] sL in handledCollisions){
			if ((sL [0] == hitter.name || sL [1] == hitter.name) && (sL [0] == hitted.name || sL [1] == hitted.name)) {
				isHandled = true;
				if(debug) Debug.Log ("This collision is already handled, returning...");
				break;
			}
		}
		if(debug) Debug.Log("This collision is not yet handled, checking collision properties...");
		if(debug) Debug.Log ("hitter's tag is =" + hitter.tag + ", hitted's tag is =" + hitted.tag);
		if (isHandled)		return;
		// Check the tags
		// Unit to Unit

		if (hitter.tag == "Unit" && hitted.tag == "Unit")
			_collisionHandler_UnitToUnit (hitter, hitted);
		// Unit to Missile
		else if (hitter.tag == "Missile" && hitted.tag == "Unit")
			_collisionHandler_UnitToMissile (hitted, hitter);
		else if (hitter.tag == "Unit" && hitted.tag == "Missile")
			_collisionHandler_UnitToMissile (hitter, hitted);
		// Missile to Missile
		else if (hitter.tag == "Missile" && hitted.tag == "Missile")
			_collisionHandler_MissileToMissile (hitter, hitted);
		// Missile to Terrain
		else if (hitter.tag == "Missile" && hitted.tag == "Terrain") 
			_collisionHandler_MissileToTerrain (hitter, hitted);
		else if (hitter.tag == "Terrain" && hitted.tag == "Missile")
			_collisionHandler_MissileToTerrain (hitted, hitter);
	}

	public void _collisionHandler_UnitToUnit(GameObject hitterObj, GameObject hittedObj){
		// Check the MG_ClassUnit owner of the GameObjects
		MG_ClassUnit hitter = MG_Globals.I.units[0]; bool hasHitter = false;
		MG_ClassUnit hitted = MG_Globals.I.units[0]; bool hasHitted = false;
		foreach(MG_ClassUnit cL in MG_Globals.I.units){
			if (cL.sprite == hittedObj) {
				hitted = cL; hasHitted = true;
			}
			else if(cL.sprite == hitterObj){
				hitter = cL; hasHitter = true;
			}

			if (hasHitter && hasHitted)			break;
		}

		// If one of the class does not exist, cancel the collision
		if (!hasHitter || !hasHitted)		return;

		// Mark this collision as already handled
		string[] hC = new string[]{hitterObj.name, hittedObj.name};
		handledCollisions.Add(hC);

		if(debug) Debug.Log ("Collision between units success!");
	}

	public void _collisionHandler_UnitToMissile(GameObject unitObj, GameObject missileObj){
		// Check the MG_ClassUnit owner of the GameObjects
		MG_ClassUnit unit = MG_Globals.I.units[0]; 				bool hasUnit = false;
		MG_ClassMissile missile = MG_Globals.I.missiles[0]; 	bool hasMissile = false;
		foreach(MG_ClassUnit cL in MG_Globals.I.units){
			if (cL.sprite == unitObj) {
				unit = cL; hasUnit = true; if(debug) Debug.Log ("Unit found...");
				break;
			}
		}
		foreach(MG_ClassMissile cL in MG_Globals.I.missiles){
			if (cL.sprite == missileObj) {
				missile = cL; hasMissile = true; if(debug) Debug.Log ("Missile found...");
				break;
			}
		}

		// If one of the class does not exist, cancel the collision
		if (!hasUnit || !hasMissile){
			if (!hasUnit)
				if(debug) Debug.Log ("Unit not found, returning...");
			if (!hasMissile)
				if(debug) Debug.Log ("Missile not found, returning...");
			return;
		}

		// Mark this collision as already handled
		string[] hC = new string[]{unitObj.name, missileObj.name};
		handledCollisions.Add(hC);

		if(debug) Debug.Log ("Collision between unit and missile success!" + Time.frameCount.ToString());
		_collisionEvent_UnitToMissile (unit, missile);
	}

	public void _collisionHandler_MissileToMissile(GameObject hitterObj, GameObject hittedObj){
		// Check the MG_ClassUnit owner of the GameObjects
		MG_ClassMissile hitter = MG_Globals.I.missiles[0]; bool hasHitter = false;
		MG_ClassMissile hitted = MG_Globals.I.missiles[0]; bool hasHitted = false;
		foreach(MG_ClassMissile cL in MG_Globals.I.missiles){
			if (cL.sprite == hittedObj) {
				hitted = cL; hasHitted = true;
			}
			else if(cL.sprite == hitterObj){
				hitter = cL; hasHitter = true;
			}

			if (hasHitter && hasHitted)			break;
		}

		// If one of the class does not exist, cancel the collision
		if (!hasHitter || !hasHitted)		return;

		// Mark this collision as already handled
		string[] hC = new string[]{hitterObj.name, hittedObj.name};
		handledCollisions.Add(hC);

		if(debug) Debug.Log ("Collision between missiles success!");
	}

	public void _collisionHandler_MissileToTerrain(GameObject missileObj, GameObject terrainObj){
		// Check the MG_ClassUnit owner of the GameObjects
		MG_ClassMissile missile = MG_Globals.I.missiles[0]; bool hasMissile = false, collidesToWalls = false;
		foreach(MG_ClassMissile cL in MG_Globals.I.missiles){
			if (cL.sprite == missileObj) {
				collidesToWalls = cL.collideToWalls;
				if (!collidesToWalls)
					break;

				missile = cL; hasMissile = true;
				break;
			}
		}
		if (!collidesToWalls) 	return;

		// If one of the class does not exist, cancel the collision
		if (!hasMissile)		return;

		// Mark this collision as already handled
		string[] hC = new string[]{missileObj.name, terrainObj.name};
		handledCollisions.Add(hC);

		if(debug) Debug.Log ("Collision between missile and terrain success!");
		_collisionEvent_MissileToTerrain (missile, terrainObj);
	}
	#endregion

	// After being passed through to determine the collision's properties,
	// colliding objects will now go through these event calculators
	// to figure out the outcome of the collision.
	#region "Collision Event Calculator"
	public void _collisionEvent_UnitToUnit(MG_ClassUnit hitter, MG_ClassUnit hitted){
		
	}

	public void _collisionEvent_UnitToMissile(MG_ClassUnit unit, MG_ClassMissile missile){
		switch (missile.type) {
			case "test":
				bool isHit = false;
				if (MG_ControlPlayer.I._getIsEnemy (missile.playerOwner, unit.owner)) {
					isHit = _collisionCondition_UnitToMissile_Enemies (unit, missile);
				}

				if (isHit) {
					MG_ClassUnit missileOwner = MG_GetUnit.I._getUnitFromID (missile.ownerID);
					MG_CalcDamage.I._damageUnit (missileOwner, unit, 1);
					MG_ControlMissile.I._addToDestroyList (missile);
				}
			break;
		}
	}

	public void _collisionEvent_MissileToMissile(MG_ClassMissile hitter, MG_ClassMissile hitted){
		
	}

	public void _collisionEvent_MissileToTerrain(MG_ClassMissile missile, GameObject terrain){
		switch (missile.type) {
			case "test":
				MG_ControlMissile.I._addToDestroyList (missile);
			break;
		}
	}
	#endregion

	#region "Collision conditions"
	/// <summary>
	/// Returns true if collision can occur.
	/// </summary>
	public bool _collisionCondition_UnitToMissile_Enemies(MG_ClassUnit unit, MG_ClassMissile missile){
		bool retVal = true;

		// Confirm the existence of missile's owner
		if(!MG_GetUnit.I._doesUnitExist(missile.ownerID))			return false;

		return retVal;
	}
	#endregion
}
