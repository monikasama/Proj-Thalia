using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Buff : MonoBehaviour {
	public static MG_DB_Buff I;
	public void Awake(){ I = this; }

	public GameObject spr_none, spr_reloading;

	public bool hasSprite;
	public float spr_offsetX, spr_offsetY;

	public void _setupNewBuff(string buffName){
		switch (buffName) {
			case "Reloading":
				hasSprite = true;

				spr_offsetX = 0;
				spr_offsetY = 1f;
			break;
		}
	}

	public GameObject _getSprite(string buffName){
		GameObject sprite = GameObject.Instantiate (spr_none, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
		Destroy (sprite);
		switch (buffName) {
			case "Reloading": sprite = GameObject.Instantiate (spr_reloading, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
				
			default: sprite = GameObject.Instantiate (spr_none, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}

		return sprite;
	}
}
