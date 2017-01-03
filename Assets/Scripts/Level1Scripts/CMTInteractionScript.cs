using Assets.Scripts.ParserXML;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CMTInteractionScript : MonoBehaviour
{

    // TextTyper variables
    public float LetterPause = 0.05f;
    public GameObject Button;
    private string _message;

    //Character's GameObjects
    private Image _mainCharacterSpeechBalloon;
    private Text _mainCharacterSpeech;

    private Image _cmtCharacterSpeechBalloon;
    private Text _cmtCharacterSpeech;

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
    private string[] _mainCharacterLines;
    private string[] _cmtCharacterLines;

    private string[] _questionChoices1;

    private string[] _answers1;

    private string[] _questionChoices2;

    private string[] _answers2;

    private string[] _questionChoices3;

    private string[] _answers3;

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
    private readonly int[] _q1Scores = { 10, 0 };
    private readonly int[] _q2Scores = { 10, 5, 0, -5 };
    private readonly int[] _q3Scores = { 10, 5, 0, -5 };

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



    // Start interaction
    public void Start()
    {
        // initialize control variables
        _playerLevelScore = 0;
        _mainCharacterDialogueCounter = 0;
        _cmtCharacterDialogueCounter = 0;
        _mainCharacterTurn = true;
        _interactionOver = false;
        _writing = false;
        _displayChoices = true;
        _displayNextButton = false;
        _questionsIterator = 0;

        //initialize dialogue
        Assets.Scripts.ParserXML.Parser cenas = new Assets.Scripts.ParserXML.Parser();
        List<NPC> npcs = cenas.npcs;

        _mainCharacterLines = new string[1];
        _cmtCharacterLines = new string[1];
        _questionChoices1 = new string[2];
        _answers1 = new string[2];
        _questionChoices2 = new string[4];
        _answers2 = new string[4];
        _questionChoices3 = new string[4];
        _answers3 = new string[4];

        _mainCharacterLines[0] = npcs[0].dialogues[2].Text;
        _cmtCharacterLines[0] = npcs[0].dialogues[2].Answer;

        _questionChoices1[0] = npcs[0].dialogues[2].Options[0];
        _questionChoices1[1] = npcs[0].dialogues[2].Options[1];

        _answers1[0] = npcs[0].dialogues[3].Answer;
        _answers1[1] = npcs[0].dialogues[4].Answer;

        _questionChoices2[0] = npcs[0].dialogues[3].Options[0];
        _questionChoices2[1] = npcs[0].dialogues[3].Options[1];
        _questionChoices2[2] = npcs[0].dialogues[3].Options[2];
        _questionChoices2[3] = npcs[0].dialogues[3].Options[3];

        _answers2[0] = npcs[0].dialogues[5].Options[0];
        _answers2[1] = npcs[0].dialogues[5].Options[1];
        _answers2[2] = npcs[0].dialogues[5].Options[2];
        _answers2[3] = npcs[0].dialogues[5].Options[3];

        _questionChoices3[0] = npcs[0].dialogues[4].Options[0];
        _questionChoices3[1] = npcs[0].dialogues[4].Options[1];
        _questionChoices3[2] = npcs[0].dialogues[4].Options[2];
        _questionChoices3[3] = npcs[0].dialogues[4].Options[3];

        _answers3[0] = npcs[0].dialogues[6].Options[0];
        _answers3[1] = npcs[0].dialogues[6].Options[1];
        _answers3[2] = npcs[0].dialogues[6].Options[2];
        _answers3[3] = npcs[0].dialogues[6].Options[3];


        // Get main character's speech objects
        _mainCharacterSpeechBalloon = GameObject.Find("mainCharacterSpeech").GetComponent<Image>();
        _mainCharacterSpeech = GameObject.Find("mainCharacterSpeechText").GetComponent<Text>();

        // Get cmt character's speech objects
        _cmtCharacterSpeechBalloon = GameObject.Find("CMTcharacterSpeech").GetComponent<Image>();
        _cmtCharacterSpeech = GameObject.Find("CMTcharacterSpeechText").GetComponent<Text>();

        // Get buttons
        _answerA = GameObject.Find("answerA").GetComponent<Button>();
        _answerB = GameObject.Find("answerB").GetComponent<Button>();
        _answerC = GameObject.Find("answerC").GetComponent<Button>();
        _answerD = GameObject.Find("answerD").GetComponent<Button>();

        // Hide Speech balloons
        _mainCharacterSpeechBalloon.enabled = false;
        _cmtCharacterSpeechBalloon.enabled = false;

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
        // TODO: Should be done with the xmlParser
        _questionsList = new List<string[]>();
        _questionsList.Add(_questionChoices1);
        _questionsList.Add(_questionChoices2);
        _questionsList.Add(_questionChoices3);

        //Initialize interaction controller
        _answerController = new List<int[]>();
        _answerController.Add(_answer1Controller);
        _answerController.Add(_answer2Controller);
        _answerController.Add(_answer2Controller);

        //Initialize scores controller
        _playerScore = new List<int[]>();
        _playerScore.Add(_q1Scores);
        _playerScore.Add(_q2Scores);
        _playerScore.Add(_q3Scores);

        _cmtCharacterSpeechList = new List<string[]> { _answers1, _answers2, _answers3 };

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
            _cmtCharacterSpeechBalloon.enabled = false;
            ClearText(_cmtCharacterSpeech);
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
            ClearText(_cmtCharacterSpeech);
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
        // Clean Everything
        ClearText(_cmtCharacterSpeech);
        // Write Text
        _message = _cmtCharacterSpeechList[_questionsIterator][number];
        StartCoroutine(WriteCmTspeech(_message, number));
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

    void GetReaction(int playerChoice)
    {
        Image reaction = null;
        switch(playerChoice)
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
        }
        if (reaction != null)
        {
            reaction.enabled = true;
            reaction.canvasRenderer.SetAlpha(1.0f);
            reaction.CrossFadeAlpha(0, 1.5f, false);
        }
    }



    void ButtonClicked(int buttonNumber)
    {
        if (_questionsIterator == _cmtCharacterSpeechList.Count - 1)
        {

        }
        HideButtons();
        GetReaction(_playerScore[_questionsIterator][buttonNumber]);
        GetCmTresponse(buttonNumber);
    }


    public void OnGUI()
    {
        if (_displayNextButton && !Button.activeSelf)
        {
            Button.SetActive(true);
            Button returnButton = Button.GetComponent<Button>();
            Text returnButtonText = Button.GetComponentInChildren<Text>();
            returnButtonText.text = "Continue";
            returnButton.onClick.RemoveAllListeners();
            returnButton.onClick.AddListener(() =>
            {
                _displayNextButton = false;
                Button.SetActive(false);
                ContinueDialogue();
            });
        }
        if (_interactionOver && !Button.activeSelf)
        {
            Button.SetActive(true);
            Button returnButton = Button.GetComponent<Button>();
            Text returnButtonText = Button.GetComponentInChildren<Text>();
            returnButtonText.text = "Return";
            returnButton.onClick.RemoveAllListeners();
            returnButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("CMTandExercise");
            });
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
            _cmtCharacterSpeechBalloon.enabled = true;
            for (int i = 0; i < message.Length; i++)
            {
                _cmtCharacterSpeech.text += message[i];
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


    IEnumerator WriteCmTspeech(string message, int number)
    {
        if (!_writing)
        {
            _writing = true;
            if (!_interactionOver)
            {
                _cmtCharacterSpeechBalloon.enabled = true;
                ClearText(_cmtCharacterSpeech);
                for (int i = 0; i < message.Length; i++)
                {
                    _cmtCharacterSpeech.text += message[i];
                    yield return new WaitForSeconds(LetterPause);
                }
                _writing = false;
                _playerLevelScore += _playerScore[_questionsIterator][number];
                if (_questionsIterator == _cmtCharacterSpeechList.Count - 1 || _answerController[_questionsIterator][number] == 1)
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
