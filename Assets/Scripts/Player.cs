using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private bool isJumping;
	private int score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void moveToIsland(Island target) {
		throw new System.NotImplementedException ();
	}
	
	public bool IsJumping {
		get {
			return this.isJumping;
		}
		set {
			isJumping = value;
		}
	}
	
	int Score {
		get {
			return this.score;
		}
		set {
			score = value;
		}
	}public 
}
