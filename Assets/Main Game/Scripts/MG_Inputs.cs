﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Inputs : MonoBehaviour {
	public static MG_Inputs I;
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
		string qp = "pressing ";
		if (Input.GetAxis ("Horizontal") == -1) { qp += " left";
			_calculateMoveDirection ("Left");
			isMoving = true;
		} else {
			usedDirections.Remove ("Left");
		}
		if (Input.GetAxis ("Horizontal") == 1) { qp += " right";
			_calculateMoveDirection ("Right"); isMoving = true;
		}else {
			usedDirections.Remove ("Right");
		}
		if (Input.GetAxis ("Vertical") == -1) { qp += " up";
			_calculateMoveDirection ("Down"); isMoving = true;
		}else {
			usedDirections.Remove ("Down");
		}
		if (Input.GetAxis ("Vertical") == 1) { qp += " down";
			_calculateMoveDirection ("Up"); isMoving = true;
		}else {
			usedDirections.Remove ("Up");
		}
		if (!isMoving) { qp += " nothing";
			usedDirections.Clear ();
			lastDirection = "";
			MG_ControlHero.I._orderStopMoving ();
		}
		Debug.Log (qp);
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
}
