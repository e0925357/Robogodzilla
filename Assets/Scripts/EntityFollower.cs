using UnityEngine;
using System.Collections;

public class EntityFollower : MonoBehaviour {
	public float followingSpeed = 1f;
	public float sqrDeadzone = 0.01f;
	public Transform targetEntity;
	
	private Vector3 startTransOffset;

	// Use this for initialization
	void Start () {
		startTransOffset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 diffVec = targetEntity.position - transform.position + startTransOffset;
		
		if(diffVec.sqrMagnitude > sqrDeadzone) {
			transform.position = transform.position + diffVec.normalized * followingSpeed * Time.deltaTime;
		}
	}
}
