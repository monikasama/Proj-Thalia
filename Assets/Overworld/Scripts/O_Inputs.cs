using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_Inputs : MonoBehaviour {
	public static O_Inputs I;
	public void Awake(){ I = this; }

	public void _start(){
		usedDirections = new List<string> ();
	}

	private string lastDirection;
	public List<string> usedDirections;
	/// <summary>
	/// Called from OverworldMain.cs -> Update()
	/// </summary>
	public void _checkPress(){
		// Directional movement keys
		bool isMoving = false;
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
		}
	}

	private void _calculateMoveDirection(string tarDirection){
		if (usedDirections.Contains (tarDirection)) {
			if (tarDirection == lastDirection) {
				O_ControlHero.I._orderMoveHero (lastDirection);
			} 
			else if (!usedDirections.Contains (lastDirection)) {
				O_ControlHero.I._orderMoveHero (tarDirection);
				lastDirection = tarDirection;
			}
		}
		else {
			usedDirections.Add (tarDirection);
			O_ControlHero.I._orderMoveHero (tarDirection);
			lastDirection = tarDirection;
		}
	}
}
