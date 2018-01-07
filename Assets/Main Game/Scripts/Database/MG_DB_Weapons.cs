using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Weapons : MonoBehaviour {
	public static MG_DB_Weapons I;
	public void Awake(){ I = this; }

	////////////////////// WEAPON SPRITES //////////////////////
	/*
		This component is attached in "_HERO"
	*/
	public Sprite weap_none, weap_test;

	public Sprite _getSprite(string weaponName){
		Sprite output = weap_none;
		switch (weaponName) {
			case "testWeapon": 						output = weap_test; break;
		}

		return output;
	}

	////////////////////// WEAPON VALUES //////////////////////

	public string _getWeaponType(int weaponNumber){
		string output = "NONE";
		switch (weaponNumber) {
			case 1: 								output = "testWeapon"; break;
		}
		return output;
	}

	// Switch currently holding weapon
	public void _switchWeapon(string newWeapon){
		switch (newWeapon) {
			case "testWeapon":
				MG_ControlWeapon.I.hero_newRecoveryTime = 0.25f;
				MG_ControlWeapon.I.hero_newReloadTime = 0.8f;
				MG_HeroWeapons.I.ammo_inHandgun_MAX = 6;
				MG_HeroWeapons.I.auto_isAutomatic = true;
				MG_HeroWeapons.I.ammoType = "HandgunA";
			break;
			default:
				MG_ControlWeapon.I.hero_newRecoveryTime = 0;
				MG_ControlWeapon.I.hero_newReloadTime = 0;
				MG_HeroWeapons.I.auto_isAutomatic = false;
				MG_HeroWeapons.I.ammoType = "NONE";
			break;
		}
	}
}
