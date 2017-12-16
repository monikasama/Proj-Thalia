using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 	Check <Project Dump> / PROJ Thalia / Documentation / _MG_COLLISION_NOTES.txt
 */

public class MG_OnTriggerEnter : MonoBehaviour {

	public List<GameObject> collideList;
	public bool hasCollided = false;

	void OnTriggerEnter2D(Collider2D other){
		/*MG_OnTriggerEnter hitList = other.GetComponent<MG_OnTriggerEnter> ();
		if (hitList != null) {
			// If other's collideList contains the owner of this component,
			// that means the collision is already calculated, thus return
			if (hitList.collideList.Contains (transform.gameObject))
				return;
		}

		collideList.Add(other.gameObject);*/
		//collider = other.gameObject;

		MG_ControlCollision.I._collisionHandler (transform.gameObject, other.transform.gameObject);
	}

	void OnTriggerExit2D(Collider2D other){
		//collideList.Remove (other.gameObject);
	}
}
