using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Level1Scripts
{
    public class Level1Manager : MonoBehaviour {

        // TextTyper variables
        public float LetterPause = 0.05f;
        string _message;
        Text _textComp;
  
        // Dialogue Variables
        public string[] Messages = {"Hi! Do you want to play football with us?", "Great! We're missing a player. Do you want to help us find one?", "Cool! See you later!"};
        public string[] Replies1 = {"Hi! Yeah, that sound's great", "Hi! Thank you, but no."};
        public string[] Replies2 = {"Sure, I'll look for somebody", "Actually, I think I'll maybe join you later"};
        private List<string[]> _answersList;
        private Button _answerA;
        private Button _answerB;
        private int _answersCounter;
        private int _dialogueCounter;

        // Control Variables
        private bool _displayReturn;
        public bool DisplayChoices;
        public bool DisplayContinue;
        private bool _interactionOver;

        // GUI styling
        private GUIStyle _style, _continueStyle;

        // Initializ
        public void Start() {

            //Parser cenas = new Parser();

            // Initialize variables
            InitializeCtrlVariables();

            _textComp = GameObject.Find("otherText").GetComponent<Text>();

            // Load possible actions
            _answersList = new List<string[]>();
            _answersList.Add(Replies1);
            _answersList.Add(Replies2);

            // Initialize buttons
            _answerA = GameObject.Find("answerA").GetComponent<Button>();
            _answerB = GameObject.Find("answerB").GetComponent<Button>();

            // Start actions
            HideButtons();
            ContinueDialogue();
        }

        void HideButtons() {
            _answerA.gameObject.SetActive(false);
            _answerB.gameObject.SetActive(false);
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
            HideButtons();
            _message = Messages[_dialogueCounter];
            StartCoroutine(TypeText(_message));
            _dialogueCounter++;
        }

        void SaidNo() {
            if (_interactionOver){
                return;
            }
            ClearText();
            HideButtons();
            _message = "Oh, ok. Maybe I'll see you later!";
            StartCoroutine(TypeText(_message));
            _interactionOver=true;
        }

        void DisplayAnswers(string[] arr) {
            _answerA.GetComponentInChildren<Text>().text = arr[0];
            _answerA.onClick.AddListener( () => ContinueDialogue());
            _answerA.gameObject.SetActive(true);

            _answerB.GetComponentInChildren<Text>().text = arr[1];
            _answerB.onClick.AddListener( () => SaidNo());
            _answerB.gameObject.SetActive(true);
            _answersCounter++;
        }

        public void OnGUI() {
            if (_displayReturn) {
                if (GUI.Button(new Rect(375, 225, 268, 25), "Return")) {
                    SceneManager.LoadScene("SchoolOutdoors");
                }
            }
            if (DisplayContinue) {
                if (GUI.Button(new Rect(375, 225, 268, 25), "Continue")) {
                    SceneManager.LoadScene("Level1a-LookingForAPlayer");
                }
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
            else {
                DisplayAnswers(_answersList[_answersCounter]);
            }
        }

        // Update is called once per frame
        public void Update () {
        }
    }
}
