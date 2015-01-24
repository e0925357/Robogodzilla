using UnityEngine;
using System.Collections;

public class EntityFollower : MonoBehaviour {
	public float followingSpeed = 1f;
	public float sqrDeadzone = 0.01f;
	public Transform targetEntity;

	public GameObject sea;
	
	private Vector3 startTransOffset;
	private Material seaMaterial;

	// Use this for initialization
	void Start () {
		startTransOffset = transform.position;

		seaMaterial = sea.renderer.material;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 diffVec = targetEntity.position - transform.position + startTransOffset;
		diffVec.y = 0;

		if(diffVec.sqrMagnitude > sqrDeadzone) {
			transform.position = transform.position + diffVec.normalized * followingSpeed * Time.deltaTime;
		}

		Vector2 textureOffset;
		textureOffset.x = -transform.position.x / sea.transform.localScale.x;
		textureOffset.y = transform.position.z / sea.transform.localScale.z;

		seaMaterial.mainTextureOffset = textureOffset;
	}
}
