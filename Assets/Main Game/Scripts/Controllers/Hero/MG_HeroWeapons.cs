using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 			This is the controller for main hero's weapons.
 * 			UI control for weapons are in UI / MG_UI_HeroWeapons.cs
 * 			Controller for all weapons in general (including enemies) goes in MG_ControlWeapon.cs 
 */

public class MG_HeroWeapons : MonoBehaviour {
	public static MG_HeroWeapons I;
	public void Awake(){ I = this; }

	public int prof;

	// Weapons
	public string selectedWeapon;
	public int selectedNum;
	public List<string> weapons;
	// Used for automatic / semi-automatic weapons
	public bool auto_isAutomatic;

	// Ammo
	public int ammo_handgunA, ammo_handgunB, ammo_rifleA, ammo_rifleB, ammo_specialA, ammo_specialB;
	public int ammo_inHandgun, ammo_inRifle, ammo_inHandgun_MAX, ammo_inRifle_MAX;
	public string ammoType;

	// Weapon usage stats
	public bool useWeaponPressed = false, isReloading = false;
	public float recoveryTime = 0, reloadTime = 0;

	public void _setupWeapons(){
		prof = ZPlayerPrefs.GetInt("Profile");

		// Ammo
		ammo_handgunA 		= PlayerPrefs.GetInt("AmmoHandgunA_" + prof.ToString ());
		ammo_handgunB 		= PlayerPrefs.GetInt("AmmoHandgunB_"+ prof.ToString ());
		ammo_rifleA 		= PlayerPrefs.GetInt("AmmoRifleA_" + prof.ToString ());
		ammo_rifleB 		= PlayerPrefs.GetInt("AmmoRifleB_" + prof.ToString ());
		ammo_specialA 		= PlayerPrefs.GetInt("AmmoSpecialA_" + prof.ToString ());
		ammo_specialB 		= PlayerPrefs.GetInt("AmmoSpecialB_" + prof.ToString ());
		ammo_inHandgun 		= PlayerPrefs.GetInt("AmmoInHandgun_" + prof.ToString ());
		ammo_inRifle 		= PlayerPrefs.GetInt("AmmoInRifle_" + prof.ToString ());

		// Weapons are stored in player prefs in number form (1, 2, etc...)
		// This will take the weapons' name from MG_DB_Weapons.cs based from the weapon's number
		weapons = new List<string> ();
		for (int i = 1; i <= 6; i++)
			weapons.Add (MG_DB_Weapons.I._getWeaponType (ZPlayerPrefs.GetInt ("Weapon" + i.ToString () + "_" + prof.ToString ())));

		selectedNum = PlayerPrefs.GetInt ("WeaponSelected");
		selectedWeapon = weapons[selectedNum];
		MG_DB_Weapons.I._switchWeapon (selectedWeapon);
	}

	#region "Use Weapon"
	public void _weaponTrigger_On(){
		if(auto_isAutomatic && recoveryTime > 0)	recoveryTime -= Time.deltaTime;

		if (!_canUseWeapon ()) 				return;

		MG_ControlWeapon.I._useWeapon (MG_ControlHero.I.hero, selectedWeapon);
		recoveryTime = MG_ControlWeapon.I.hero_newRecoveryTime;
		if (!auto_isAutomatic) {
			useWeaponPressed = true;
		} else{
			
		}
	}

	public void _weaponTrigger_Off(){
		if (recoveryTime > 0) {
			recoveryTime -= Time.deltaTime;
		}

		useWeaponPressed = false;
	}

	// Returns true if player can use a weapon
	public bool _canUseWeapon(){
		if (recoveryTime > 0)								return false;
		if (reloadTime > 0) 								return false;
		if (useWeaponPressed && !auto_isAutomatic) 			return false;

		return true;
	}
	#endregion

