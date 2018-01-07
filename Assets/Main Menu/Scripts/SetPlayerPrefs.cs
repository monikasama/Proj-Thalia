using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPrefs : MonoBehaviour {
	public static SetPlayerPrefs I;
	public void Awake(){ I = this; }

	public void _setPlayerPrefs(){
		PlayerPrefs.SetString("NextMap", "test");

		// Non-profiled prefs
		PlayerPrefs.SetInt ("FirstRun", 1);
		PlayerPrefs.SetInt ("EntranceUsed", 1);
		PlayerPrefs.SetInt ("WeaponSelected", 0);

		// Profiled prefs
		for (int i = 1; i <= 10; i++) {
			PlayerPrefs.SetInt ("O_LastX_" + i.ToString (), 0);
			PlayerPrefs.SetInt ("O_LastY_" + i.ToString (), 0);
			PlayerPrefs.GetString ("O_LastFacing_" + i.ToString (), "Down");

			ZPlayerPrefs.SetString ("LastMap_" + i.ToString (), "test");
			ZPlayerPrefs.SetString ("LastScene_" + i.ToString (), "Overworld");

			ZPlayerPrefs.SetInt ("Weapon1_" + i.ToString (), 1);
			ZPlayerPrefs.SetInt ("Weapon2_" + i.ToString (), 0);
			ZPlayerPrefs.SetInt ("Weapon3_" + i.ToString (), 0);
			ZPlayerPrefs.SetInt ("Weapon4_" + i.ToString (), 0);
			ZPlayerPrefs.SetInt ("Weapon5_" + i.ToString (), 0);
			ZPlayerPrefs.SetInt ("Weapon6_" + i.ToString (), 0);

			PlayerPrefs.SetInt("AmmoHandgunA_" + i.ToString(), 50);
			PlayerPrefs.SetInt("AmmoHandgunB_" + i.ToString(), 0);
			PlayerPrefs.SetInt("AmmoRifleA_" + i.ToString(), 0);
			PlayerPrefs.SetInt("AmmoRifleB_" + i.ToString(), 0);
			PlayerPrefs.SetInt("AmmoSpecialA_" + i.ToString(), 0);
			PlayerPrefs.SetInt("AmmoSpecialB_" + i.ToString(), 0);

			PlayerPrefs.SetInt("AmmoInHandgun_" + i.ToString(), 6);
			PlayerPrefs.SetInt("AmmoInRifle_" + i.ToString(), 0);
		}
	}
}
