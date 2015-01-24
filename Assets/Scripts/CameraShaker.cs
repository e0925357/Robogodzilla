using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {

	private Vector3 originPosition;
	private Quaternion originRotation;
	public float startShakeIntensity = .3f;
	private float shake_intensity = 0;
	public float shake_decay = 0.002f;
	
	// Update is called once per frame
	void Update () {
		if(shake_intensity > 0){
			transform.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;
			transform.localRotation = new Quaternion(
				originRotation.x + Random.Range(-shake_intensity,shake_intensity)*.2f,
				originRotation.y + Random.Range(-shake_intensity,shake_intensity)*.2f,
				originRotation.z + Random.Range(-shake_intensity,shake_intensity)*.2f,
				originRotation.w + Random.Range(-shake_intensity,shake_intensity)*.2f);
			shake_intensity -= shake_decay*Time.deltaTime;
		}
	}
	
	public void Shake(){
		originPosition = transform.localPosition;
		originRotation = transform.localRotation;
		shake_intensity = startShakeIntensity;
	}
}
