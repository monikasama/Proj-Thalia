using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_OnTriggerEnter : MonoBehaviour {

	public GameObject collider;
	public List<GameObject> collideList;
	public bool hasCollided = false;

	void OnTriggerEnter2D(Collider2D other){
		hasCollided = true;
		collideList.Add(other.gameObject);
		Debug.Log ("Colliding with " + other.gameObject.name);
		//collider = other.gameObject;
	}

	void OnTriggerExit2D(Collider2D other){
		hasCollided = false;
		collider = null;
	}
}
