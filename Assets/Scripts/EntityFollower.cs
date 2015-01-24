using UnityEngine;
using System.Collections;

public class EntityFollower : MonoBehaviour {
	public float followingSpeed = 1f;
	public float sqrDeadzone = 0.01f;
	public Transform targetEntity;

	public GameObject sea;
	
	private Vector3 startTransOffset;
	private Material seaMaterial;
	private float shakingTimer = 0;

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
		
		if(shakingTimer > 0) {
			shakingTimer -= Time.deltaTime;
			
			float rotation = 360*Random.value;
			Vector3 offset = new Vector3(Mathf.Cos(rotation), 0, Mathf.Sin(rotation));
			offset *= (0.2f + 0.3f*Random.value)*Time.deltaTime;
			transform.localPosition += transform.localPosition + offset;
		}
	}
	
	public void startShaking() {
		shakingTimer = 0.5f;
	}
}
