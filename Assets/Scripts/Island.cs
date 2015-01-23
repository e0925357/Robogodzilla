using UnityEngine;
using System.Collections;

public class Island : MonoBehaviour {

	public AnimationCurve spawnCurve;

	public AnimationCurve dieCurve;

	public float spawnTime = 2;

	public float spawnHeight = -1;

	private bool islandSpawned = false;
	private float spawnTimer = 0;
	
	// Use this for initialization
	void Start () {
		Vector3 pos = transform.position;
		pos.y = spawnHeight;

		transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		if (!islandSpawned) {
			spawnTimer += Time.deltaTime;

			if(spawnTimer >= spawnTime) {
				islandSpawned = true;
				spawnTimer = spawnTime;
			}

			Vector3 pos = transform.position;
			pos.y = (1 - spawnCurve.Evaluate(spawnTimer / spawnTime)) * spawnHeight;

			transform.position = pos;
		}
	}
}
