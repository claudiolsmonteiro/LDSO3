using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.ParserXML;
using System.Collections.Generic;

namespace Assets.Scripts.Level3Scripts
{
    public class ProfessorInteractionScript : MonoBehaviour
    {

        //Character's GameObjects
        private Image _mainCharacterSpeechBalloon;
        private Text _mainCharacterSpeech;
        private string _mainCharacterSpeechText;
        public GameObject Button;
        public float LetterPause = 0.05f;
        private bool _interactionOver;

        private Image _cmtCharacterSpeechBalloon;
        private Text _scottSpeech;
        private string _cmtCharacterSpeechText;

        private string[] professorLines;

        private string[] jimmyLines;


        private int _jimmyCounter;
        private int _professorCounter;
        private bool _mainCharacterTurn;
        private bool _displayButton;

        // Use this for initialization
        [UsedImplicitly]
        private void Start()
        {

            Assets.Scripts.ParserXML.Parser parser = new Assets.Scripts.ParserXML.Parser();
            List<NPC> npcs = parser.npcs;

            professorLines = new string[2];
            jimmyLines = new string[2];

            professorLines[0] = npcs[4].dialogues[2].Options[0];
            professorLines[1] = npcs[4].dialogues[2].Options[1];

            jimmyLines[0] = npcs[4].dialogues[3].Options[0];
            jimmyLines[1] = npcs[4].dialogues[3].Options[1];

            // Get main character's speech objects
            _mainCharacterSpeechBalloon = GameObject.Find("mainCharacterSpeech").GetComponent<Image>();
            _mainCharacterSpeech = GameObject.Find("mainCharacterSpeechText").GetComponent<Text>();
            _mainCharacterSpeechText = _mainCharacterSpeech.text;

            // Get cmt character's speech objects
            _cmtCharacterSpeechBalloon = GameObject.Find("CMTcharacterSpeech").GetComponent<Image>();
            _scottSpeech = GameObject.Find("CMTcharacterSpeechText").GetComponent<Text>();
            _cmtCharacterSpeechText = _mainCharacterSpeech.text;
            _mainCharacterTurn = false;
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
                line = jimmyLines[_jimmyCounter];
            }
            else
            {
                line = professorLines[_professorCounter];

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
                    SceneManager.LoadScene("4-FetchingScott");
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
                _jimmyCounter++;
            }
            else if (!_interactionOver)
            {
                _cmtCharacterSpeechBalloon.enabled = true;
                for (int i = 0; i < message.Length; i++)
                {
                    _scottSpeech.text += message[i];
                    yield return new WaitForSeconds(LetterPause);
                }
                _professorCounter++;
            }

            if (_professorCounter == professorLines.Length && _jimmyCounter == jimmyLines.Length)
            {
                _interactionOver = true;
            }
            _displayButton = true;
        }

    }
}
