using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UI_HeroWeapons : MonoBehaviour {
	public static MG_UI_HeroWeapons I;
	public void Awake(){ I = this; }

	public Canvas canvas;

	public List<Image> img_weapIcon, img_weapIcon_Border;
	public Text txt_weapName, txt_weapAmmo;
	public int selectedNum;
	public List<string> str_weapName;

	public Sprite borderSelected, borderNotSelected;

	public void _start(){
		canvas 									= GameObject.Find ("C_ItemBelt").GetComponent<Canvas> ();
		img_weapIcon 							= new List<Image> ();
		img_weapIcon_Border 					= new List<Image> ();
		str_weapName 							= new List<string> ();
		txt_weapName 							= GameObject.Find("ItemName").GetComponent<Text>();
		txt_weapAmmo 							= GameObject.Find("ItemBullet").GetComponent<Text>();

		selectedNum = MG_HeroWeapons.I.selectedNum;
		Image i_item, i_itemBorder;
		for (int i = 1; i <= 6; i++) {
			i_item = GameObject.Find ("Item00" + i.ToString ()).GetComponent<Image> ();
			i_itemBorder = GameObject.Find ("ItemBorder_00" + i.ToString ()).GetComponent<Image> ();

			img_weapIcon.Add (i_item);
			img_weapIcon_Border.Add (i_itemBorder);
			str_weapName.Add (MG_HeroWeapons.I.weapons [i - 1]);

			i_item.sprite = MG_DB_Weapons.I._getSprite (MG_HeroWeapons.I.weapons [i - 1]);
			if (selectedNum + 1 == i)
				i_itemBorder.sprite = borderSelected;
			else 
				i_itemBorder.sprite = borderNotSelected;
		}

		txt_weapName.text = str_weapName [selectedNum];
		_updateAmmo ();
	}

	public void _changeEquippedWeapon(int newWeaponNum){
		img_weapIcon_Border [selectedNum].sprite = borderNotSelected;
		selectedNum = newWeaponNum;
		img_weapIcon_Border [selectedNum].sprite = borderSelected;

		txt_weapName.text = str_weapName [selectedNum];
		_updateAmmo ();
	}

	public void _updateAmmo(){
		int ammo = 0, ammoAll = 0;
		bool hasAmmo = false;
		switch (MG_HeroWeapons.I.ammoType) {
			case "HandgunA":
				ammo 				= MG_HeroWeapons.I.ammo_inHandgun;
				ammoAll 			= MG_HeroWeapons.I.ammo_handgunA;
				hasAmmo = true;
			break;
			case "HandgunB":
				ammo 				= MG_HeroWeapons.I.ammo_inHandgun;
				ammoAll 			= MG_HeroWeapons.I.ammo_handgunB;
				hasAmmo = true;
			break;
			case "RifleA":
				ammo 				= MG_HeroWeapons.I.ammo_inRifle;
				ammoAll 			= MG_HeroWeapons.I.ammo_rifleA;
				hasAmmo = true;
			break;
			case "RifleB":
				ammo 				= MG_HeroWeapons.I.ammo_inRifle;
				ammoAll 			= MG_HeroWeapons.I.ammo_rifleB;
				hasAmmo = true;
			break;
		}
		if (hasAmmo) 			txt_weapAmmo.text = ammo + "/" + ammoAll;
		else 					txt_weapAmmo.text = "";
	}
}
