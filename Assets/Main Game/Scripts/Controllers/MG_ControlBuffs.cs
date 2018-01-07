using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlBuffs : MonoBehaviour {
	public static MG_ControlBuffs I;
	public void Awake(){ I = this; }

	public List<MG_ClassBuff> buffList;
	public List<int> toDestroy;

	public int buffCount = 0;

	public void _start(){
		buffList = new List<MG_ClassBuff> ();
		toDestroy = new List<int> ();
	}

	public void _update(){
		foreach (MG_ClassBuff bL in buffList) {
			bL._update ();
		}
	}

	#region "Create Buff"
	public void _addBuff(int targetUnitID, string buffType, float duration){
		MG_DB_Buff.I._setupNewBuff (buffType);

		if(MG_DB_Buff.I.hasSprite)
			buffList.Add(new MG_ClassBuff(MG_DB_Buff.I._getSprite(buffType), buffType, buffCount, targetUnitID, duration));
		else 
			buffList.Add(new MG_ClassBuff(targetUnitID, buffType, buffCount, duration));
	}
	#endregion

	#region "Destroy Codes"
	public void _addToDestroyList(MG_ClassBuff targetBuff){
		toDestroy.Add (targetBuff.id);
	}

	public void _destroyListed(){
		if (toDestroy.Count > 0) {
			for (int i = 0; i < toDestroy.Count; i++) {
				_destroyBuff (i);
			}
		}
		toDestroy.Clear ();
	}

	public void _destroyBuff(int targetBuffIndex){
		int indexToRemove = -1;
		for (int i = buffList.Count - 1; i >= 0; i--) {
			if (toDestroy [targetBuffIndex] == buffList [i].id) {
				indexToRemove = i;
				break;
			}
		}
		if (indexToRemove > -1) {
			if(buffList [indexToRemove].sprite)
				Destroy (buffList [indexToRemove].sprite);
			buffList.RemoveAt (indexToRemove);
		}
	}
	#endregion
}
