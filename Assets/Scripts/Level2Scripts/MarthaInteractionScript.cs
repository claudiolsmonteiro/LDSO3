using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Level2Scripts
{
    public class MarthaInteractionScript : MonoBehaviour {

        // TextTyper variables
        public float LetterPause = 0.05f;
        public GameObject Button;
        string _message;
        Text _textComp;
  
        // Dialogue Variables
        public string[] MarthaLines = {"Hey Jimmy! Can you please help Scott? He's trying to climb the stairs but he can't seem to do it without help.", "Cool, he's right by the school's entrance"};
        public string[] Replies1 = {"Hi! Sure, I'll help him!", "Hi! Sorry, but I don't want to arrive late to my class..."};
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


        // Initializ
        public void Start() {

            // Initialize variables
            InitializeCtrlVariables();

            _textComp = GameObject.Find("otherText").GetComponent<Text>();

            // Load possible actions
            _answersList = new List<string[]>();
            _answersList.Add(Replies1);
           // _answersList.Add(Replies2);

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
            Debug.Log(MarthaLines[_dialogueCounter]);

            if (_interactionOver || _dialogueCounter == MarthaLines.Length){
                return;
            }
            if (!_textComp.Equals("")) {
                ClearText();
            }
            HideButtons();
            _message = MarthaLines[_dialogueCounter];
            StartCoroutine(TypeText(_message));
            _dialogueCounter++;
        }

        void SaidNo() {
            if (_interactionOver){
                return;
            }
            ClearText();
            HideButtons();
            _message = "Oh, very nice of you.... Nevermind, I'll find someone else";
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
            if (_displayReturn && !Button.activeSelf) {
                Button.SetActive(true);
                Button returnButton = Button.GetComponent<Button>();
                returnButton.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene("3-LookingForScottB");
                });
            }
            if (DisplayContinue) {
                Button.SetActive(true);
                Button returnButton = Button.GetComponent<Button>();
                Text returnButtonText = Button.GetComponentInChildren<Text>();
                returnButtonText.text = "Continue";
                returnButton.onClick.RemoveAllListeners();
                returnButton.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene("3-LookingForScott");
                });
            }

        }

        void InitializeCtrlVariables() {
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
            else if (_dialogueCounter == MarthaLines.Length) {
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
