﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlCamera : MonoBehaviour {
	public static MG_ControlCamera I;
	public void Awake(){ I = this; }

	public void _start(){
		//Variables
		dampTime = 0.1f;
		dampTimeDEF = dampTime;
	}

	private float dampTime, dampTimeDEF;
	private Vector3 velocity = Vector3.zero;
	public bool isEditor = false;

	private static readonly int CAMERA_X_LIMIT = 11, CAMERA_Y_LIMIT = 7;

	public void _reposition(float newPosX, float newPosY){
		// Camera border
		if (!isEditor) {
			if (CAMERA_Y_LIMIT < MG_Globals.I.map_maxY) {
				if (newPosY < -MG_Globals.I.map_maxY + CAMERA_Y_LIMIT)
					newPosY = -MG_Globals.I.map_maxY + CAMERA_Y_LIMIT;
				else if (newPosY > MG_Globals.I.map_maxY - CAMERA_Y_LIMIT)
					newPosY = MG_Globals.I.map_maxY - CAMERA_Y_LIMIT;
			} 
			else {
				newPosY = 0;
			}

			if (CAMERA_X_LIMIT < MG_Globals.I.map_maxX) {
				if (newPosX < -MG_Globals.I.map_maxX + CAMERA_X_LIMIT)
					newPosX = -MG_Globals.I.map_maxX + CAMERA_X_LIMIT;
				else if (newPosX > MG_Globals.I.map_maxX - CAMERA_X_LIMIT)
					newPosX = MG_Globals.I.map_maxX - CAMERA_X_LIMIT;
			} 
			else {
				newPosX = 0;
			}
		}

		Vector3 destination = new Vector3(newPosX, newPosY);
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
	}

	public Vector3 _getGamePoint(Vector3 screenPoint){
		Camera camera = GetComponent<Camera>();
		screenPoint.z = camera.nearClipPlane;
		Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return p;
	}

	public Vector3 _getCameraPosition(){
		Camera camera = GetComponent<Camera>();
		return camera.transform.position;
	}

	// Includes
	//	- _changeBackgroundColor()						- Changes the background color of the camera
	#region "Misc"
	public void _changeBackgroundColor(Color newColor){
		Camera camera = GetComponent<Camera>();

		camera.backgroundColor = newColor;
	}
	#endregion
}
