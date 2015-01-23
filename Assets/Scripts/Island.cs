using UnityEngine;
using System.Collections;

public class Island : MonoBehaviour {

	public AnimationCurve spawnCurve;

	public AnimationCurve dieCurve;

	public float spawnTime = 1;

	public float spawnHeight = -1;

	private bool islandSpawned = false;
	private float spawnTimer = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!islandSpawned) {
			spawnTimer += Time.deltaTime;
		}
	}
}
