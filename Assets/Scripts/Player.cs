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
	public AudioSource switchSound;
	public GameObject playerBackDisplay;

	public float jumpTime = 1;
	public AnimationCurve jumpHeight;
	public float maxTimeLeft = 10;
	public float minTimeLeft = 1;
	public FlagType currentFlag = FlagType.AMERICA;
	public CameraShaker cameraShaker;
	
	public float maxRoarTime = 30;
	public float minRoarTime = 10;
	public AudioSource roarSound;
	private float roarTimer = 0;
	
	public Material deathMaterial;
	public float maxDeathAnimationTime = 10;
	public float deathAnimationTime;
	public string deathMessage = "-";
	public Canvas hudCanvas;
	public Canvas gameOverCanvas;
	public Text deathTextLabel;
	public Text deathScoreLabel;


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
		deathAnimationTime = maxDeathAnimationTime;
		playerBackDisplay.renderer.material.mainTexture = islandManager.flagTextures[0];
	}
	
	void resetTimer() {
		currentMaxTimeLeft = timeLeft = maxTimeLeft/(score*0.01f + 1) + minTimeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isAlive) {
			if(!IsReallyDead) {
				deathAnimationTime -= Time.deltaTime;
				
				deathMaterial.color = new Color(1, 1, 1, Mathf.Max(deathAnimationTime/maxDeathAnimationTime, 0));
				
				if(IsReallyDead) {
					hudCanvas.enabled = false;
					deathTextLabel.text = deathMessage;
					deathScoreLabel.text = "Score: " + score;
					gameOverCanvas.enabled = true;
				}
			}
			
			return;
		}
		
		if (isJumping) {
			jumpTimer += Time.deltaTime;

			if(jumpTimer >= jumpTime) {
				isJumping = false;
				jumpTimer = jumpTime;
			}

			Vector3 newPosition = oldPosition + (targetIsland.transform.position - oldPosition) * (jumpTimer  /jumpTime);
			newPosition.y = yCoordinate + jumpHeight.Evaluate(jumpTimer/jumpTime);
			transform.position = newPosition;

			if(!isJumping) {
				islandManager.OnPlayerJumpFinished();
				cameraShaker.Shake();
			}
		} else {
			timeLeft -= Time.deltaTime;
		}
		
		roarTimer -= Time.deltaTime;
		if(roarTimer <= 0) {
			roarTimer = minRoarTime + (maxRoarTime - minRoarTime)*Random.value;
			roarSound.pitch = roarSound.pitch*0.8f + Random.value*0.4f;
			roarSound.Play();
		}
		
		if(timeLeft <= 0) {
			killPlayer("TimeOutException: Jump took too long!");
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
		
		switchSound.pitch = switchSound.pitch*0.8f + Random.value*0.4f;
		switchSound.Play();
		
		switch(newType) {
		case FlagType.AMERICA:
			currentUIFlag = flagUI[0];
			playerBackDisplay.renderer.material.mainTexture = islandManager.flagTextures[0];
			break;
		case FlagType.RUSSIA:
			currentUIFlag = flagUI[1];
			playerBackDisplay.renderer.material.mainTexture = islandManager.flagTextures[1];
			break;
		case FlagType.CHINA:
			currentUIFlag = flagUI[2];
			playerBackDisplay.renderer.material.mainTexture = islandManager.flagTextures[2];
			break;
		case FlagType.GERMANY:
			currentUIFlag = flagUI[3];playerBackDisplay.renderer.material.mainTexture = islandManager.flagTextures[3];
			break;
		}
		
		currentUIFlag.color = new Color(1f, 0.2f, 0.2f, 1f);
		currentFlag = newType;
	}
	
	public void killPlayer(string deathMessage) {
		isAlive = false;
		deathSound.Play();
		
		Renderer[] renderes = GetComponentsInChildren<Renderer>();
		
		foreach(Renderer renderer in renderes) {
			renderer.material = deathMaterial;
		}
		
		if(this.renderer != null) {
			this.renderer.material = deathMaterial;
		}
		
		this.deathMessage = deathMessage;
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
		} set {
			score = value;
			scoreText.text = "" + score;
		}
	}

	public bool IsAlive{
		get {
			return this.isAlive;
		}
	}
	
	public bool IsReallyDead{
		get {
			return !isAlive && deathAnimationTime <= 0;
		}
	}
}
