using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	void Start () {
		// First time running the game
		if (PlayerPrefs.GetInt ("FirstRun") != 1)
			SetPlayerPrefs.I._setPlayerPrefs ();

		// Z-Player prefs
		ZPlayerPrefs.Initialize("sqPrefEncrypt29845", "09164667352sss");
		// FOR TESTING
		ZPlayerPrefs.SetInt("Profile", 1);
		Debug.Log (ZPlayerPrefs.GetInt ("Profile"));
	}

	#region "Test Buttons"
	public void _startGame(){
		// FOR TESTING
		ZPlayerPrefs.SetInt("Profile", 1);

		int prof = ZPlayerPrefs.GetInt ("Profile");
		Debug.Log (prof);
		Debug.Log (prof.ToString ());
		Debug.Log (ZPlayerPrefs.GetString ("LastScene_" + prof.ToString ()));
		SceneManager.LoadScene (ZPlayerPrefs.GetString("LastScene_" + prof.ToString()));
	}

	public void _clearPrefs(){
		PlayerPrefs.DeleteAll ();
		ZPlayerPrefs.DeleteAll ();

		SetPlayerPrefs.I._setPlayerPrefs ();
	}
	#endregion
}