	#region "Switch Weapon"
	// Switch currently holding weapon
	public void _switchWeapon(int increment){
		if (reloadTime > 0) return;

		selectedNum += increment;
		if (selectedNum < 0) 						selectedNum = 5;
		else if (selectedNum > 5) 					selectedNum = 0;

		selectedWeapon = weapons[selectedNum];
		MG_DB_Weapons.I._switchWeapon (selectedWeapon);
		PlayerPrefs.SetInt ("WeaponSelected", selectedNum);

		MG_UI_HeroWeapons.I._changeEquippedWeapon (selectedNum);

		#region "Auto-reload"
		if (selectedNum == 0) {
			if (ammo_inHandgun <= 0)
				if ((ammoType == "HandgunA" && ammo_handgunA > 0) || (ammoType == "HandgunB" && ammo_handgunB > 0))
					MG_ControlWeapon.I._reloadWeapon (ammoType);
		} else if (selectedNum == 1) {
			if (ammo_inRifle <= 0)
				if ((ammoType == "RifleA" && ammo_rifleA > 0) || (ammoType == "RifleB" && ammo_rifleB > 0))
					MG_ControlWeapon.I._reloadWeapon (ammoType);
		}
		#endregion
	}
   	#endregion

	#region "Reload Weapon"
	// This is the update function. Reload start can be found at MG_ControlWeapons.cs
	public void _reloadUpdate(){
		if (isReloading) {
			reloadTime -= Time.deltaTime;
			int ammoToLoad = 0;

			if (reloadTime <= 0) {
				isReloading = false;
				reloadTime = 0;

				if (selectedNum == 0) {
					ammo_inHandgun = ammo_inHandgun_MAX;

					if (ammoType == "HandgunA") {
						if (ammo_handgunA >= ammo_inHandgun_MAX) {
							ammo_handgunA -= ammo_inHandgun_MAX;
							ammo_inHandgun = ammo_inHandgun_MAX;
						} else {
							ammo_inHandgun = ammo_handgunA;
							ammo_handgunA = 0;
						}
						PlayerPrefs.SetInt("AmmoHandgunA_" + prof.ToString (), ammo_handgunA);
					} else if (ammoType == "HandgunB") {
						if (ammo_handgunB >= ammo_inHandgun_MAX) {
							ammo_handgunB -= ammo_inHandgun_MAX;
							ammo_inHandgun = ammo_inHandgun_MAX;
						} else {
							ammo_inHandgun = ammo_handgunB;
							ammo_handgunB = 0;
						}
						PlayerPrefs.SetInt("AmmoHandgunB_" + prof.ToString (), ammo_handgunB);
					}

					MG_UI_HeroWeapons.I._updateAmmo ();
					PlayerPrefs.SetInt("AmmoInHandgun_" + prof.ToString (), ammo_inHandgun);
				} else if (selectedNum == 1) {
					ammo_inRifle = ammo_inRifle_MAX;

					if (ammoType == "RifleA") {
						if (ammo_rifleA >= ammo_inRifle_MAX) {
							ammo_rifleA -= ammo_inRifle_MAX;
							ammo_inRifle = ammo_inRifle_MAX;
						} else {
							ammo_inRifle = ammo_rifleA;
							ammo_rifleA = 0;
						}
						PlayerPrefs.SetInt("AmmoRifleA_" + prof.ToString (), ammo_rifleA);
					} else if (ammoType == "RifleB") {
						if (ammo_rifleB >= ammo_inRifle_MAX) {
							ammo_rifleB -= ammo_inRifle_MAX;
							ammo_inRifle = ammo_inRifle_MAX;
						} else {
							ammo_inRifle = ammo_rifleB;
							ammo_rifleB = 0;
						}
						PlayerPrefs.SetInt("AmmoRifleB_" + prof.ToString (), ammo_rifleB);
					}

					MG_UI_HeroWeapons.I._updateAmmo ();
					PlayerPrefs.SetInt("AmmoInRifle_" + prof.ToString (), ammo_inRifle);
				}
			}
		}
	}
	#endregion

	//Includes
	//	- _getWeaponAmmo()					- Returns true if player has enough ammo of selected type, works for both ammo in weapon and in inventory
	#region "Misc"
	public bool _getWeaponAmmo(int reqAmmo, string reqAmmoType){
		switch (reqAmmoType) {
			case "inHandgun":		return (ammo_inHandgun >= reqAmmo); break;
			case "inRifle":			return (ammo_inRifle >= reqAmmo); break;

			case "HandgunA": 		return (ammo_handgunA >= reqAmmo); break;
			case "HandgunB": 		return (ammo_handgunB >= reqAmmo); break;
			case "RifleA": 			return (ammo_rifleA >= reqAmmo); break;
			case "RifleB": 			return (ammo_rifleB >= reqAmmo); break;

			default: return false;
		}
	}
	#endregion
}
