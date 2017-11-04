using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_ControlDoodad : MonoBehaviour {
	public static O_ControlDoodad I;
	public void Awake(){ I = this; }

	public int doodCnt;
	public List<int> toDestroy;

	#region "Creation Codes"
	public void _createDoodad(string newDoodType, float newPosX, float newPosY){
		O_Globals.I.doodadsTemp.Add(new O_ClassDoodad(O_DB_Doodad.I._getSprite(newDoodType), newDoodType, doodCnt, newPosX, newPosY));
		doodCnt++;
	}

	public void _createDoodad(string newDoodType, float newPosX, float newPosY, float zRotation){
		O_Globals.I.doodadsTemp.Add(new O_ClassDoodad(O_DB_Doodad.I._getSprite(newDoodType), newDoodType, doodCnt, newPosX, newPosY, zRotation));
		doodCnt++;
	}

	public void _createDoodad(string newDoodType, float newPosX, float newPosY, float scaleX, float scaleY, float zPosition, float zRotation = 0){
		O_Globals.I.doodadsTemp.Add(new O_ClassDoodad(O_DB_Doodad.I._getSprite(newDoodType), doodCnt, newPosX, newPosY, scaleX, scaleY, zPosition, zRotation, newDoodType));
		doodCnt++;
	}
	#endregion

	#region "Destroy Codes"
	public void _addToDestroyList(O_ClassDoodad targetDood){
		toDestroy.Add (targetDood.id);
	}

	public void _destroyListed(){
		if (toDestroy.Count > 0) {
			for (int i = 0; i < toDestroy.Count; i++) {
				_destroyDood (i);
			}
		}
		toDestroy.Clear ();
	}

	public void _destroyDood(int targetDoodIndex){
		int indexToRemove = -1;
		for (int i = O_Globals.I.doodads.Count - 1; i >= 0; i--) {
			if (toDestroy [targetDoodIndex] == O_Globals.I.doodads [i].id) {
				indexToRemove = i;
				break;
			}
		}
		if (indexToRemove > -1) {
			Destroy (O_Globals.I.doodads [indexToRemove].sprite);
			O_Globals.I.doodads.RemoveAt (indexToRemove);
		}
	}
	#endregion

	// Tail Creator
	//	- Gets a newly created doodad via editor 
	#region "MISC - Tail Creator"
	public void _createTails(O_ClassDoodad createdDood, bool isTree, float zRotation){
		float doodX = createdDood.posX, doodY = createdDood.posY;

		for (int x = -1; x < 2; x++) {
			for (int y = -1; y < 2; y++) {
				
				// Skip created doodad and diagonal corners
				if( x == 0 && y == 0)	continue;
				if (x != 0 && y != 0) 	continue;

				// Check if doodad node exists
				if( !O_GetDoodad.I._pointHasDood(doodX + x, doodY + y) )		continue;
				O_ClassDoodad nodeDood = O_GetDoodad.I._getDoodFromPoint (doodX + x, doodY + y);

				// Created doodad should be the same with the node doodad
				if (createdDood.type != nodeDood.type) 		continue;

				bool isXAlign = (createdDood.posX != nodeDood.posX) ? true : false;

				// Non-trees
				if (!isTree) {
					if (isXAlign) {
						if (createdDood.posX > nodeDood.posX) {
							_createTailOnPos (createdDood.type, createdDood.posX - 0.5f, createdDood.posY - 0.5f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX - 0.5f, createdDood.posY + 0.5f, zRotation);
						} else {
							_createTailOnPos (createdDood.type, createdDood.posX + 0.5f, createdDood.posY - 0.5f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.5f, createdDood.posY + 0.5f, zRotation);
						}
					} else {
						if (createdDood.posY > nodeDood.posY) {
							_createTailOnPos (createdDood.type, createdDood.posX - 0.5f, createdDood.posY - 0.5f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.5f, createdDood.posY - 0.5f, zRotation);
						} else {
							_createTailOnPos (createdDood.type, createdDood.posX - 0.5f, createdDood.posY + 0.5f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.5f, createdDood.posY + 0.5f, zRotation);
						}
					}
				}

				// Trees
				else{
					if (isXAlign) {
						if (createdDood.posX > nodeDood.posX) {
							_createTailOnPos (createdDood.type, createdDood.posX - 0.25f, createdDood.posY - 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX - 0.25f, createdDood.posY + 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX - 0.75f, createdDood.posY - 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX - 0.75f, createdDood.posY + 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX - 0.5f, createdDood.posY, zRotation);
						} else {
							_createTailOnPos (createdDood.type, createdDood.posX + 0.25f, createdDood.posY - 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.25f, createdDood.posY + 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.75f, createdDood.posY - 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.75f, createdDood.posY + 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.5f, createdDood.posY, zRotation);
						}
					} else {
						if (createdDood.posY > nodeDood.posY) {
							_createTailOnPos (createdDood.type, createdDood.posX - 0.25f, createdDood.posY - 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.25f, createdDood.posY - 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX, createdDood.posY - 0.5f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX - 0.5f, createdDood.posY - 0.5f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.5f, createdDood.posY - 0.5f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX - 0.25f, createdDood.posY - 0.75f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.25f, createdDood.posY - 0.75f, zRotation);
						} else {
							_createTailOnPos (createdDood.type, createdDood.posX - 0.25f, createdDood.posY + 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.25f, createdDood.posY + 0.25f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX, createdDood.posY + 0.5f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX - 0.5f, createdDood.posY + 0.5f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.5f, createdDood.posY + 0.5f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX - 0.25f, createdDood.posY + 0.75f, zRotation);
							_createTailOnPos (createdDood.type, createdDood.posX + 0.25f, createdDood.posY + 0.75f, zRotation);
						}
					}
				}
			}
		}
	}

	private void _createTailOnPos(string doodToCreate, float tarPosX, float tarPosY, float zRotation){
		// Check if tail exists
		if (O_GetDoodad.I._pointHasDood (tarPosX, tarPosY, doodToCreate))
			return;

		_createDoodad (doodToCreate, tarPosX, tarPosY, zRotation);
	}
	#endregion
}
