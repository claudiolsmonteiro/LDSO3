using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpingScottScript : MonoBehaviour
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

    private string[] _scottLines = { "Hey, Scott. \n Sorry I didn't help you before",
                                    "Do you want some help?" };

    private string[] _jimmyLines = { "Oh, hey Jimmy. No problem, but you should be more careful with what you say",
                                     "Sure, if you don't mind"};


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
        _mainCharacterTurn = true;
        _interactionOver = false;
        displayButton = false;
        _professorCounter = _jimmyCounter = 0;
        interaction();
    }

    public void interaction()
    {
        string line;
        if (_professorCounter == _scottLines.Length && _jimmyCounter == _jimmyLines.Length)
        {
            _interactionOver = true;
            return;
        }
        else
        {
            clearText();
            if (_mainCharacterTurn)
            {
                line = _scottLines[_professorCounter];
            }
            else
            {
                line = _jimmyLines[_jimmyCounter];

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
            if (GUI.Button(new Rect(300, 300, 100, 50), "Return"))
            {
                SceneManager.LoadScene("0-intro");
            }
        }
        if (displayButton && !_interactionOver)
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
            _professorCounter++;

        }
        else if (!_interactionOver)
        {
            cmtCharacterSpeechBalloon.enabled = true;
            for (int i = 0; i < message.Length; i++)
            {
                scottSpeech.text += message[i];
                yield return new WaitForSeconds(LetterPause);
            }
            _jimmyCounter++;
        }
        displayButton = true;
    }

}
