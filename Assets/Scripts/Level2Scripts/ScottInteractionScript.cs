using Assets.Scripts.ParserXML;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Level2Scripts
{
    public class ScottInteractionScript : MonoBehaviour
    {

        // TextTyper variables
        public float LetterPause = 0.05f;
        public GameObject Button;
        string _message;

        //Character's GameObjects
        private Image _mainCharacterSpeechBalloon;
        private Text _mainCharacterSpeech;
        private string _mainCharacterSpeechText;

        private Image cmtCharacterSpeechBalloon;
        private Text scottSpeech;
        private string _cmtCharacterSpeechText;

        // Reactions
        private Image _excellent;
        private Image _good;
        private Image _bad;
        private Image _awful;

        // Character Response Game Objects
        private Button _answerA;
        private Button _answerB;
        private Button _answerC;
        private Button _answerD;


        // Dialogue Variables

        private string[] _PlayerLines1;

        private readonly int[] _PlayerLines1Scores = { 10, 5, 0, -5 };
        private readonly int[] _PlayerLines2AScores = { 10, -6 };
        private readonly int[] _PlayerLines2BScores = { 10, -6 };
        private readonly int[] _PlayerLines2CScores = { 10, -6 };
        private readonly int[] _PlayerLines2DScores = { 10, -6 };

        private bool _didntHelp = false;

        private string[] _PlayerLines2A;

        private string[] _PlayerLines2B;

        private string[] _PlayerLines2C;

        private string[] _PlayerLines2D;

        private string[] _ScottLines1;

        private string[] _ScottLines2A;

        private string[] _ScottLines2B;

        private string[] _ScottLines2C;

        private string[] _ScottLines2D;

        private string _optionsToPrint;

        private int _playerChoice;

        // 1 -> End of Interaction
        private List<int[]> _answerController;
        private readonly int[] _answer1Controller = { 0, 0 };
        private readonly int[] _answer2Controller = { 1, 1, 0, 0 };
        private readonly int[] _answer3Controller = { 0, 0, 0, 0 };


        // Player scores work as follows
        // +10 for excellent action
        //  +5 for goot action
        //   0 for bad action
        //  -5 for an awfull action
        private List<int[]> _playerScore;
        private int _playerLevelScore;
        private readonly int[] _Lines1Scores = { 10, 5, 0, -5 };
        private readonly int[] _q2Scores = { 10, 5, 0, -5 };
        private readonly int[] _q3Scores = { 10, 5, 0, -5 };

        private List<string[]> _PlayerLinesList;
        private List<string[]> _ScottLinesList;

        // Control Variables
        private bool _mainCharacterTurn;
        private bool _displayChoices;
        private bool _displayNextButton;
        private bool _interactionOver;
        private int _mainCharacterDialogueCounter;
        private int _cmtCharacterDialogueCounter;
        private int _interactionIterator;
        private bool _writing;



        // Start interaction
        public void Start()
        {
            Assets.Scripts.ParserXML.Parser parser = new Assets.Scripts.ParserXML.Parser();
            List<NPC> npcs = parser.npcs;

            _PlayerLines1 = new string[4];
            _PlayerLines2A = new string[2];
            _PlayerLines2B = new string[2];
            _PlayerLines2C = new string[2];
            _PlayerLines2D = new string[2];
            _ScottLines1 = new string[4];
            _ScottLines2A = new string[4];
            _ScottLines2B = new string[4];
            _ScottLines2C = new string[4];
            _ScottLines2D = new string[4];

            _PlayerLines1[0] = npcs[3].dialogues[0].Options[0];
            _PlayerLines1[1] = npcs[3].dialogues[0].Options[1];
            _PlayerLines1[2] = npcs[3].dialogues[0].Options[2];
            _PlayerLines1[3] = npcs[3].dialogues[0].Options[3];

            _PlayerLines2A[0] = npcs[3].dialogues[1].Options[0];
            _PlayerLines2A[1] = npcs[3].dialogues[1].Options[1];

            _PlayerLines2B[0] = npcs[3].dialogues[2].Options[0];
            _PlayerLines2B[1] = npcs[3].dialogues[2].Options[1];

            _PlayerLines2C[0] = npcs[3].dialogues[3].Options[0];
            _PlayerLines2C[1] = npcs[3].dialogues[3].Options[1];

            _PlayerLines2D[0] = npcs[3].dialogues[4].Options[0];
            _PlayerLines2D[1] = npcs[3].dialogues[4].Options[1];

            _ScottLines1[0] = npcs[3].dialogues[5].Options[0];
            _ScottLines1[1] = npcs[3].dialogues[5].Options[1];
            _ScottLines1[2] = npcs[3].dialogues[5].Options[2];
            _ScottLines1[3] = npcs[3].dialogues[5].Options[3];

            _ScottLines2A[0] = npcs[3].dialogues[6].Options[0];
            _ScottLines2A[1] = npcs[3].dialogues[6].Options[1];

            _ScottLines2B[0] = npcs[3].dialogues[7].Options[0];
            _ScottLines2B[1] = npcs[3].dialogues[7].Options[1];

            _ScottLines2C[0] = npcs[3].dialogues[8].Options[0];
            _ScottLines2C[1] = npcs[3].dialogues[8].Options[1];

            _ScottLines2D[0] = npcs[3].dialogues[9].Options[0];
            _ScottLines2D[1] = npcs[3].dialogues[9].Options[1];

            // initialize control variables
            _playerLevelScore = 0;
            _mainCharacterDialogueCounter = 0;
            _cmtCharacterDialogueCounter = 0;
            _mainCharacterTurn = true;
            _interactionOver = false;
            _writing = false;
            _displayChoices = true;
            _displayNextButton = false;
            _interactionIterator = 0;


            // Get Character's Lines
            //  XMLParser("level1, mainCharacterLines ,cmtCharacterLines");

            // Get main character's speech objects
            _mainCharacterSpeechBalloon = GameObject.Find("mainCharacterSpeech").GetComponent<Image>();
            _mainCharacterSpeech = GameObject.Find("mainCharacterSpeechText").GetComponent<Text>();
            _mainCharacterSpeechText = _mainCharacterSpeech.text;

            // Get cmt character's speech objects
            cmtCharacterSpeechBalloon = GameObject.Find("CMTcharacterSpeech").GetComponent<Image>();
            scottSpeech = GameObject.Find("CMTcharacterSpeechText").GetComponent<Text>();
            _cmtCharacterSpeechText = _mainCharacterSpeech.text;

            // Get buttons
            _answerA = GameObject.Find("answerA").GetComponent<Button>();
            _answerB = GameObject.Find("answerB").GetComponent<Button>();
            _answerC = GameObject.Find("answerC").GetComponent<Button>();
            _answerD = GameObject.Find("answerD").GetComponent<Button>();

            // Hide Speech balloons
            _mainCharacterSpeechBalloon.enabled = false;
            cmtCharacterSpeechBalloon.enabled = false;

            // Init Reactions & hide everything
            _excellent = GameObject.Find("excellent").GetComponent<Image>();
            _excellent.enabled = false;
            _good = GameObject.Find("good").GetComponent<Image>();
            _good.enabled = false;
            _bad = GameObject.Find("bad").GetComponent<Image>();
            _bad.enabled = false;
            _awful = GameObject.Find("awful").GetComponent<Image>();
            _awful.enabled = false;


            // initialize questions and answers
            _PlayerLinesList = new List<string[]>();
            _PlayerLinesList.Add(_PlayerLines2A);
            _PlayerLinesList.Add(_PlayerLines2B);
            _PlayerLinesList.Add(_PlayerLines2C);
            _PlayerLinesList.Add(_PlayerLines2D);


            //Initialize interaction controller
            _answerController = new List<int[]>();
            _answerController.Add(_answer1Controller);
            //  _answerController.Add(_answer2Controller);
            // _answerController.Add(_answer2Controller);

            //Initialize scores controller
            _playerScore = new List<int[]>();
            /*  _playerScore.Add(_q1Scores);
          _playerScore.Add(_q2Scores);
          _playerScore.Add(_q3Scores);*/

            _ScottLinesList = new List<string[]> { _ScottLines1, _ScottLines2A, /*_answers3*/ };

            // Hide response buttons
            HideButtons();
            StartCoroutine(Wait2Seconds());
            // start actions


        }

        IEnumerator Wait2Seconds()
        {
            yield return new WaitForSeconds(1);
            LoadQuestions(_PlayerLines1);
        }

        void AskQuestions()
        {
            switch (_playerChoice)
            {
                case 0:
                    LoadQuestions(_PlayerLines2A);
                    break;
                case 1:
                    LoadQuestions(_PlayerLines2B);
                    break;
                case 2:
                    LoadQuestions(_PlayerLines2C);
                    break;
                case 3:
                    LoadQuestions(_PlayerLines2D);
                    break;
            }
        }


        void GetScottResponse(int number)
        {
            // Clean Everything
            ClearText(scottSpeech);
            // Write Text
            Debug.Log("playerChoice: " + _playerChoice);
            if (_interactionIterator != 0)
            {
                switch (_playerChoice)
                {
                    case 0:
                        _message = _ScottLines2A[number];
                        break;
                    case 1:
                        _message = _ScottLines2B[number];
                        break;
                    case 2:
                        _message = _ScottLines2C[number];
                        break;
                    case 3:
                        _message = _ScottLines2D[number];
                        break;
                }
            }
            else
                _message = _ScottLines1[number];

            StartCoroutine(writeScottSpeech(_message, number));
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
        }

        // Clear Speech Balloon
        void ClearText(Text textObject)
        {
            if (_writing)
            {
                return;
            }
            textObject.text = "";
        }

        void GetReaction(int PlayerChoice)
        {
            Debug.Log("reaction: " + PlayerChoice);
            Image reaction = null;
            switch (PlayerChoice)
            {
                case 10:
                    reaction = _excellent;
                    break;
                case 5:
                    reaction = _good;
                    break;
                case 0:
                    reaction = _bad;
                    break;
                case -5:
                    reaction = _awful;
                    break;
                case -6:
                    reaction = _awful;
                    _didntHelp = true;
                    break;
            }
            reaction.enabled = true;
            reaction.canvasRenderer.SetAlpha(1.0f);
            reaction.CrossFadeAlpha(0, 1.5f, false);

        }



        void ButtonClicked(int buttonNumber)
        {
            if (_interactionIterator == _ScottLinesList.Count - 1)
            {

            }

            HideButtons();
            int tempScore = 0;
            if (_interactionIterator == 0)
            {
                tempScore = _PlayerLines1Scores[buttonNumber];
                _playerChoice = buttonNumber;
            }
            else
            {
                switch (_playerChoice)
                {
                    case 0:
                        tempScore = _PlayerLines2AScores[buttonNumber];
                        break;
                    case 1:
                        tempScore = _PlayerLines2BScores[buttonNumber];
                        break;
                    case 2:
                        tempScore = _PlayerLines2CScores[buttonNumber];
                        break;
                    case 3:
                        tempScore = _PlayerLines2DScores[buttonNumber];
                        break;
                }

            }

            _playerLevelScore += tempScore;
            Debug.Log(tempScore);
            GetReaction(tempScore);
            GetScottResponse(buttonNumber);
        }


        public void OnGUI()
        {
            if (_interactionOver && !Button.activeSelf)
            {
                Button.SetActive(true);
                Button returnButton = Button.GetComponent<Button>();
                Text returnButtonText = Button.GetComponentInChildren<Text>();
                Debug.Log("didnt help: " + _didntHelp);
                returnButtonText.text = "Return";
                returnButton.onClick.RemoveAllListeners();
                if (_didntHelp)
                {
                    returnButton.onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("1-Corridor");
                    });
                }
                else
                {
                    returnButton.onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("0-intro");
                    });
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
                    scottSpeech.text += message[i];
                    yield return new WaitForSeconds(LetterPause);
                }
            }
            if (_mainCharacterDialogueCounter == _PlayerLinesList.Count && _cmtCharacterDialogueCounter == _ScottLinesList.Count)
            {
                AskQuestions();
            }
            else
                _displayNextButton = true;
        }


        IEnumerator writeScottSpeech(string message, int number)
        {
            if (!_writing)
            {
                _writing = true;
                if (!_interactionOver)
                {
                    cmtCharacterSpeechBalloon.enabled = true;
                    ClearText(scottSpeech);
                    for (int i = 0; i < message.Length; i++)
                    {
                        scottSpeech.text += message[i];
                        yield return new WaitForSeconds(LetterPause);
                    }
                    _writing = false;
                    if (_interactionIterator == _ScottLinesList.Count - 1)
                    {
                        HideButtons();
                        _interactionOver = true;
                    }
                    else if (_interactionIterator < (_ScottLinesList.Count - 1))
                    {
                        _interactionIterator++;
                        AskQuestions();
                    }
                }
            }

        }

    }
}
