using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfessorInteractionScript : MonoBehaviour
{

    //Character's GameObjects
    private Image _mainCharacterSpeechBalloon;
    private Text _mainCharacterSpeech;
    private string _mainCharacterSpeechText;
    public float LetterPause = 0.05f;
    private bool _interactionOver;

    private Image cmtCharacterSpeechBalloon;
    private Text scottSpeech;
    private string _cmtCharacterSpeechText;

    private string[] professorLines = { "Good morning, Jimmy! \n Have you seen Scott? \n He's usually not late for classes",
                                        "And don't you think you should help him? Go get him right now!" };

    private string[] jimmyLines = { "Good morning, Mr Woods. Yes, he's stuck at the stairs.",
                                     "Yes, Mr Woods..."};


    private int _jimmyCounter;
    private int _professorCounter;
    private bool _mainCharacterTurn;
    private bool displayButton;

    // Use this for initialization
    void Start()
    {

        // Get main character's speech objects
        _mainCharacterSpeechBalloon = GameObject.Find("mainCharacterSpeech").GetComponent<Image>();
        _mainCharacterSpeech = GameObject.Find("mainCharacterSpeechText").GetComponent<Text>();
        _mainCharacterSpeechText = _mainCharacterSpeech.text;

        // Get cmt character's speech objects
        cmtCharacterSpeechBalloon = GameObject.Find("CMTcharacterSpeech").GetComponent<Image>();
        scottSpeech = GameObject.Find("CMTcharacterSpeechText").GetComponent<Text>();
        _cmtCharacterSpeechText = _mainCharacterSpeech.text;
        _mainCharacterTurn = false;
        _interactionOver = false;
        displayButton = false;
        _professorCounter = _jimmyCounter = 0;
        interaction();
    }

    public void interaction()
    {
        string line;
        if (_professorCounter == professorLines.Length && _jimmyCounter == jimmyLines.Length)
        {
            _interactionOver = true;
            return;
        }
        else
        {
            clearText();
            if (_mainCharacterTurn)
            {
                line = jimmyLines[_jimmyCounter];
            }
            else
            {
                line = professorLines[_professorCounter];

            }
        }
        StartCoroutine(TypeText(line));

    }

    public void clearText()
    {
        scottSpeech.text = "";
        _mainCharacterSpeech.text = "";

        cmtCharacterSpeechBalloon.enabled = false;
        _mainCharacterSpeechBalloon.enabled = false;
        

    }

    void OnGUI()
    {
        if (_interactionOver)
        {
            if (GUI.Button(new Rect(600, 250, 100, 50), "Return"))
            {
                SceneManager.LoadScene("4-FetchingScott");
            }
        }
        if (displayButton)
        {
            if (GUI.Button(new Rect(600, 250, 100, 50), "Continue"))
            {
                _mainCharacterTurn = !_mainCharacterTurn;
                displayButton = false;
                interaction();
            }
        }
    }


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
            _jimmyCounter++;
        }
        else if (!_interactionOver)
        {
            cmtCharacterSpeechBalloon.enabled = true;
            for (int i = 0; i < message.Length; i++)
            {
                scottSpeech.text += message[i];
                yield return new WaitForSeconds(LetterPause);
            }
            _professorCounter++;
        }
        displayButton = true;
    }

}
