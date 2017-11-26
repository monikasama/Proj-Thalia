using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
		"NONE", "Terrain", "Unit", "Unit Eraser", "Doodad", "Doodad Eraser"
	};

	// Terrain Tiles
	string[] optionsTer = new string[]{
		"mg_grass01",
		"mg_cliff01",
		"mg_waterPlain01"
		// Do not forget the corners, place them on "LIST - Corners" on this file
	};

	#region "LIST - Doodads"
	string[] optionsDood = new string[]{
		"GrassCorner"
		,"CliffCorner"
		,"WaterPlainCorner"
	};
	List<string> optionsDoodName = new List<string>();
	public List<string> _getDoodNames(string curDoodType){
		List<string> retVal = new List<string> ();

		switch (curDoodType) {
			case "GrassCorner": retVal.AddRange ( new string[]{"mg_grassCorner1", "mg_grassCorner2", "mg_grassCorner3"}); break;
			case "CliffCorner": retVal.AddRange ( new string[]{"mg_cliffCorner1", "mg_cliffCorner2", "mg_cliffCorner3", "mg_cliffCorner4", "mg_cliffCorner5", "mg_cliffCorner6", "mg_cliffCorner7", "mg_cliffCorner8"}); break;
			case "WaterPlainCorner": retVal.AddRange ( new string[]{"mg_waterPlainCorner1", "mg_waterPlainCorner2", "mg_waterPlainCorner3"}); break;
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
			MG_Globals.I.editorParent.SetActive(true);
			brush = GameObject.Find ("EditorCursor");
			editorMode = true;

			_startHelper();					// Start editor helper

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
		if(!crash && Application.isPlaying)
			MG_ControlCamera.I._changeBackgroundColor (Color.black);
		
		MG_ControlCamera.I.isEditor = false;
		editorMode = false;

		MG_Globals.I.editorParent.SetActive(false);
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

		if (!editorMode)
			return;

		ind_optBT = EditorGUILayout.Popup("Brush Type", ind_optBT, optionsBT);
		brushMode = optionsBT[ind_optBT];
		_selectHelperSubCanvas (brushMode);		// Update editor helper sub-canvas

		if (brushMode == "Terrain") {
			GUILayout.Label ("Terrain Creation", EditorStyles.boldLabel);
			ind_terrainList = EditorGUILayout.Popup ("Terrain Type", ind_terrainList, optionsTer);
			terType = optionsTer [ind_terrainList];

			if (GUILayout.Button ("Fill Map Terrain")) {
				foreach (MG_ClassTerrain tL in MG_Globals.I.terrains) {
					tL._changeTerrain (MG_DB_Terrain.I._getSprite (terType), terType);
				}
			}
			GUILayout.Label ("-\tThis will turn all tiles into the selected terrain ");

			try {
				if (Application.isPlaying) {
					// Show selected terrain in helper
					GameObject dummy = MG_DB_Terrain.I._getSprite (terType);
					Sprite image = dummy.GetComponent<SpriteRenderer> ().sprite;
					iHlp_Terrain.sprite = image;
					DestroyImmediate(dummy);
				}
			} catch (Exception ex) {

			}
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
					// Show selected doodad in helper
					GameObject dummy = MG_DB_Doodad.I._getSprite (doodType);
					Sprite image = dummy.GetComponent<SpriteRenderer> ().sprite;
					iHlp_Doodad1.sprite = image;
					iHlp_Doodad2.sprite = image;
					iHlp_Doodad3.sprite = image;
					iHlp_Doodad4.sprite = image;
					DestroyImmediate(dummy);
				}
			} catch (Exception ex) {

			}
		} else if (brushMode == "Doodad Eraser") {

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

	//Includes
	//	- _startHelper									- Starts the editor helper
	//	- _selectHelperSubCanvas						- Selects the editor helper'ś subcanvas depending on the current mode of the editor
	#region "Helper UI Control"
	public Canvas helperDoodad, helperTerrain;
	public Image iHlp_Doodad1, iHlp_Doodad2, iHlp_Doodad3, iHlp_Doodad4, iHlp_Terrain;

	public void _startHelper(){
		helperDoodad = GameObject.Find ("C_EditorDoodad").GetComponent<Canvas>();
		helperTerrain = GameObject.Find ("C_EditorTerrain").GetComponent<Canvas>();

		iHlp_Doodad1 = GameObject.Find ("E_Dood_ImgSelected1").GetComponent<Image> ();
		iHlp_Doodad2 = GameObject.Find ("E_Dood_ImgSelected2").GetComponent<Image> ();
		iHlp_Doodad3 = GameObject.Find ("E_Dood_ImgSelected3").GetComponent<Image> ();
		iHlp_Doodad4 = GameObject.Find ("E_Dood_ImgSelected4").GetComponent<Image> ();

		iHlp_Terrain = GameObject.Find ("E_Terr_ImgSelected").GetComponent<Image> ();
	}

	public void _selectHelperSubCanvas(string canvasMode){
		helperDoodad.enabled = false;
		helperTerrain.enabled = false;

		switch (canvasMode) {
			case "Doodad":								helperDoodad.enabled = true; break;
			case "Terrain":								helperTerrain.enabled = true; break;
		}
	}
	#endregion

	#region "Misc Functions"
	Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
	{
		Color32[] original = originalTexture.GetPixels32();
		Color32[] rotated = new Color32[original.Length];
		int w = originalTexture.width;
		int h = originalTexture.height;

		int iRotated, iOriginal;

		for (int j = 0; j < h; ++j)
		{
			for (int i = 0; i < w; ++i)
			{
				iRotated = (i + 1) * h - j - 1;
				iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
				rotated[iRotated] = original[iOriginal];
			}
		}

		Texture2D rotatedTexture = new Texture2D(h, w);
		rotatedTexture.SetPixels32(rotated);
		rotatedTexture.Apply();
		return rotatedTexture;
	}

	/*public Texture2D rotateTexture(Texture2D origTexture, float rotation){
		Color32[] original = origTexture.GetPixels32 ();
		Color32[] rotated = new Color32[original.Length];
		int w = origTexture.width;
		int h = origTexture.height;

		if (rotation == 0) {
			return origTexture;
		} else if (rotation == 90) {
			for (int x = w; x >= 0; x--) {
				for (int y = 0; y < h; y++) {
					rotated[]
				}
			}
		}
	}*/
	#endregion
}
