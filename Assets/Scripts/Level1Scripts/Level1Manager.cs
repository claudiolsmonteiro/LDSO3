using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour {

	// TextTyper variables
	public float letterPause = 0.05f;
	string message;
  Text textComp;

	// Dialogue Variables
	public string[] messages = {"Hi! Do you want to play football with us?", "Great! We're missing a player. Do you want to help us find one?", "CCool! See you later!"};
	public string[] replies1 = {"Hi! Yeah, that sound's great", "Hi! Thank you, but no."};
	public string[] replies2 = {"Sure, I'll look for somebody", "Actually, I think I'll maybe join you later"};
	private List<string[]> answersList;
	private Button answerA;
	private Button answerB;
	private int answersCounter;

	private int dialogueCounter;
	public bool displayChoices = false;
	public bool displayNextButton = false;
	private bool interactionOver = false;

	// GUI styling
	private GUIStyle style, continueStyle;


	// Initializ
	void Start() {

		// Initialize variables
		dialogueCounter = 0;
		answersCounter = 0;

		textComp = GameObject.Find("otherText").GetComponent<Text>();

		// Load possible actions
		answersList = new List<string[]>();
		answersList.Add(replies1);
		answersList.Add(replies2);

		// Initialize buttons
		answerA = GameObject.Find("answerA").GetComponent<Button>();
		answerB = GameObject.Find("answerB").GetComponent<Button>();

		// Start actions
		hideButtons();
		continueDialogue();
	}

	void hideButtons() {
		answerA.gameObject.SetActive(false);
		answerB.gameObject.SetActive(false);
	}

	void clearText() {
		textComp.text = "";
	}

	void continueDialogue() {
		clearText();
		hideButtons();
		message = messages[dialogueCounter];
		StartCoroutine(TypeText(message));
		dialogueCounter++;
	}


	void saidNo() {
		clearText();
		hideButtons();
		message = "Oh, ok. Maybe I'll see you later!";
		StartCoroutine(TypeText(message));
		interactionOver = true;
	}

	bool displayContinue = false;


	void displayAnswers(string[] arr) {
		answerA.GetComponentInChildren<Text>().text = arr[0];
		answerA.onClick.AddListener( () => continueDialogue());
		answerA.gameObject.SetActive(true);

		answerB.GetComponentInChildren<Text>().text = arr[1];
		answerB.onClick.AddListener( () => saidNo());
		answerB.gameObject.SetActive(true);
		answersCounter++;
	}

	void OnGUI() {
		if (displayContinue) {
			if (GUI.Button(new Rect(375, 225, 268, 25), "Return")) {
				SceneManager.LoadScene("SchoolOutdoors");
			}
		}
		if (displayNextButton) {
			if (GUI.Button(new Rect(375, 225, 268, 25), "Continue")) {
				SceneManager.LoadScene("LookingForAPlayer");
			}
		}

	}


	// Write text to screen
	IEnumerator TypeText (string message) {
		for (int i = 0; i < message.Length; i++) {
			textComp.text += message[i];
			yield return new WaitForSeconds (letterPause);
		}
		if (interactionOver) {
			displayContinue = true;
		}
		if (dialogueCounter == messages.Length) {
			displayChoices = false;
			displayNextButton = true;
		} else {
			displayAnswers(answersList[answersCounter]);
		}
	}



	// Update is called once per frame
	void Update () {

	}
}
