using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Level3Scripts
{
    public class HelpingScottScript : MonoBehaviour
    {

        //Character's GameObjects
        private Image _mainCharacterSpeechBalloon;
        private Text _mainCharacterSpeech;
        private string _mainCharacterSpeechText;
        public float LetterPause = 0.05f;
        public GameObject Button;
        private bool _interactionOver;

        private Image _cmtCharacterSpeechBalloon;
        private Text _scottSpeech;
        private string _cmtCharacterSpeechText;

        private readonly string[] _scottLines = { "Hey, Scott. \n Sorry I didn't help you before",
            "Do you want some help?" };

        private readonly string[] _jimmyLines = { "Oh, hey Jimmy. No problem, but you should be more careful with what you say",
            "Sure, if you don't mind"};


        private int _jimmyCounter;
        private int _professorCounter;
        private bool _mainCharacterTurn;
        private bool _displayButton;

        // Use this for initialization
        void Start()
        {

            // Get main character's speech objects
            _mainCharacterSpeechBalloon = GameObject.Find("mainCharacterSpeech").GetComponent<Image>();
            _mainCharacterSpeech = GameObject.Find("mainCharacterSpeechText").GetComponent<Text>();
            _mainCharacterSpeechText = _mainCharacterSpeech.text;

            // Get cmt character's speech objects
            _cmtCharacterSpeechBalloon = GameObject.Find("CMTcharacterSpeech").GetComponent<Image>();
            _scottSpeech = GameObject.Find("CMTcharacterSpeechText").GetComponent<Text>();
            _cmtCharacterSpeechText = _mainCharacterSpeech.text;
            _mainCharacterTurn = true;
            _interactionOver = false;
            _displayButton = false;
            _professorCounter = _jimmyCounter = 0;
            Interaction();
        }

        public void Interaction()
        {
            string line;
            ClearText();
            if (_mainCharacterTurn)
            {
                line = _scottLines[_professorCounter];
            }
            else
            {
                line = _jimmyLines[_jimmyCounter];

            }
            StartCoroutine(TypeText(line));
        }

        public void ClearText()
        {
            _scottSpeech.text = "";
            _mainCharacterSpeech.text = "";

            _cmtCharacterSpeechBalloon.enabled = false;
            _mainCharacterSpeechBalloon.enabled = false;


        }

        [UsedImplicitly]
        void OnGUI()
        {
            if (_interactionOver && !Button.activeSelf)
            {
                Button.SetActive(true);
                Button returnButton = Button.GetComponent<Button>();
                Text returnButtonText = Button.GetComponentInChildren<Text>();
                returnButtonText.text = "Return";
                returnButton.onClick.RemoveAllListeners();
                returnButton.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene("0-intro");
                });
            }
            if (_displayButton && !_interactionOver)
            {
                Button.SetActive(true);
                Button returnButton = Button.GetComponent<Button>();
                Text returnButtonText = Button.GetComponentInChildren<Text>();
                returnButtonText.text = "Continue";
                returnButton.onClick.RemoveAllListeners();
                returnButton.onClick.AddListener(() =>
                {
                    _mainCharacterTurn = !_mainCharacterTurn;
                    _displayButton = false;
                    Button.SetActive(false);
                    Interaction();
                });
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
                _cmtCharacterSpeechBalloon.enabled = true;
                for (int i = 0; i < message.Length; i++)
                {
                    _scottSpeech.text += message[i];
                    yield return new WaitForSeconds(LetterPause);
                }
                _jimmyCounter++;
            }

            if (_professorCounter == _scottLines.Length && _jimmyCounter == _jimmyLines.Length)
            {
                _interactionOver = true;
            }

            _displayButton = true;
        }

    }
}
