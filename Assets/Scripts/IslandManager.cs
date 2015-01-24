using UnityEngine;
using System.Collections.Generic;

public class IslandManager : MonoBehaviour {
	public Player player;
	
	public GameObject[] islandPrefabs;
	
	public AnimationCurve maxIslandsCurve;
	public AnimationCurve minIslandsCurve;
	
	public int maxACPoints = 100;
	
	public Island currentIsland;
	public List<Island> neighbours = new List<Island>();

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
		
		//currentIsland.destroyIsland();
		
		player.moveToIsland(target);
		currentIsland = target;
		
		foreach(Island island in neighbours) {
			if(island != target) {
				//island.destroyIsland();
			}
		}
		
		neighbours.Clear();
		generateIslands();
	}
	
	private void generateIslands() {
		
	}

	public void OnPlayerJumpFinished(){

	}
}
