using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class EditorMainGame : EditorWindow {
	[MenuItem("Map Editor/Editor Windows/MainGame Editor")]
	public static void _showWindow(){
		EditorWindow.GetWindow (typeof(EditorMainGame));
	}

	string 			terType, unitType, doodClass, doodType, doodRotation, unitOwner;
	bool 			createTail = true, isTailTree, rotateDood;

	// Editor
	bool editorMode, telePressed;
	GameObject brush;
	string brushMode;

	#region "Pop-up lists AND Index"
	// Brush Types
	string[] optionsBT = new string[]{
		"NONE", "Terrain", "Unit", "Unit Eraser", "Doodad", "Doodad Eraser", "Corner"
	};

	// Terrain Tiles
	string[] optionsTer = new string[]{
		"mg_grass01",
		"mg_cliff01",
		// Do not forget the corners, place them on "LIST - Corners" on this file
	};

	#region "LIST - Doodads"
	string[] optionsDood = new string[]{
		"GrassCorner",

	};
	List<string> optionsDoodName = new List<string>();
	public List<string> _getDoodNames(string curDoodType){
		List<string> retVal = new List<string> ();

		switch (curDoodType) {
			case "GrassCorner": retVal.AddRange ( new string[]{"GrassCorner1", "GrassCorner2", "GrassCorner3"}); break;
		}

		return retVal;
	}
	#endregion
	#region "LIST - Units"
	string[] optionsUnit = new string[]{
		"pathBlocker_Editor", "testYou", "testTown"
	};
	#endregion
	#region "LIST - Corners"
	string[] optionsCornerBase 	= new string[]{ "Line", "Outward", "Inward" };
	string[] optionsCornerLine 	= new string[]{ "Up", "Down", "Left", "Right" };
	string[] optionCornerOutIn	= new string[]{ "0", "90", "180", "270" };
	string[] optionsCornerType = new string[]{
		"Grass",
	};

	string cornerBase, cornerLine, cornerType;
	#endregion

	// Index of pop-up lists
	int ind_optBT = 0;
	int ind_terrainList = 0;
	int ind_unitList = 0;
	int ind_doodList = 0, ind_doodName = 0;
	int ind_cornerBase = 0, ind_cornerLine = 0, ind_cornerType = 0;
	#endregion

	#region "SWITCH - Turn on/off editor"
	private void _turnOnEditor(){
		try{
			Debug.Log ("Editor Mode has started. (Main Game)");
			brush = GameObject.Find ("EditorCursor");
			editorMode = true;

			MG_ControlCamera.I._changeBackgroundColor (Color.blue);
			MG_ControlCamera.I.isEditor = true;

			#region "Replace Objects (PathBlocker, etc...)"
			foreach(MG_ClassUnit obj in MG_Globals.I.units){
				switch(obj.type){
					case "pathBlocker":
						obj._changeSprite("pathBlocker_Editor");
						obj.sprite.transform.SetParent(GameObject.Find("_MG_UNITS").transform);
					break;
				}
			}
			#endregion
		}catch(Exception ex){
			Debug.Log ("Editor failed to start. Reason: " + ex.Message);
			_turnOffEditor (false);
		}
	}

	private void _turnOffEditor(bool crash = false){
		if(!crash)
			MG_ControlCamera.I._changeBackgroundColor (Color.black);

		MG_ControlCamera.I.isEditor = false;
		editorMode = false;
	}
	#endregion

	#region "GUI Design"
	void OnGUI(){
		GUILayout.Label ("Editor Mode", EditorStyles.boldLabel);
		if (GUILayout.Button ("Start Editor Mode")) {
			if (!editorMode) {
				_turnOnEditor ();
			} else {
				_turnOffEditor ();
				Debug.Log ("Editor Mode is turned off.");
			}
		}

		ind_optBT = EditorGUILayout.Popup("Brush Type", ind_optBT, optionsBT);
		brushMode = optionsBT[ind_optBT];

		if (brushMode == "Terrain") {
			GUILayout.Label ("Terrain Creation", EditorStyles.boldLabel);
			ind_terrainList = EditorGUILayout.Popup ("Terrain Type", ind_terrainList, optionsTer);
			terType = optionsTer [ind_terrainList];
		} else if (brushMode == "Unit") {
			GUILayout.Label ("Unit Creation", EditorStyles.boldLabel);

			ind_unitList = EditorGUILayout.Popup ("Unit Type", ind_unitList, optionsUnit);
			unitType = optionsUnit [ind_unitList];

			unitOwner = EditorGUILayout.TextField ("Player Owner Number", unitOwner);
		} else if (brushMode == "Unit Eraser") {

		} else if (brushMode == "Doodad") {
			GUILayout.Label ("This will create the doodads per tile. You can also ");
			GUILayout.Label ("do an exact position placement by dragging doodads ");
			GUILayout.Label ("to the map. ");
			GUILayout.Label ("");
			GUILayout.Label ("");

			GUILayout.Label ("Doodad Creation", EditorStyles.boldLabel);
			ind_doodList = EditorGUILayout.Popup ("Doodad Type", ind_doodList, optionsDood);
			doodClass = optionsDood [ind_doodList];

			optionsDoodName = _getDoodNames (doodClass);
			ind_doodName = EditorGUILayout.Popup ("Doodad Name", ind_doodName, optionsDoodName.ToArray ());
			if (ind_doodName >= optionsDoodName.Count)
				ind_doodName = 0;
			doodType = optionsDoodName [ind_doodName];

			createTail = EditorGUILayout.Toggle ("Create Tail", createTail);
			isTailTree = EditorGUILayout.Toggle ("Tree-styled Tail", isTailTree);
			rotateDood = EditorGUILayout.Toggle ("Rotate Doodad", rotateDood);
			doodRotation = EditorGUILayout.TextField ("Doodad Rotation", doodRotation);

			try {
				if (Application.isPlaying) {
					GameObject dummy = MG_DB_Doodad.I._getSprite (doodType);
					Texture2D image = dummy.GetComponent<SpriteRenderer> ().sprite.texture;
					GUILayout.Label (image);
					DestroyImmediate (dummy);
				}
			} catch (Exception ex) {

			}
		} else if (brushMode == "Doodad Eraser") {

		} else if (brushMode == "Corner") {
			GUILayout.Label ("Doodad Corner Creation", EditorStyles.boldLabel);

			ind_cornerBase = EditorGUILayout.Popup ("Corner Base", ind_cornerBase, optionsCornerBase);
			cornerBase = optionsCornerBase [ind_cornerBase];
			GUILayout.Label (" ");
			GUILayout.Label (cornerBase);

			if (cornerBase == "Line") {
				ind_cornerLine = EditorGUILayout.Popup ("Corner Mode", ind_cornerLine, optionsCornerLine);
				cornerLine = optionsCornerLine [ind_cornerLine];
			} 
			else if (cornerBase == "Outward" || cornerBase == "Inward") {
				ind_cornerLine = EditorGUILayout.Popup ("Corner Line Mode", ind_cornerLine, optionCornerOutIn);
				cornerLine = optionCornerOutIn [ind_cornerLine];
			}

			ind_cornerType = EditorGUILayout.Popup ("Corner Type", ind_cornerType, optionsCornerType);
			cornerType = optionsCornerType [ind_cornerType];

		}

		GUILayout.Label ("Other Controls", EditorStyles.boldLabel);
		GUILayout.Label ("E = Teleport Player to mouse");
	}
	#endregion

	#region "Update();"
	void Update(){
		if (!editorMode) {
			return;
		}
		try{
			Vector3 gamePoint = MG_ControlCamera.I._getGamePoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
			int actPosX = (int)gamePoint.x, actPosY = (int)gamePoint.y;

			// Doodad and tail rotation
			float zRotation = 0;
			if(rotateDood)
				float.TryParse(doodRotation, out zRotation);

			#region "Mouse Click"
			if (Input.GetMouseButton(0)){
				#region "Terrain Brush"
				if(brushMode == "Terrain"){
					MG_ClassTerrain targetTile = MG_GetTerrain.I._getTerrain(actPosX, actPosY);
					targetTile._changeTerrain(MG_DB_Terrain.I._getSprite(terType), terType);
					targetTile.sprite.name = targetTile.sprite.name.Replace("(Clone)", "");
					targetTile.sprite.transform.SetParent(GameObject.Find("_MG_TERRAIN").transform);
				}
				#endregion
				#region "Unit Brush"
				else if(brushMode == "Unit"){
					// Remove existing unit if present
					bool hasUnit = MG_GetUnit.I._pointHasUnit(actPosX, actPosY);
					if(hasUnit)
						MG_ControlUnit.I._addToDestroyList(MG_GetUnit.I._getUnitFromPoint(actPosX, actPosY));

					// Create Unit
					int uOwner = 0;
					int.TryParse(unitOwner, out uOwner);
					MG_ControlUnit.I._createUnit(unitType, actPosX, actPosY, uOwner);

					// Specials
					MG_ClassUnit createdUnit = MG_GetUnit.I._getLastCreatedUnit();
					switch(createdUnit.type){
						case "pathBlocker":
							// Switch pathBlocker sprite
							createdUnit._changeSprite("pathBlocker_Editor");
							createdUnit.sprite.transform.SetParent(GameObject.Find("_MG_UNITS").transform);
						break;
					}

					// Finalize creation
					createdUnit.sprite.name = createdUnit.sprite.name.Replace("(Clone)", "");
					createdUnit.sprite.transform.SetParent(GameObject.Find("_MG_UNITS").transform);

				}
				#endregion
				#region "Doodad Brush (Per Tile)"
				else if(brushMode == "Doodad"){
					// Remove existing doodad if present
					bool hasDood = MG_GetDoodad.I._pointHasDood(actPosX, actPosY);
					if(hasDood)
						MG_ControlDoodad.I._addToDestroyList(MG_GetDoodad.I._getDoodFromPoint(actPosX, actPosY));

					// Create Doodad
					if(rotateDood)
						MG_ControlDoodad.I._createDoodad(doodType, actPosX, actPosY, zRotation);
					else
						MG_ControlDoodad.I._createDoodad(doodType, actPosX, actPosY);

					// Specials
					MG_ClassDoodad createdDood = MG_GetDoodad.I._getLastCreatedDood();
					///*Create tail*/ if(createTail) MG_ControlDoodad.I._createTails(createdDood, isTailTree, zRotation);
					/// Create tail is disabled since there is no need for this in main game

					// Finalize creation
					createdDood.sprite.name = createdDood.sprite.name.Replace("(Clone)", "");
					createdDood.sprite.transform.SetParent(GameObject.Find("_MG_DOODADS").transform);
				}
				#endregion
				#region "Corner Brush"
				else if(brushMode == "Corner"){
					// Remove existing corner if present
					bool hasDood = MG_GetDoodad.I._pointHasDood(actPosX, actPosY, "Corner");
					if(hasDood)
						MG_ControlDoodad.I._addToDestroyList(MG_GetDoodad.I._getDoodFromPoint(actPosX, actPosY, "Corner"));

					// Create Doodad
					string cornerType_orig = cornerType;

					if(cornerBase == "Line"){

					}
					else{
						if(cornerBase == "Outward")			cornerType += "Corner3";
						else if(cornerBase == "Inward")		cornerType += "Corner2";

						float zRot = 0;
						float.TryParse(cornerLine, out zRot);
						MG_ControlDoodad.I._createDoodad(cornerType, actPosX, actPosY, zRot);
					}

					// Specials
					MG_ClassDoodad createdDood = MG_GetDoodad.I._getLastCreatedDood();
					/*Return original cornerBase*/			cornerType = cornerType_orig;

					// Finalize creation
					createdDood.sprite.name = createdDood.sprite.name.Replace("(Clone)", "");
					createdDood.sprite.transform.SetParent(GameObject.Find("_MG_DOODADS").transform);
				}
				#endregion
				#region "Unit Eraser"
				else if(brushMode == "Unit Eraser"){
					// Remove existing unit if present
					bool hasUnit = MG_GetUnit.I._pointHasUnit(actPosX, actPosY);
					if(hasUnit){
						MG_ControlUnit.I._addToDestroyList(MG_GetUnit.I._getUnitFromPoint(actPosX, actPosY));
						Debug.Log("Unit erased!");
					}
				}
				#endregion
				#region "Doodad Eraser"
				else if(brushMode == "Doodad Eraser"){
					// Remove existing unit if present
					bool hasDood = MG_GetDoodad.I._pointHasDood(actPosX, actPosY);
					if(hasDood){
						MG_ControlDoodad.I._addToDestroyList(MG_GetDoodad.I._getDoodFromPoint(actPosX, actPosY));
						Debug.Log("Doodad erased!");

						// Remove tails
						for(float x = -0.5f; x < 1; x+=0.25f){
							for(float y = -0.5f; y < 1; y+=0.25f){
								hasDood = MG_GetDoodad.I._pointHasDood(actPosX + x, actPosY + y);
								if(hasDood)
									MG_ControlDoodad.I._addToDestroyList(MG_GetDoodad.I._getDoodFromPoint(actPosX + x, actPosY + y));
							}
						}
					}
				}
				#endregion
			}
			#endregion

			#region "Keyboard Keys"
			if(Input.GetKey(KeyCode.E) && !telePressed){
				MG_ControlHero.I._instantMove(actPosX, actPosY);
				Debug.Log("Teleported to " + actPosX + ", " + actPosY + ", " + telePressed);

				telePressed = true;
			}
			else if(!Input.GetKey(KeyCode.E) && telePressed){
				telePressed = false;				
			}
			#endregion

			#region "Bursh control"
			brush.transform.position = new Vector3(actPosX, actPosY, 0);
			#endregion
		}catch(Exception ex){
			Debug.Log ("Turning off editor mode. Reason: " + ex.Message);
			_turnOffEditor (true);
		}
	}
	#endregion
}
