using UnityEngine;
using System.Collections;

public class RandomTeleport : MonoBehaviour {
	private float time = 0f;
	
	public float intervall = 10f;
	public float teleportRadius = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		
		if(time > intervall) {
			transform.position += new Vector3(Random.value*teleportRadius, 0, Random.value*teleportRadius);
			time = 0;
		}
	}
}
