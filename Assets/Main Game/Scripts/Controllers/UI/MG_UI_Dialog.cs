using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UI_Dialog : MonoBehaviour { 
	public static MG_UI_Dialog I;
	public void Awake(){ I = this; }

	public bool isOnDialog;

	public Canvas c_dialog, c_dialogOptions;
	public Image i_portrait;
	public Text t_speaker, t_text;

	public bool isPressConfirm;
	private int curDialog, curTxt, curTxt_lim;
	private int delayTxt, delayTxtLimit, frameDelay, frameDelay_Max;
	public bool hasContinuation;

	// For dialog options
	public List<Button> optionsBTN;
	public List<Text> optionsTXT;
	public bool isOnOptions;

	public Sprite btn_red, btn_blue;

	public void _start(){
		// Setup variables
		c_dialog 						= GameObject.Find("C_Dialog").GetComponent<Canvas>();
		c_dialogOptions 				= GameObject.Find("C_Dialog").GetComponent<Canvas>();
		i_portrait 						= GameObject.Find("I_DialogPortrait").GetComponent<Image>();
		t_speaker 						= GameObject.Find("T_DialogSpeaker").GetComponent<Text>();
		t_text 							= GameObject.Find("T_DialogText").GetComponent<Text>();

		// Variables
		curDialog = 1;
		delayTxtLimit = 1; //3
		frameDelay_Max = 15;
		curTxt = 0;

		// Lists
		optionsBTN 						= new List<Button>();
		optionsTXT 						= new List<Text> ();
		for (int i = 1; i <= 4; i++) {
			optionsBTN.Add(GameObject.Find("BTN_DialogOption" + i.ToString()).GetComponent<Button>());
			optionsTXT.Add(GameObject.Find("TXT_DialogOption" + i.ToString()).GetComponent<Text>());
			optionsBTN [optionsBTN.Count - 1].gameObject.SetActive (false);
			optionsBTN [optionsBTN.Count - 1].enabled = false;
		}

		// Misc
		/*Hide dialog while not in use*/ 	c_dialog.enabled = false;
											c_dialogOptions.enabled = false;
	}

	#region "Start dialog"
	public void _initDialog(int dialogNumber, string database = "current map"){
		// Misc on start
		///*Hide dialog item picture*/_hideItemPicture();

		curDialog = dialogNumber;
		if (database == "current map") 			database = MG_Globals.I.curMap;

		MG_DB_Dialog.I._setupDialogText (dialogNumber, database);
		t_speaker.text = MG_DB_Dialog.I.speaker;
		t_text.text = "";
		hasContinuation = MG_DB_Dialog.I.hasContinuation;
		curTxt_lim = MG_DB_Dialog.I.main.Length;

		curTxt = 0;
		frameDelay = frameDelay_Max;
		c_dialog.enabled = true;
		isOnDialog = true;

		// Misc
		/*Hide player canvas*/ 
	}
	#endregion

	#region "Dialog frame update"
	private bool colorMode = false;

	public void _drawDialogFrame(){
		if (!isOnDialog) 	return;
		
		delayTxt++;
		if (frameDelay > 0) 			frameDelay--;

		if (delayTxt <= delayTxtLimit) {
			curTxt++;
			if (curTxt >= curTxt_lim) {
				colorMode = false;
				curTxt = curTxt_lim;
			}

			string dialogToShow = MG_DB_Dialog.I.main.Substring (0, curTxt);
			dialogToShow = _translateSpecialSymbols (dialogToShow);

			t_text.text = dialogToShow;
			delayTxt = 0;
		}

		// Misc
		///*Check press confirm*/_pressConirm();
	}

	public string _translateSpecialSymbols(string dialogToShow){
		bool isColor = false, isModify = false;
		//isModify tracks if dialogToShow is modified at the course of running this function

		if (dialogToShow.EndsWith ("<")) {
			isModify = true;
			string nextSymbol = MG_DB_Dialog.I.main.Substring (curTxt + 6, 1);

			switch (nextSymbol) {
				case "r": curTxt += 10; isColor = true; break; // red
				case "g": curTxt += 12; isColor = true; break; // green
				case "b": curTxt += 11; isColor = true; break; // blue
				case "y": curTxt += 13; isColor = true; break; // yellow
				case "p": curTxt += 12; isColor = true; break; // purple
			}
		}

		if (isColor || colorMode) {
			colorMode = true;
			dialogToShow = MG_DB_Dialog.I.main.Substring (0, curTxt);
			dialogToShow += "</color>";
		} else {
			colorMode = false;
		}

		if (isModify && !isColor) {
			curTxt += 7;
			dialogToShow = MG_DB_Dialog.I.main.Substring (0, curTxt);
		}

		return dialogToShow;
	}
	#endregion

	#region "Press confirm"
	public void _pressConfirm(){
		if (isPressConfirm && isOnDialog && frameDelay <= 0 && !isOnOptions) {
			if (curTxt < curTxt_lim) {
				curTxt = curTxt_lim;
			} else {
				curDialog++;

				if (hasContinuation) {
					_initDialog (curDialog);
				} else {
					// This line has options
					if(MG_DB_Dialog.I.hasOptions){
						
					}
					// No options, dialog will now end
					else{
						_removeDialog ();
					}
				}
			}
		}
	}

	public void _removeDialog(){
		isOnDialog = false;
		c_dialog.enabled = false;
		///*Hide item picture*/ _hideItemPicture ();
	}
	#endregion

	#region "Create dialog options"
	public void _createDialogOptions(string[] options){
		for (int i = 1; i < 4; i++) {
			if (options [i] != "NONE") {
				optionsBTN [i].gameObject.SetActive (true);
				switch (i) {
					case 0: optionsBTN [i].image.sprite = (MG_DB_Dialog.I.option1_isRed) ? btn_red : btn_blue; break;
					case 1: optionsBTN [i].image.sprite = (MG_DB_Dialog.I.option1_isRed) ? btn_red : btn_blue; break;
					case 2: optionsBTN [i].image.sprite = (MG_DB_Dialog.I.option1_isRed) ? btn_red : btn_blue; break;
					case 3: optionsBTN [i].image.sprite = (MG_DB_Dialog.I.option1_isRed) ? btn_red : btn_blue; break;
				}
				optionsTXT [i].enabled = true;
				optionsTXT [i].text = options [i];
			}
		}
	}
	#endregion

	//Place your option events here
	//Place your option events here
	//Place your option events here
	//Place your option events here
	//Place your option events here
	//Place your option events here
	//Place your option events here
	#region "Buttons & press dialog options"
	public void _BTN_confirm(){
		isPressConfirm = true;
	}

	public void _pressDialog_1(){ _pressDialogOption (MG_DB_Dialog.I.optnEfct1);}
	public void _pressDialog_2(){ _pressDialogOption (MG_DB_Dialog.I.optnEfct2);}
	public void _pressDialog_3(){ _pressDialogOption (MG_DB_Dialog.I.optnEfct3);}
	public void _pressDialog_4(){ _pressDialogOption (MG_DB_Dialog.I.optnEfct4);}

	private void _pressDialogOption(int optionNum){
		if (optionNum == 0) 	return;

		// Misc events before running options
		// Destroys the dialog and option buttons
		isOnDialog = false;
		c_dialog.enabled = false;
		isOnOptions = false;
		for (int i = 0; i < 4; i++) {
			optionsBTN [i].gameObject.SetActive (false);
			optionsTXT [i].enabled = false;
		}
		/*Show hero bar canvas*/		MG_UI_HeroBars.I.c_HPbars.enabled = false;
										MG_UI_HeroBars.I.c_MPbars.enabled = false;
		/*Show hero weapons canvas*/	MG_UI_HeroWeapons.I.canvas.enabled = false;

		// Private variables
		int pref = MG_Globals.I.prof;

		//Place your option events here
		//Place your option events here
		//Place your option events here
		//Place your option events here
		//Place your option events here
		//Place your option events here
		//Place your option events here
		#region "Option Events"
		switch (optionNum) {
			#region "1 - TEST"
			case 1:
				Debug.Log("Option button works");
			break;
			#endregion
		}
		#endregion
	}
	#endregion

	//Includes
	//	Nothing
	#region "Misc"

	#endregion
}
