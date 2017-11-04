using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transistor : MonoBehaviour {

	public float timeLeft = 0.1f;
	private bool isSwitching = false;
	
	void Start () {
		
	}

	void Update () {
		timeLeft -= Time.deltaTime;

		if (timeLeft <= 0 && !isSwitching) {
			_startSwitchScene ();
		}
	}

	private void _startSwitchScene(){
		_switchScene ();
		isSwitching = true;
	}

	private void _switchScene(){
		SceneManager.LoadScene (PlayerPrefs.GetString ("NextScene"));
	}
}
