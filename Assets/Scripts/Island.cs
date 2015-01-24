﻿using UnityEngine;
using System.Collections;

public class Island : MonoBehaviour {

	public AnimationCurve spawnCurve;

	public AnimationCurve dieCurve;

	public float spawnTime = 2;

	public float spawnHeight = -1;

	private bool islandSpawned = false;
	private float timer = 0;

	bool islandDying = false;
	
	// Use this for initialization
	void Start () {
		Vector3 pos = transform.position;
		pos.y = spawnHeight;

		transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		if (islandDying) {
			timer += Time.deltaTime;

			if(timer >= spawnTime) {
				Destroy(gameObject);
			}

			Vector3 pos = transform.position;
			pos.y = dieCurve.Evaluate(timer / spawnTime) * spawnHeight;
			
			transform.position = pos;
		}
		else if (!islandSpawned) {
			timer += Time.deltaTime;

			if(timer >= spawnTime) {
				islandSpawned = true;
				timer = spawnTime;
				DestroyIsland();
			}

			Vector3 pos = transform.position;
			pos.y = (1 - spawnCurve.Evaluate(timer / spawnTime)) * spawnHeight;

			transform.position = pos;
		}
	}

	public void DestroyIsland(){
		islandDying = true;
		timer = 0;
	}

	public void OnPlayerLanded(){

	}
}
