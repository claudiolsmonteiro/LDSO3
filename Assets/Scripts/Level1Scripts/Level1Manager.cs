using Assets.Scripts.ParserXML;
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
        public GameObject Button;
        string _message;
        string _nomessage;
        Text _textComp;

        // Dialogue Variables
        public string[] Messages; 
        public string[] Replies1;
        public string[] Replies2;
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

            Assets.Scripts.ParserXML.Parser cenas = new Assets.Scripts.ParserXML.Parser();
            List<NPC> npcs = cenas.npcs;

            Messages = new string[3];
            Replies1 = new string[2];
            Replies2 = new string[2];
            
            Messages[0] = npcs[0].dialogues[0].Text;
            Messages[1] = npcs[0].dialogues[1].Text;
            Messages[2] = npcs[0].dialogues[1].Answer;

            Replies1[0] = npcs[0].dialogues[0].Options[0];
            Replies1[1] = npcs[0].dialogues[0].Options[1];

            Replies2[0] = npcs[0].dialogues[1].Options[0];
            Replies2[1] = npcs[0].dialogues[1].Options[1];

            _nomessage = npcs[0].dialogues[6].Text;

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
            StartCoroutine(TypeText(_nomessage));
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
            else {
                DisplayAnswers(_answersList[_answersCounter]);
            }
        }

        // Update is called once per frame
        public void Update () {
        }
    }
}
