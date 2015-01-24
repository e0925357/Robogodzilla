﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public IslandManager islandManager;

	public float jumpTime = 1;
	public int scoreReward = 10;

	private bool isJumping = false;
	private int score = 0;

	private Vector3 oldPosition;
	private Island targetIsland;
	private float jumpTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isJumping) {
			jumpTimer += Time.deltaTime;

			if(jumpTimer >= jumpTime) {
				isJumping = false;
				jumpTimer = jumpTime;
			}

			transform.position = oldPosition + (targetIsland.transform.position - oldPosition) * (jumpTimer  /jumpTime);

			if(!isJumping) {
				score += scoreReward;
				islandManager.OnPlayerJumpFinished();
			}
		}
	}

	public void moveToIsland(Island target) {
		targetIsland = target;
		oldPosition = transform.position;
		jumpTimer = 0;
		isJumping = true;

		audio.pitch = 0.8f + Random.value* 0.4f;
		audio.
		audio.Play ();
	}
	
	public bool IsJumping {
		get {
			return this.isJumping;
		}
	}
	
	public int Score {
		get {
			return this.score;
		}
	}
}
