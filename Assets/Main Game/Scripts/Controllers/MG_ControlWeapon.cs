using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlWeapon : MonoBehaviour {
	public static MG_ControlWeapon I;
	public void Awake(){ I = this; }

	int prof;
	// FOR PLAYER
	public float hero_newRecoveryTime, hero_newReloadTime;
	public bool auto_weaponFired;

	public void _start(){
		prof = MG_Globals.I.prof;
	}

	public void _useWeapon(MG_ClassUnit user, string weaponType){
		switch (weaponType) {
			#region "Hero Weapons"
			case "testWeapon":
				string ammoType = "inHandgun";

				// Ammo check
				if(!_getPlayerHasEnoughAmmo(1, ammoType)){
					_reloadWeapon(ammoType);
					return;
				}

				MG_ControlMissile.I._createMissile ("test", MG_ControlHero.I.hero.posX, MG_ControlHero.I.hero.posY, MG_ControlHero.I.hero.id, MG_ControlHero.I.hero.facingAngle);
				_reduceAmmo(1, ammoType);	// Ammo reduction
			break;
			#endregion
		}
	}

	//Includes
	//	- _getPlayerHasEnoughAmmo()				- Returns true if player has enough ammo. If not, a prompt is shown.
	//	- _reduceAmmo()							- Reduces ammo of selected type, reloads automatically when reaches <= 0, if reducing from equipped weapon
	#region "Ammo Control = CHECK, REDUCE, RELOAD"
	// Returns true if player has enough ammo. If not, a prompt is shown.
	public bool _getPlayerHasEnoughAmmo(int reqAmmo, string reqAmmoType){
		if (MG_HeroWeapons.I._getWeaponAmmo (reqAmmo, reqAmmoType)) {
			return true;
		} else {
			return false;
		}
	}

	// Reduces ammo of selected type, reloads automatically when reaches <= 0, if reducing from equipped weapon
	public void _reduceAmmo(int reqAmmo, string reqAmmoType){
		switch(reqAmmoType){
			case "inHandgun":
				MG_HeroWeapons.I.ammo_inHandgun -= reqAmmo;
				PlayerPrefs.SetInt ("AmmoInHandgun_" + prof.ToString (), MG_HeroWeapons.I.ammo_inHandgun);
				MG_UI_HeroWeapons.I._updateAmmo ();

				if (MG_HeroWeapons.I.ammo_inHandgun <= 0) {
					MG_HeroWeapons.I.ammo_inHandgun = 0;
					_reloadWeapon (reqAmmoType);
				}
			break;
		}
	}

	// Reduces ammo of selected type, reloads automatically when reaches <= 0, if reducing from equipped weapon
	public void _reloadWeapon(string reqAmmoType){
		switch (reqAmmoType) {
			case "inHandgun":
				if (!MG_HeroWeapons.I._getWeaponAmmo (1, MG_HeroWeapons.I.ammoType)) return;

				MG_HeroWeapons.I.reloadTime = MG_ControlWeapon.I.hero_newReloadTime;
				MG_HeroWeapons.I.isReloading = true;

				MG_ControlBuffs.I._addBuff (MG_ControlHero.I.hero.id, "Reloading", MG_HeroWeapons.I.reloadTime);
			break;
		}
	}
	#endregion
}
