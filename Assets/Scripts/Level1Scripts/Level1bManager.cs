using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Level1bManager : MonoBehaviour {

	// TextTyper variables
	public float letterPause = 0.05f;
	string message;
  Text textComp;

	//Character's GameObjects
	private Image mainCharacterSpeechBalloon;
	private Text mainCharacterSpeech;
	private string mainCharacterSpeechText;

	private Image cmtCharacterSpeechBalloon;
	private Text cmtCharacterSpeech;
	private string cmtCharacterSpeechText;

	// Character Response Game Objects
	private Button answerA;
	private Button answerB;
	private Button answerC;
	private Button answerD;

	// Dialogue Variables
	private string[] mainCharacterLines = {"Hey, new kid! Wanna join us playing football? We need one more player.", "Great!", "v!"};
	private string[] cmtCharacterLines = {"Hey there, thanks. I would love to play with you, but I cannot play football.", "v"};
	private string[] choices = {"yes", "no"};

	// Control Variables
	bool mainCharacterTurn;
	private bool displayChoices = true;
	private bool displayNextButton = false;
	private bool interactionOver;
	private int mainCharacterDialogueCounter;
	private int cmtCharacterDialogueCounter;

	// GUI styling
	private GUIStyle style, continueStyle;


	/////////////////////////////////////////////////////////////////////////////
	void Start() {

		// initialize control variables
		mainCharacterDialogueCounter = 0;
		cmtCharacterDialogueCounter = 0;
		mainCharacterTurn = true;
		interactionOver = false;

		// Get Character's Lines
		//  XMLParser("level1, mainCharacterLines ,cmtCharacterLines");

		// Get main character's speech objects
		mainCharacterSpeechBalloon = GameObject.Find("mainCharacterSpeech").GetComponent<Image>();
		mainCharacterSpeech = GameObject.Find("mainCharacterSpeechText").GetComponent<Text>();
		mainCharacterSpeechText = mainCharacterSpeech.text;

		// Get cmt character's speech objects
		cmtCharacterSpeechBalloon = GameObject.Find("CMTcharacterSpeech").GetComponent<Image>();
		cmtCharacterSpeech = GameObject.Find("CMTcharacterSpeechText").GetComponent<Text>();
		cmtCharacterSpeechText = mainCharacterSpeech.text;

		// Get buttons
		answerA = GameObject.Find("answerA").GetComponent<Button>();
		answerB = GameObject.Find("answerB").GetComponent<Button>();
		answerC = GameObject.Find("answerC").GetComponent<Button>();
		answerD = GameObject.Find("answerD").GetComponent<Button>();

		// Hide Speech balloons
		mainCharacterSpeechBalloon.enabled = false;
		cmtCharacterSpeechBalloon.enabled = false;

		// Hide response buttons
		answerA.gameObject.SetActive(false);
		answerB.gameObject.SetActive(false);
		answerC.gameObject.SetActive(false);
		answerD.gameObject.SetActive(false);

		// Format buttons
		formatGUI();

		// start actions
		continueDialogue();
	}

	// Clear Speech Balloon
	void clearText(Text textObject) {
		textObject.text = "";
	}

	void interactionCycle() {
		if (mainCharacterDialogueCounter == mainCharacterLines.Length) {
			displayChoices = false;
			displayNextButton = true;
		} else {
			displayChoices = true;
		}
	}

	// Dialogue cycle
	void continueDialogue() {
		if (mainCharacterDialogueCounter == mainCharacterLines.Length-1 && cmtCharacterDialogueCounter == cmtCharacterLines.Length-1) {
			interactionOver = true;
			return;
		}

		if (mainCharacterTurn) {
			// clear everything
			cmtCharacterSpeechBalloon.enabled = false;
			clearText(cmtCharacterSpeech);
			clearText(mainCharacterSpeech);
			// Write Text
			message = mainCharacterLines[mainCharacterDialogueCounter];
			StartCoroutine(TypeText(message));
			mainCharacterDialogueCounter++;
		} else {
			// Clean Everything
			mainCharacterSpeechBalloon.enabled = false;
			clearText(cmtCharacterSpeech);
			clearText(mainCharacterSpeech);
			// Write Text
			message = cmtCharacterLines[cmtCharacterDialogueCounter];
			StartCoroutine(TypeText(message));
			cmtCharacterDialogueCounter++;
		}
		mainCharacterTurn = !mainCharacterTurn;
	}

	void OnGUI() {
		if (displayNextButton) {
			if (GUI.Button(new Rect(375, 225, 268, 25), "Continue")) {
				continueDialogue();
				displayNextButton = false;
			}
		}
		if (interactionOver){
			if (GUI.Button(new Rect(375, 225, 268, 25), "Return")) {
				SceneManager.LoadScene("SchoolOutdoors");
			}
		}

	}


	// Write text to screen
	IEnumerator TypeText (string message) {
		if (mainCharacterTurn) {
			for (int i = 0; i < message.Length; i++) {
				mainCharacterSpeechBalloon.enabled = true;
				mainCharacterSpeech.text += message[i];
				yield return new WaitForSeconds (letterPause);
			}
		} else {
			for (int i = 0; i < message.Length; i++) {
				cmtCharacterSpeechBalloon.enabled = true;
				cmtCharacterSpeech.text += message[i];
				yield return new WaitForSeconds (letterPause);
			}
		}
		displayNextButton = true;
	}

	void formatGUI()	{
		//style.fontSize = 14;
		//	style.font = (Font)Resources.Load("Fonts/Jekyll"); ;
	}

	// Update is called once per frame
	void Update () {

	}
}
