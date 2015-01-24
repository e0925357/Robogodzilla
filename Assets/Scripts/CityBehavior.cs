using UnityEngine;
using System.Collections;

public class CityBehavior : SpecialIslandBehavior {

	public GameObject animationObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnPlayerLanded ()
	{
		animationObject.animation.Play ("Fall");
	}
}
