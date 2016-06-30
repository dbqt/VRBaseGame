using UnityEngine;
using System.Collections;

public class MainMenuNavigation : MonoBehaviour {

	public GameObject startButton, rulesButton, optionsButton, backButton, tiltToggleButton, cardboardModeButton, rulesText;

	public void startGame(){
		//Application.LoadLevel(level);
	}

	public void openRules(){
		disableMainMenu();
		rulesText.SetActive(true);
		backButton.SetActive(true);
	}

	public void returnToMainMenu(){
		showMainMenu();
		rulesText.SetActive(false);
		backButton.SetActive(false);
		tiltToggleButton.SetActive(false);
		cardboardModeButton.SetActive(false);
	}

	public void openOptions(){
		disableMainMenu();
		tiltToggleButton.SetActive(true);
		cardboardModeButton.SetActive(true);
		backButton.SetActive(true);
	}

	private void showMainMenu(){
		startButton.SetActive(true);
		rulesButton.SetActive(true);
		optionsButton.SetActive(true);
	}

	private void disableMainMenu(){
		startButton.SetActive(false);
		rulesButton.SetActive(false);
		optionsButton.SetActive(false);
	}
}
