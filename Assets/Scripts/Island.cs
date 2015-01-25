using UnityEngine;
using System.Collections;

public class Island : MonoBehaviour {

	public AnimationCurve spawnCurve;

	public AnimationCurve dieCurve;

	public float spawnTime = 2;

	public float spawnHeight = -1;
	
	public IslandManager islandManager;

	public bool playSpawnAnimation = true;

	private bool islandSpawned = false;
	private float timer = 0;
	
	public FlagType flagType;

	bool islandDying = false;
	
	// Use this for initialization
	void Start () {
		Vector3 pos = transform.position;

		if (!playSpawnAnimation) {
			islandSpawned = true;
		} else {
			pos.y = spawnHeight;
		}

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
			}

			Vector3 pos = transform.position;
			pos.y = (1 - spawnCurve.Evaluate(timer / spawnTime)) * spawnHeight;

			transform.position = pos;
		}
	}

	public void DestroyIsland(){
		islandDying = true;
		timer = 0;
		collider.enabled = false;
	}

	public void OnPlayerLanded(){
		transform.GetComponentInChildren<IslandTheme> ().OnRoboGodzillaLand ();
	}
	
	void OnMouseDown() {
		islandManager.islandClicked(this);
	}
}
