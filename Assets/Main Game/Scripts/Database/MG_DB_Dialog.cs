using UnityEngine;
using System.Collections;

/*



	Colors available to be used by this system:

	Red, Green, Blue, Yellow, Purple

	Go to _translateSpecialSymbols inside the controller to add more.
*/

public class MG_DB_Dialog : MonoBehaviour {
	public static MG_DB_Dialog I;
	public void Awake(){ I = this; }

	public string speaker, main, portraitName, options1, options2, options3, options4;
	public bool hasContinuation, hasOptions, option1_isRed, option2_isRed, option3_isRed, option4_isRed;
	public int optnEfct1, optnEfct2, optnEfct3, optnEfct4;

	// Custom values are used to store strings to be used in dialog
	// To use, set these values first before calling P_ControlUI_Dialog.I._initDialog()
	public string cusValue1, cusValue2, cusValue3, cusValue4;

	public void _setupDialogText(int dialogNumber, string database = "current map"){
		int prof = PlayerPrefs.GetInt("CurProfile");
		if(database == "current map") database = PlayerPrefs.GetString("P_Map_" + prof.ToString());

		//Defaults for the next line
		hasOptions = false;
		hasContinuation = true;

		//Defaults for options
		options1 = "NONE"; options2 = "NONE"; options3 = "NONE"; options4 = "NONE";
		optnEfct1 = 0; optnEfct2 = 0; optnEfct3 = 0; optnEfct4 = 1;

		switch (database) {
			case "testTown":
				//Test
				switch (dialogNumber) {
					case 1:
						speaker = " "; portraitName = "NONE"; 
						main = "Hello World!";
					break;
					case 2:
						speaker = " "; portraitName = "NONE";
						hasContinuation = false;
						main = "Dialog system works perfect!";
					break;
				}
			break;
		}

		#region "Old references taken from PROJ Ronin"
			/*switch(database){
				 /* case "1": //test 1
					switch(dialogNumber){

						//Test
						case 1: speaker = " "; portraitName = "NONE"; 
							main = "Hello World!"; break;
						case 2: speaker = " "; portraitName = "NONE"; hasContinuation = false;
							main = "Dialog system works perfect!"; break;

							//Test NPC
						case 3: speaker = "TestNPC:"; portraitName = "NONE"; hasContinuation = false;
							main = "Interaction system works perfect!"; break;

							//Test Shop
						case 4: speaker = "TestShop:"; portraitName = "NONE";  hasContinuation = false; hasOptions = true;
							options1 = "Buy"; optnEfct1 = 6; option1_isRed = false;
							options2 = "Sell"; optnEfct2 = 7; option2_isRed = false;
							options3 = "Do Nothing"; optnEfct3 = 5; option3_isRed = true;
							//Go to P_ControlUI_Dialog for more options interaction
							//Always reset the database when using global interactions
							main = "Welcome to TestShop! Cheap and quality goods.";
							P_ControlUI_Dialog.I._resetDatabase();
						break;

					}
				break;

				/// ///// Dialogs that can be used by any map. /////
				/// ///// Dialogs that can be used by any map. /////
				/// ///// Dialogs that can be used by any map. /////
				/// ///// Dialogs that can be used by any map. /////
				case "death": //On death dialog
					switch(dialogNumber){

						case 1: speaker = " "; portraitName = "NONE";
							main = "You have died. Gold lost: " + cusValue1; break;
						case 2: speaker = " "; portraitName = "NONE"; hasContinuation = false; hasOptions = true;
							//Go to P_ControlUI_Dialog for more options interaction
							options1 = "Respawn"; optnEfct1 = 1; option1_isRed = false; 
							main = "Respawn from last checkpoint?";
							P_ControlUI_Dialog.I._resetDatabase();
						break;

					}
				break;
				case "checkpoint": //Checkpoint
					switch(dialogNumber){

						case 1: speaker = "Checkpoint:"; portraitName = "NONE";  hasContinuation = false; hasOptions = true;
							options1 = "Rest"; optnEfct1 = 2; option1_isRed = false;
							options2 = "Craft items"; optnEfct2 = 4; option2_isRed = false;
							options3 = "Access item box"; optnEfct3 = 3; option3_isRed = false;
							options4 = "Nothing"; optnEfct4 = 5; option4_isRed = true;
							//Go to P_ControlUI_Dialog for more options interaction
							//Always reset the database when using global interactions
							main = "What do you want to do?";
							P_ControlUI_Dialog.I._resetDatabase();
						break;

					}
				break;
				case "itemChest": //Item chest
					switch(dialogNumber){

						case 1: speaker = " "; portraitName = "NONE";  hasContinuation = false;
							//Always reset the database when using global interactions
							P_DB_Items.I._setValues(cusValue1);
							if(P_DB_Items.I.itemClass == "Weapon" || P_DB_Items.I.itemClass == "Passive")
								main = "Found <color=yellow>" + cusValue1 + "</color>.";
							else
								main = "Found <color=yellow>" + cusValue1 + " x" + cusValue2 + "</color>.";
							P_ControlUI_Dialog.I._showItemPicture(cusValue1);
							P_ControlUI_Dialog.I._resetDatabase();
						break;

					}
				break;
			}*/
			#endregion
	}
}
