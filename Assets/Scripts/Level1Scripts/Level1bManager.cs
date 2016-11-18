using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Level1Scripts
{
    public class Level1BManager : MonoBehaviour
    {

        // TextTyper variables
        public float LetterPause = 0.05f;
        string _message;
        Text _textComp;

        //Character's GameObjects
        private Image _mainCharacterSpeechBalloon;
        private Text _mainCharacterSpeech;
        private string _mainCharacterSpeechText;

        private Image cmtCharacterSpeechBalloon;
        private Text cmtCharacterSpeech;
        private string _cmtCharacterSpeechText;

        // Character Response Game Objects
        private Button _answerA;
        private Button _answerB;
        private Button _answerC;
        private Button _answerD;

        private Button _continueButton;
        private Button _returnButton;

        // Dialogue Variables
        private readonly string[] _mainCharacterLines = { "Hey, new kid! Wanna join us playing football? We need one more player." };
        private readonly string[] _cmtCharacterLines = { "Hey there, thanks. I would love to play with you, but I cannot play football." };

        private readonly string[] _questionChoices1 = {
            "Why? Is there something wrong?",
            "Why not? You don't look sick or anything."
        };

        private readonly string[] _answers1 = {
            "Yes, I have a disease called Charcot-Marie-Tooth. It makes it really hard for me to maintain balance and control my body like you.",
            "You shouldn't assume things just by looking at people. I'm not sick, but I actually have this disease called Charcot-Marie-Tooth. It makes it really hard for me to maintain balance and control my body like you."
        };

        private readonly string[] _questionChoices2 = {
            "Oh, I'm sorry to hear that. If you want, I can ask the guys to play something other than football so you can join us." ,
            "Oh, I'm sorry to hear that. I guess we'll have to play with an odd number of players, then. See you later!",
            "You can be the goalkeeper, then. You won't have to move too much and we get to play with an even number of players.",
            "I guess no one will want to play with you then. I'll try to find someone else."
        };

        private readonly string[] _answers2 = {
            "Thanks. I really appreciate it.",
            "Thanks. See you later then.",
            "I think I'll pass.",
            "That was not very nice from you. You seemed like a respectful guy at first."
        };

        private readonly string[] _questionChoices3 = {
            "I'm sorry I said that. That was rude. If you want, I can ask the guys to play something other than football so you can join us.",
            "I'm sorry I said that. That was rude. I guess we'll have to play with an odd number of players, then. See you later!",
            "You can be the goalkeeper, then. You won't have to move too much and we get to play with an even number of players.",
            "Who cares? No one will want to play with you, then. I'll try to find someone else."
        };

        private readonly string[] _answers3 = {
            "No problem, I don't think you meant to be rude. I'd really appreciate it if you guys played something else just so I can play too.",
            "Sure, no problem. See you later, then.",
            "No thanks. I'd rather do something else.",
            "You are really rude."
        };

        private List<string[]> _questionsList;
        private List<string[]> _cmtCharacterSpeechList;

        // Control Variables
        private bool _mainCharacterTurn;
        private bool _displayChoices;
        private bool _displayNextButton;
        private bool _interactionOver;
        private int _mainCharacterDialogueCounter;
        private int _cmtCharacterDialogueCounter;
        private int _questionsIterator;
        private bool _writing;


        // GUI styling
        private GUIStyle _style, _continueStyle;

        // Start interaction
        public void Start()
        {

            // initialize control variables
            _mainCharacterDialogueCounter = 0;
            _cmtCharacterDialogueCounter = 0;
            _mainCharacterTurn = true;
            _interactionOver = false;
            _writing = false;
            _displayChoices = true;
            _displayNextButton = false;
            _questionsIterator = 0;

            // Get Character's Lines
            //  XMLParser("level1, mainCharacterLines ,cmtCharacterLines");

            // Get main character's speech objects
            _mainCharacterSpeechBalloon = GameObject.Find("mainCharacterSpeech").GetComponent<Image>();
            _mainCharacterSpeech = GameObject.Find("mainCharacterSpeechText").GetComponent<Text>();
            _mainCharacterSpeechText = _mainCharacterSpeech.text;

            // Get cmt character's speech objects
            cmtCharacterSpeechBalloon = GameObject.Find("CMTcharacterSpeech").GetComponent<Image>();
            cmtCharacterSpeech = GameObject.Find("CMTcharacterSpeechText").GetComponent<Text>();
            _cmtCharacterSpeechText = _mainCharacterSpeech.text;

            // Get buttons
            _answerA = GameObject.Find("answerA").GetComponent<Button>();
            _answerB = GameObject.Find("answerB").GetComponent<Button>();
            _answerC = GameObject.Find("answerC").GetComponent<Button>();
            _answerD = GameObject.Find("answerD").GetComponent<Button>();
            _continueButton = GameObject.Find("continue").GetComponent<Button>();
            _returnButton = GameObject.Find("return").GetComponent<Button>();

            // Hide Speech balloons
            _mainCharacterSpeechBalloon.enabled = false;
            cmtCharacterSpeechBalloon.enabled = false;

            // initialize questions and answers
            // TODO: Should be done with the xmlParser
            _questionsList = new List<string[]>();
            _questionsList.Add(_questionChoices1);
            _questionsList.Add(_questionChoices2);
            _questionsList.Add(_questionChoices3);

            _cmtCharacterSpeechList = new List<string[]> {_answers1, _answers2, _answers3};
            Debug.Log("speechList: " + _cmtCharacterSpeechList.Count);


            // Hide response buttons
            HideButtons();

            // start actions
            ContinueDialogue();
        }

        // Dialogue cycle
        void ContinueDialogue()
        {
            if (_mainCharacterTurn)
            {
                // clear everything
                cmtCharacterSpeechBalloon.enabled = false;
                ClearText(cmtCharacterSpeech);
                ClearText(_mainCharacterSpeech);
                // Write Text
                _message = _mainCharacterLines[_mainCharacterDialogueCounter];
                StartCoroutine(TypeText(_message));
                _mainCharacterDialogueCounter++;
            }
            else
            {
                // Clean Everything
                _mainCharacterSpeechBalloon.enabled = false;
                ClearText(cmtCharacterSpeech);
                ClearText(_mainCharacterSpeech);
                // Write Text
                _message = _cmtCharacterLines[_cmtCharacterDialogueCounter];
                StartCoroutine(TypeText(_message));
                _cmtCharacterDialogueCounter++;
            }
            _mainCharacterTurn = !_mainCharacterTurn;
        }


        void AskQuestions()
        {
            LoadQuestions(_questionsList[_questionsIterator]);
        }


        void GetCmTresponse(int number)
        {
            Debug.Log("questionsIterator: " + _questionsIterator);
            // Clean Everything
            ClearText(cmtCharacterSpeech);
            // Write Text
            _message = _cmtCharacterSpeechList[_questionsIterator][number];
            StartCoroutine(WriteCmTspeech(_message));
        }


        void LoadQuestions(string[] arr)
        {

            _answerA.GetComponentInChildren<Text>().text = arr[0];
            _answerA.onClick.AddListener(() => ButtonClicked(0));
            _answerA.gameObject.SetActive(true);

            _answerB.GetComponentInChildren<Text>().text = arr[1];
            _answerB.onClick.AddListener(() => ButtonClicked(1));
            _answerB.gameObject.SetActive(true);

            if (arr.Length > 2)
            {
                _answerC.GetComponentInChildren<Text>().text = arr[2];
                _answerC.onClick.AddListener(() => ButtonClicked(2));
                _answerC.gameObject.SetActive(true);

            }
            if (arr.Length == 4)
            {
                _answerD.GetComponentInChildren<Text>().text = arr[3];
                _answerD.onClick.AddListener(() => ButtonClicked(3));
                _answerD.gameObject.SetActive(true);
            }
        }

        // Hide buttons
        void HideButtons()
        {
            _answerA.gameObject.SetActive(false);
            _answerB.gameObject.SetActive(false);
            _answerC.gameObject.SetActive(false);
            _answerD.gameObject.SetActive(false);
            _continueButton.gameObject.SetActive(false);
            _returnButton.gameObject.SetActive(false);
        }

        // Clear Speech Balloon
        void ClearText(Text textObject)
        {
            textObject.text = "";
        }


        void ButtonClicked(int buttonNumber)
        {
            if (_questionsIterator == _cmtCharacterSpeechList.Count - 1)
            {

            }
            HideButtons();
            GetCmTresponse(buttonNumber);
        }


        public void OnGUI()
        {
            if (_displayNextButton)
            {
                if (GUI.Button(new Rect(375, 225, 268, 25), "Continue"))
                {
                    ContinueDialogue();
                    _displayNextButton = false;
                }
            }
            if (_interactionOver)
            {
                if (GUI.Button(new Rect(375, 225, 268, 25), "Return"))
                {
                    SceneManager.LoadScene("CMTandExercise");
                }

            }
        }


        // Write text to screen
        IEnumerator TypeText(string message)
        {
            if (_mainCharacterTurn)
            {
                _mainCharacterSpeechBalloon.enabled = true;
                for (int i = 0; i < message.Length; i++)
                {
                    _mainCharacterSpeech.text += message[i];
                    yield return new WaitForSeconds(LetterPause);
                }
            }
            else if (!_interactionOver)
            {
                cmtCharacterSpeechBalloon.enabled = true;
                for (int i = 0; i < message.Length; i++)
                {
                    cmtCharacterSpeech.text += message[i];
                    yield return new WaitForSeconds(LetterPause);
                }
            }
            if (_mainCharacterDialogueCounter == _mainCharacterLines.Length && _cmtCharacterDialogueCounter == _cmtCharacterLines.Length)
            {
                AskQuestions();
            }
            else
                _displayNextButton = true;
        }


        IEnumerator WriteCmTspeech(string message)
        {
            if (!_writing)
            {
                _writing = true;
                if (!_interactionOver)
                {
                    cmtCharacterSpeechBalloon.enabled = true;
                    ClearText(cmtCharacterSpeech);
                    for (int i = 0; i < message.Length; i++)
                    {
                        cmtCharacterSpeech.text += message[i];
                        yield return new WaitForSeconds(LetterPause);
                    }
                    _writing = false;
                    if (_questionsIterator == _cmtCharacterSpeechList.Count - 1)
                    {
                        HideButtons();
                        _interactionOver = true;
                    }
                    else if (_questionsIterator < (_questionsList.Count - 1))
                    {
                        _questionsIterator++;
                        AskQuestions();
                    }
                }
            }

        }

        // Update is called once per frame
        public void Update()
        {
        }
    }
}
