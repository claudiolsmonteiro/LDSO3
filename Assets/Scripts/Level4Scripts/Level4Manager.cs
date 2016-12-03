using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Level4Scripts
{
	public class Level4Manager : MonoBehaviour {

		// TextTyper variables
		public float LetterPause = 0.05f;
		public GameObject Button;
		string _message;
		Text _textComp;

		// Dialogue Variables
		public string[] Messages = {"Hi! I'm in trouble, can you please help me?"};
		private int _answersCounter;
		private int _dialogueCounter;

		// Control Variables
		private bool _displayReturn;
		public bool DisplayChoices;
		public bool DisplayContinue;
		private bool _interactionOver;


		// Initializ
		public void Start() {

			//Parser cenas = new Parser();

			// Initialize variables
			InitializeCtrlVariables();

			_textComp = GameObject.Find("otherText").GetComponent<Text>();

			// Load possible actions


			// Initialize buttons


			// Start actions
			ContinueDialogue();
		}
			

		void ClearText() {
			_textComp.text = "";
		}

		void ContinueDialogue() {
			if (_interactionOver || _dialogueCounter == Messages.Length){
				return;
			}
			if (!_textComp.Equals("")) {
				ClearText();
			}
			_message = Messages[_dialogueCounter];
			StartCoroutine(TypeText(_message));
			_dialogueCounter++;
		}

		void SaidNo() {
			if (_interactionOver){
				return;
			}
			ClearText();
			_message = "Oh, ok. Maybe I'll see you later!";
			StartCoroutine(TypeText(_message));
			_interactionOver=true;
		}

		public void OnGUI() {
			if (_displayReturn && !Button.activeSelf)
			{
				Button.SetActive(true);
				Button returnButton = Button.GetComponent<Button>();
				returnButton.onClick.RemoveAllListeners();
				returnButton.onClick.AddListener(() =>
					{
						SceneManager.LoadScene("SchoolOutdoors");
					});
			}
			if (DisplayContinue && !Button.activeSelf) {
				Button.SetActive(true);
				Button returnButton = Button.GetComponent<Button>();
				Text returnButtonText = Button.GetComponentInChildren<Text>();
				returnButtonText.text = "Continue";
				returnButton.onClick.RemoveAllListeners();
				returnButton.onClick.AddListener(() =>
					{
						SceneManager.LoadScene("Level1a-LookingForAPlayer");
					});
			}

		}

		void InitializeCtrlVariables() {
			Debug.Log("Hello");
			_dialogueCounter = 0;
			_answersCounter = 0;
			_interactionOver = false;
			_displayReturn = false;
			DisplayChoices = false;
			DisplayContinue = false;
		}


		// Write text to screen
		IEnumerator TypeText (string message) {
			for (int i = 0; i < message.Length; i++) {
				_textComp.text += message[i];
				yield return new WaitForSeconds (LetterPause);
			}
			if (_interactionOver) {
				DisplayChoices = false;
				_displayReturn = true;
				//return true;
			}
			else if (_dialogueCounter == Messages.Length) {
				DisplayContinue = true;
			}
		}

		// Update is called once per frame
		public void Update () {
		}
	}
}
