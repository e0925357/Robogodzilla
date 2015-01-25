using UnityEngine;
using System.Collections;

public class Sheep : MonoBehaviour {

	public AnimationCurve heightCurve;
	public float lifetime;

	private Vector3 movement;
	private bool launched = false;

	private float timer = 0;
	private float startY;
	private Vector3 rotation;
	private float launchDelay;
	private bool launchDelayActive = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (launched && timer <= lifetime) {
			timer += Time.deltaTime;
		
			if(launchDelayActive) {
				if(timer >= launchDelay) {
					launchDelayActive = false;
					timer = 0;

					audio.pitch = 0.8f + Random.value * 0.8f;
					audio.PlayDelayed(Random.value * 0.4f);
				}
			}
			else {
				if(timer >= lifetime) {
					Destroy(this.gameObject);
				}

				Vector3 newPosition = transform.position;
				newPosition += movement * Time.deltaTime;
				newPosition.y = startY + heightCurve.Evaluate(timer / lifetime) * 4;

				transform.position = newPosition;
				transform.Rotate(rotation * Time.deltaTime);
			}
		}
	}

	public void launchSheep(){
		Vector3 projectedPosition = transform.position;
		projectedPosition.y = 0;

		Vector3 projectedOrigin = transform.parent.position;
		projectedOrigin.y = 0;

		movement = projectedPosition - projectedOrigin;
		movement.Normalize ();

		movement *= 6 + Random.value  * 2;

		startY = transform.position.y;
		rotation.x = 180 + 360 * Random.value;
		rotation.y = 180 + 360 * Random.value;
		rotation.z = 180 + 360 * Random.value;

		launchDelay = Random.value * 0.1f;

		launched = true;
	}
}
