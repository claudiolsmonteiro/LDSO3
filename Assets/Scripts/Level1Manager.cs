using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour {

	// TextTyper variables
	public float letterPause = 0.05f;
	string message;
  Text textComp;

	// Dialogue Variables
	public string[] messages = {"Hi! Do you want to play football with us?", "Great! We're missing a player. dou you want to help us find one?", "cool! see you later!"};
	public string[] replies = {"yes", "no"};
	private int dialogueCounter;
	public bool displayChoices = false;
	public bool displayNextButton = false;

	// GUI styling
	private GUIStyle style, continueStyle;

	void Start() {

		// initialize variables
		dialogueCounter = 0;
		textComp = FindObjectOfType<Text>();

		// Format buttons
		formatGUI();

		// start actions
		continueDialogue();
	}

	void clearText() {
		textComp.text = "";
	}

	void continueDialogue() {
		clearText();
		message = messages[dialogueCounter];
		StartCoroutine(TypeText(message));
		dialogueCounter++;
	}

	void OnGUI() {
		if (displayChoices){
			if (GUI.Button(new Rect(375, 225, 268, 25), "yes")){
				displayChoices = false;
				continueDialogue();
			}
			if (GUI.Button(new Rect(375, 275, 268, 25), "no")) {
				displayChoices = false;
			}
		}
		if (displayNextButton) {
			if (GUI.Button(new Rect(375, 225, 268, 25), "Continue")) {
				SceneManager.LoadScene("SchoolOutdoors");
			}
		}

	}


	// Write text to screen
	IEnumerator TypeText (string message) {
		for (int i = 0; i < message.Length; i++) {
			textComp.text += message[i];
			yield return new WaitForSeconds (letterPause);
		}
		if (dialogueCounter == messages.Length) {
			displayChoices = false;
			displayNextButton = true;
		} else {
			displayChoices = true;
		}
	}


	void formatGUI()	{
		//	style.fontSize = 14;
		//	style.font = (Font)Resources.Load("Fonts/Jekyll"); ;
	}

	// Update is called once per frame
	void Update () {

	}
}
