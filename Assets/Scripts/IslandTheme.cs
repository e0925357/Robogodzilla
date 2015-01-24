using UnityEngine;
using System.Collections;

public class IslandTheme : MonoBehaviour {

	public FlagType flagType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnRoboGodzillaLand() {
		audio.pitch = 0.9f + 0.2f * Random.value;
		audio.Play ();
	}
}
