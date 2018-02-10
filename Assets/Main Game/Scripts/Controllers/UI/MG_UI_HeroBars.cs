using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UI_HeroBars : MonoBehaviour {
	public static MG_UI_HeroBars I;
	public void Awake(){ I = this; }

	public Canvas c_HPbars, c_MPbars;

	public Image hpBar, mpBar;
	private float hpBarHeightMax, mpBarHeightMax, hpBarYPos, mpBarYPos, screenSizeDiff;

	public void _start(){
		hpBar = GameObject.Find("HPBar").GetComponent<Image> ();
		mpBar = GameObject.Find("MPBar").GetComponent<Image> ();
		c_HPbars = GameObject.Find ("C_HPBar").GetComponent<Canvas> ();
		c_MPbars = GameObject.Find ("C_MPBar").GetComponent<Canvas> ();

		hpBarHeightMax = hpBar.rectTransform.sizeDelta.y;
		mpBarHeightMax = mpBar.rectTransform.sizeDelta.y;
		hpBarYPos = hpBar.rectTransform.position.y;
		mpBarYPos = mpBar.rectTransform.position.y;
		screenSizeDiff = 49.439f - hpBarYPos;
	}

	public void _update(){
		if (MG_ControlHero.I.hero == null) 	return;

		int hp = MG_ControlHero.I.hero.HP,
			hpMax = MG_ControlHero.I.hero.HPmax,
			mp = MG_ControlHero.I.hero.MP,
			mpMax = MG_ControlHero.I.hero.MPmax;

		float 	hpPerc = ((float)hp / (float)hpMax),
				mpPerc = ((float)mp / (float)mpMax),
				newBarSize_HP = hpBarHeightMax * hpPerc,
				newBarSize_MP = mpBarHeightMax * mpPerc;

		// Screen resize adjustment
		if (hpPerc >= 1 && mpPerc >= 1) {
			hpBarYPos = hpBar.rectTransform.position.y;
			mpBarYPos = mpBar.rectTransform.position.y;
			screenSizeDiff = 49.439f - hpBarYPos;
		}

		hpBar.rectTransform.sizeDelta = new Vector2 (hpBar.rectTransform.sizeDelta.x, hpBarHeightMax * hpPerc);
		mpBar.rectTransform.sizeDelta = new Vector2 (mpBar.rectTransform.sizeDelta.x, mpBarHeightMax * mpPerc);
		hpBar.rectTransform.position = new Vector2 (hpBar.rectTransform.position.x, screenSizeDiff * (1-hpPerc) + hpBarYPos - hpBarHeightMax * ((1-hpPerc)/2) );
		mpBar.rectTransform.position = new Vector2 (mpBar.rectTransform.position.x, screenSizeDiff * (1-mpPerc) + mpBarYPos - mpBarHeightMax * ((1-mpPerc)/2) );
	}
}
