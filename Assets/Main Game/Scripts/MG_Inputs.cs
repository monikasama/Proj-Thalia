using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Inputs : MonoBehaviour {
	public static MG_Inputs I;
	public void Awake(){ I = this; }

	public bool IS_MULTIDIRECTIONAL;
	private bool 	frameHasDir_Hor = false, 
					frameHasDir_Ver = false;						// Every frame this is setted to false, then setted to true when a direction has been setted

	public void _start(){
		usedDirections = new List<string> ();

		IS_MULTIDIRECTIONAL = false;
	}

	private string lastDirection;
	public List<string> usedDirections;
	/// <summary>
	/// Called from OverworldMain.cs -> Update()
	/// </summary>
	public void _checkPress(){
		// Directional movement keys
		string directionToMove = "";

		bool isMoving = false;
		if (IS_MULTIDIRECTIONAL) {
			// Multidirectional

			if (Input.GetAxis ("Horizontal") == -1) {
				directionToMove = _calculateMoveDirection_withDiagonal ("Left");
				isMoving = true;
			} else {
				usedDirections.Remove ("Left");
			}
			if (Input.GetAxis ("Horizontal") == 1) {
				directionToMove = _calculateMoveDirection_withDiagonal ("Right");
				isMoving = true;
			} else {
				usedDirections.Remove ("Right");
			}
			if (Input.GetAxis ("Vertical") == -1) {
				if (!directionToMove.Contains ("Down"))			// Prevent overwriting directional movement
					directionToMove = _calculateMoveDirection_withDiagonal ("Down");
				isMoving = true;
			} else {
				usedDirections.Remove ("Down");
			}
			if (Input.GetAxis ("Vertical") == 1) {
				if (!directionToMove.Contains ("Up"))				// Prevent overwriting directional movement
					directionToMove = _calculateMoveDirection_withDiagonal ("Up");
				isMoving = true;
			} else {
				usedDirections.Remove ("Up");
			}
			if (!isMoving) {
				usedDirections.Clear ();
				lastDirection = "";
				MG_ControlHero.I._orderStopMoving ();
			}

			if(directionToMove != "")
				MG_ControlHero.I._orderMoveHero (directionToMove);
		} else {
			// Non-multidirectional
			
			if (Input.GetAxis ("Horizontal") == -1) {
				_calculateMoveDirection ("Left");
				isMoving = true;
			} else {
				usedDirections.Remove ("Left");
			}
			if (Input.GetAxis ("Horizontal") == 1) {
				_calculateMoveDirection ("Right"); isMoving = true;
			}else {
				usedDirections.Remove ("Right");
			}
			if (Input.GetAxis ("Vertical") == -1) {
				_calculateMoveDirection ("Down"); isMoving = true;
			}else {
				usedDirections.Remove ("Down");
			}
			if (Input.GetAxis ("Vertical") == 1) {
				_calculateMoveDirection ("Up"); isMoving = true;
			}else {
				usedDirections.Remove ("Up");
			}
			if (!isMoving) {
				usedDirections.Clear ();
				lastDirection = "";
				MG_ControlHero.I._orderStopMoving ();
			}
		}
	}

	#region "Calculate Move Direction"
	private string _calculateMoveDirection_withDiagonal(string tarDirection){
		string moveOrder = tarDirection;

		if (usedDirections.Contains (tarDirection)) {
			if (tarDirection == lastDirection) {
				// MG_ControlHero.I._orderMoveHero (lastDirection);
				moveOrder = lastDirection;
			} 
			else if (!usedDirections.Contains (lastDirection)) {
				// MG_ControlHero.I._orderMoveHero (tarDirection);
				lastDirection = tarDirection;
				moveOrder = tarDirection;
			}
		}
		else {
			usedDirections.Add (tarDirection);
			// MG_ControlHero.I._orderMoveHero (tarDirection);
			moveOrder = tarDirection;
			lastDirection = tarDirection;
		}

		// Diagonal directions
		if (IS_MULTIDIRECTIONAL) {
			if ((moveOrder == "Up" || moveOrder == "Down") && (lastDirection == "Left" || lastDirection == "Right")) {
				moveOrder = moveOrder + lastDirection;
			} else if ((moveOrder == "Left" || moveOrder == "Right") && (lastDirection == "Up" || lastDirection == "Down")) {
				moveOrder = lastDirection + moveOrder;
			}
		}

		return moveOrder;
	}

	private void _calculateMoveDirection(string tarDirection){
		if (usedDirections.Contains (tarDirection)) {
			if (tarDirection == lastDirection) {
				MG_ControlHero.I._orderMoveHero (lastDirection);
			} 
			else if (!usedDirections.Contains (lastDirection)) {
				MG_ControlHero.I._orderMoveHero (tarDirection);
				lastDirection = tarDirection;
			}
		}
		else {
			usedDirections.Add (tarDirection);
			MG_ControlHero.I._orderMoveHero (tarDirection);
			lastDirection = tarDirection;
		}
	}
	#endregion
}
