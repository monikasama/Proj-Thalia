using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_Inputs : MonoBehaviour {
	public static O_Inputs I;
	public void Awake(){ I = this; }

	public void _start(){
		
	}

	private string lastDirection;
	/// <summary>
	/// Called from OverworldMain.cs -> Update()
	/// </summary>
	public void _checkPress(){
		// Directional movement keys
		if (lastDirection == "") {
			if (Input.GetAxis ("Horizontal") == -1) {
				O_ControlHero.I._orderMoveHero ("Left");
				lastDirection = "Left";
			} else if (Input.GetAxis ("Horizontal") == 1) {
				O_ControlHero.I._orderMoveHero ("Right");
				lastDirection = "Right";
			} else if (Input.GetAxis ("Vertical") == -1) {
				O_ControlHero.I._orderMoveHero ("Down");
				lastDirection = "Down";
			} else if (Input.GetAxis ("Vertical") == 1) {
				O_ControlHero.I._orderMoveHero ("Up");
				lastDirection = "Up";
			}
		} 
		else {
			if (
				(lastDirection == "Left" 	&& Input.GetAxis ("Horizontal") == -1) ||
				(lastDirection == "Right" 	&& Input.GetAxis ("Horizontal") == 1) ||
				(lastDirection == "Up" 		&& Input.GetAxis ("Vertical") == 1) ||
				(lastDirection == "Down" 	&& Input.GetAxis ("Vertical") == -1)
			) {
				O_ControlHero.I._orderMoveHero (lastDirection);
			} else {
				lastDirection = "";
				_checkPress ();
			}
		}
	}
}
