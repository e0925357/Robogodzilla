using UnityEngine;
using System.Collections.Generic;

public class IslandManager : MonoBehaviour {
	public Player player;
	
	public GameObject masterIslandPrefab;
	public GameObject[] islandPrefabs;
	
	public AnimationCurve maxIslandsCurve;
	public AnimationCurve minIslandsCurve;
	
	public int maxACPoints = 100;
	
	public AnimationCurve islandPrefabsCurve;
	public int maxAPPoints = 500;
	
	public Island currentIsland;
	public List<Island> neighbours = new List<Island>();
	
	public float spawnRadius = 3f;

	// Use this for initialization
	void Start () {
		generateIslands();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void islandClicked(Island target) {
		if(player.IsJumping || target == currentIsland) {
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
		int numIslandsToGenerate = (int)islandPrefabsCurve.Evaluate(player.Score/maxACPoints);
		float phaseShift = Random.value*Mathf.PI;
		
		for(int i = 0; i < numIslandsToGenerate; i++) {
			int islandToGenerateIndex = (int)islandPrefabsCurve.Evaluate(player.Score/maxAPPoints);
			
			GameObject masterInstance = (GameObject)GameObject.Instantiate(masterIslandPrefab);
			GameObject islandInstance = (GameObject)GameObject.Instantiate(islandPrefabs[i]);
			
			Island islandScript = masterInstance.GetComponent<Island>();
			islandScript.islandManager = this;
			
			islandInstance.transform.parent = masterInstance.transform;
			islandInstance.transform.localPosition = Vector3.zero;
			
			Vector3 masterPosition = masterInstance.transform.position;
			
			masterPosition.x = Mathf.Cos(Mathf.PI*2f*(float)i/(float)numIslandsToGenerate + phaseShift)*spawnRadius;
			masterPosition.z = Mathf.Sin(Mathf.PI*2f*(float)i/(float)numIslandsToGenerate + phaseShift)*spawnRadius;
			
			masterInstance.transform.position = masterPosition;
		}
		
	}

	public void OnPlayerJumpFinished(){

	}
}
