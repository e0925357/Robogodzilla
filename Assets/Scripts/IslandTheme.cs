using UnityEngine;
using System.Collections;

public class IslandTheme : MonoBehaviour {
	public int scoreValue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnRoboGodzillaLand() {
		audio.pitch = 0.9f + 0.2f * Random.value;
		audio.Play ();

		SpecialIslandBehavior behavior = GetComponent<SpecialIslandBehavior> ();
		if (behavior != null) {
			behavior.OnPlayerLanded();
		}
		
		ParticleSystem[] pses = GetComponentsInChildren<ParticleSystem>();
		
		if(pses != null) {
			foreach(ParticleSystem ps in pses) {
				ps.Play();
			}
		}
	}
}
