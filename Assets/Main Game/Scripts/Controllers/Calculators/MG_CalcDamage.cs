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
		Debug.Log (damaged.id + " takes damage =" + damageAmt);
	}

	/// <summary>
	/// Returns true if damage can be dealt
	/// </summary>
	public bool _damageUnit_Conditions(MG_ClassUnit damager, MG_ClassUnit damaged){
		bool retVal = true;

		return retVal;
	}
}
