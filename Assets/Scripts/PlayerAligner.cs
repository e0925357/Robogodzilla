using UnityEngine;
using System.Collections;

public class PlayerAligner : MonoBehaviour {

	public Camera playerCamera;
	public Player player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.IsJumping && player.IsAlive) {
			Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
			
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			Vector3 point = hit.point;
			point.y = 0;
			
			Vector3 direction = point - transform.position;
			direction.Normalize();
			
			Vector3 rotation = transform.localRotation.eulerAngles;

			float dotProduct = Vector3.Dot(Vector3.right, direction);
			float angle = Mathf.Acos(dotProduct);
			
			if(direction.z > 0){
				angle *= -1;
			}

			rotation.y = angle * 180.0f / Mathf.PI;

			transform.rotation = Quaternion.identity;
			transform.Rotate(rotation);
		}
	}
}
