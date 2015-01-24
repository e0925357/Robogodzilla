using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public IslandManager islandManager;
	public RectTransform timeBar;

	public float jumpTime = 1;
	public int scoreReward = 10;
	public float maxTimeLeft = 10;
	public float minTimeLeft = 1;
	public FlagType currentFlag = FlagType.AMERICA; 

	private bool isJumping = false;
	private int score = 0;

	private Vector3 oldPosition;
	private Island targetIsland;
	private float jumpTimer;
	private float timeLeft;
	private float currentMaxTimeLeft;
	private bool isAlive;

	private float yCoordinate;

	// Use this for initialization
	void Start () {
		isAlive = true;
		yCoordinate = transform.position.y;
		resetTimer ();
	}
	
	void resetTimer() {
		currentMaxTimeLeft = timeLeft = maxTimeLeft/(score*0.01f + 1) + minTimeLeft;
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

			Vector3 newPosition = oldPosition + (targetIsland.transform.position - oldPosition) * (jumpTimer  /jumpTime);
			newPosition.y = yCoordinate;
			transform.position = newPosition;

			if(!isJumping) {
				score += scoreReward;
				islandManager.OnPlayerJumpFinished();
			}
		} else {
			timeLeft -= Time.deltaTime;
		}
		
		if(timeLeft <= 0) {
			isAlive = false;
		}
		
		timeBar.localScale = new Vector3(timeLeft/currentMaxTimeLeft, 1, 1);
		
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			currentFlag = FlagType.AMERICA;
		} else if(Input.GetKeyDown(KeyCode.Alpha2)) {
			currentFlag = FlagType.RUSSIA;
		} else if(Input.GetKeyDown(KeyCode.Alpha3)) {
			currentFlag = FlagType.CHINA;
		} else if(Input.GetKeyDown(KeyCode.Alpha4)) {
			currentFlag = FlagType.GERMANY;
		}
	}

	public void moveToIsland(Island target) {
		resetTimer ();
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

	public bool IsAlive{
		get {
			return this.isAlive;
		}
	}
}
