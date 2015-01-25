using UnityEngine;
using System.Collections.Generic;

public class IslandManager : MonoBehaviour {
	public Player player;
	
	public GameObject masterIslandPrefab;
	public GameObject flagPrefab;
	public GameObject[] islandPrefabs;
	public Texture[] flagTextures;
	
	public AnimationCurve maxIslandsCurve;
	public AnimationCurve minIslandsCurve;
	
	public int maxACPoints = 100;
	
	public AnimationCurve nationCountCurve;
	public int maxNationCurvePoints = 1500;
	
	public Island currentIsland;
	public List<Island> neighbours = new List<Island>();
	
	public float spawnRadius = 3f;
	
	private static readonly FlagType[] FLAG_TYPE_ARRAY = {FlagType.NONE, FlagType.AMERICA, FlagType.RUSSIA, FlagType.CHINA, FlagType.GERMANY, FlagType.PIRATES};

	// Use this for initialization
	void Start () {
		generateIslands();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void islandClicked(Island target) {
		if(player.IsJumping || target == currentIsland || !player.IsAlive) {
			return;
		}
		
		currentIsland.DestroyIsland();
		
		player.moveToIsland(target);
		currentIsland = target;
		
		foreach(Island island in neighbours) {
			if(island != target) {
				island.DestroyIsland();
			}
		}
		
		neighbours.Clear();
		generateIslands();
	}
	
	private void generateIslands() {
		int numIslandsToGenerate = (int)maxIslandsCurve.Evaluate(player.Score/(float)maxACPoints);
		float phaseShift = Random.value*Mathf.PI * 2f;
		
		for(int i = 0; i < numIslandsToGenerate; i++) {
			int nationIndex = (int)(Mathf.Min((nationCountCurve.Evaluate(player.Score/(float)maxNationCurvePoints))*Random.value, FLAG_TYPE_ARRAY.Length-1));
			FlagType type = FLAG_TYPE_ARRAY[nationIndex];
			
			int islandToGenerateIndex = (int)((islandPrefabs.Length)*Random.value);
			
			GameObject masterInstance = (GameObject)GameObject.Instantiate(masterIslandPrefab);
			GameObject islandInstance = (GameObject)GameObject.Instantiate(islandPrefabs[islandToGenerateIndex]);
			
			Island islandScript = masterInstance.GetComponent<Island>();
			islandScript.islandManager = this;
			islandScript.flagType = type;
			
			islandInstance.transform.parent = masterInstance.transform;
			islandInstance.transform.localPosition = Vector3.zero;
			
			masterInstance.transform.Rotate(new Vector3(0, 80*Random.value - 40, 0));
			
			if(type != FlagType.NONE){
				GameObject flagInstance = (GameObject)GameObject.Instantiate(flagPrefab);
				Vector3 relativePos = flagInstance.transform.position;
				flagInstance.transform.parent = masterInstance.transform;
				flagInstance.transform.localPosition = relativePos;
				
				flagInstance.transform.GetChild(0).renderer.material.mainTexture = flagTextures[nationIndex-1];
			}
			
			Vector3 masterPosition = masterInstance.transform.position;
			
			masterPosition.x = Mathf.Cos(Mathf.PI*2f*(float)i/(float)numIslandsToGenerate + phaseShift)*spawnRadius
				+ currentIsland.transform.position.x;
			masterPosition.z = Mathf.Sin(Mathf.PI*2f*(float)i/(float)numIslandsToGenerate + phaseShift)*spawnRadius
				+ currentIsland.transform.position.z;
			
			masterInstance.transform.position = masterPosition;
			neighbours.Add(masterInstance.GetComponent<Island>());
		}
		
	}

	public void OnPlayerJumpFinished(){
		currentIsland.OnPlayerLanded ();
		FlagType islandFlagType = currentIsland.GetComponent<Island>().flagType;
		
		if(islandFlagType != FlagType.NONE && islandFlagType != player.currentFlag) {
			player.killPlayer();
		} else {
			int score = currentIsland.GetComponentInChildren<IslandTheme>().scoreValue;
			player.Score += score;
		}
	}
}
