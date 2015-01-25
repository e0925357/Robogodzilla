using UnityEngine;
using System.Collections;

public class RestartScript : MonoBehaviour {

	public void restartLevel() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
