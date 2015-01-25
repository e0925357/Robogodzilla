using UnityEngine;
using System.Collections;

public class GrasBehavior : SpecialIslandBehavior {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void OnPlayerLanded ()
	{
		Sheep[] sheeps = GetComponentsInChildren<Sheep> ();

		foreach (Sheep sheep in sheeps) {
			sheep.launchSheep();
		}
	}
}
