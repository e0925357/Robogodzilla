using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public IslandManager islandManager;
	public RectTransform timeBar;
	
	public Image[] flagUI;
	private Image currentUIFlag;
	public Text scoreText;
	public AudioSource deathSound;

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
		
		currentUIFlag = flagUI[0];
		currentUIFlag.color = new Color(1f, 0.8f, 0.8f, 1f);
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
				scoreText.text = "" + score;
				islandManager.OnPlayerJumpFinished();
			}
		} else {
			timeLeft -= Time.deltaTime;
		}
		
		if(timeLeft <= 0) {
			killPlayer();
		}
		
		timeBar.localScale = new Vector3(timeLeft/currentMaxTimeLeft, 1, 1);
		
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			changeToNation(FlagType.AMERICA);
		} else if(Input.GetKeyDown(KeyCode.Alpha2)) {
			changeToNation(FlagType.RUSSIA);
		} else if(Input.GetKeyDown(KeyCode.Alpha3)) {
			changeToNation(FlagType.CHINA);
		} else if(Input.GetKeyDown(KeyCode.Alpha4)) {
			changeToNation(FlagType.GERMANY);
		}
	}
	
	public void changeToNation(FlagType newType) {
		currentUIFlag.color = new Color(1, 1, 1, 0.6f);
		
		switch(newType) {
		case FlagType.AMERICA:
			currentUIFlag = flagUI[0];
			break;
		case FlagType.RUSSIA:
			currentUIFlag = flagUI[1];
			break;
		case FlagType.CHINA:
			currentUIFlag = flagUI[2];
			break;
		case FlagType.GERMANY:
			currentUIFlag = flagUI[3];
			break;
		}
		
		currentUIFlag.color = new Color(1f, 0.8f, 0.8f, 1f);
		currentFlag = newType;
	}
	
	public void killPlayer() {
		isAlive = false;
		deathSound.Play();
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
