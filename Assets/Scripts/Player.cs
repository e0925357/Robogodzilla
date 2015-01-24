using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public IslandManager islandManager;
	public RectTransform timeBar;

	public float jumpTime = 1;
	public float maxTimeLeft = 10;
	public float minTimeLeft = 1;

	private bool isJumping = false;
	private int score = 0;

	private Vector3 oldPosition;
	private Island targetIsland;
	private float jumpTimer;
	private float timeLeft;
	private float currentMaxTimeLeft;
	private bool isAlive;

	// Use this for initialization
	void Start () {
		isAlive = true;
	}
	
	void resetTimer() {
		currentMaxTimeLeft = timeLeft = maxTimeLeft/(score*0.01f) + minTimeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isAlive) return;
		
		if (isJumping) {
			jumpTimer += Time.deltaTime;

			if(jumpTimer >= jumpTime) {
				isJumping = false;
				jumpTimer = jumpTime;
			}

			transform.position = oldPosition + (targetIsland.transform.position - oldPosition) * (jumpTimer  /jumpTime);

			if(!isJumping) {
				islandManager.OnPlayerJumpFinished();
			}
		} else {
			timeLeft -= Time.deltaTime;
		}
		
		if(timeLeft <= 0) {
			isAlive = false;
		}
		
		timeBar.localScale = new Vector3(timeLeft/currentMaxTimeLeft, 1, 1);
	}

	public void moveToIsland(Island target) {
		targetIsland = target;
		oldPosition = transform.position;
		jumpTimer = 0;
		isJumping = true;
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
