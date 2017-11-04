using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPrefs : MonoBehaviour {
	public static SetPlayerPrefs I;
	public void Awake(){ I = this; }

	public void _setPlayerPrefs(){
		PlayerPrefs.SetString("NextMap", "test");

		// Non-profiled prefs
		PlayerPrefs.SetInt ("FirstRun", 1);
		PlayerPrefs.GetInt("EntranceUsed", 1);

		// Profiled prefs
		for (int i = 1; i <= 10; i++) {
			PlayerPrefs.SetInt ("O_LastX_" + i.ToString (), 0);
			PlayerPrefs.SetInt ("O_LastY_" + i.ToString (), 0);
			PlayerPrefs.GetString ("O_LastFacing_" + i.ToString (), "Down");

			ZPlayerPrefs.SetString ("LastMap_" + i.ToString (), "test");
			ZPlayerPrefs.SetString ("LastScene_" + i.ToString (), "Overworld");
		}
	}
}
