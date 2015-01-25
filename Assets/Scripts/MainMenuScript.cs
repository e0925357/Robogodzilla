using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

	public Image creditsPanel;
	public Image helpPanel;

	public void quit() {
		Application.Quit();
	}
	
	public void credits() {
		bool newValue = !creditsPanel.enabled;
		
		creditsPanel.enabled = newValue;
		
		Text[] texts = creditsPanel.GetComponentsInChildren<Text>();
		
		foreach(Text text in texts) {
			text.enabled = newValue;
		}
	}
	
	public void play() {
		Application.LoadLevel(1);
	}
}
