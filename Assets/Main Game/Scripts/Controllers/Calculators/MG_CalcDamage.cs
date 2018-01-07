using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_CalcDamage : MonoBehaviour {
	public static MG_CalcDamage I;
	public void Awake(){ I = this; }

	public void _damageUnit(MG_ClassUnit damager, MG_ClassUnit damaged, int damageAmt){
		if (!_damageUnit_Conditions (damager, damaged))
			return;

		damaged.HP -= damageAmt;

		// Kill
		bool kill = true;
		if(damaged.HP <= 0){

			if (kill) {
				damaged._kill ();
			}
		}
	}

	/// <summary>
	/// Returns true if damage can be dealt
	/// </summary>
	public bool _damageUnit_Conditions(MG_ClassUnit damager, MG_ClassUnit damaged){

		if (damaged.isDead) 		return false;

		return true;
	}
}
