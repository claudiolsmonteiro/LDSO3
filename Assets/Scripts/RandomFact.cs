using Assets.Scripts.ParserXML;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class RandomFact : MonoBehaviour {

        private string[] _facts;
        private string _fact;
        private System.Random _rand;
        private GameObject _textObject;
        private Text _text;

        public void Start ()
        {

            Assets.Scripts.ParserXML.Parser cenas = new Assets.Scripts.ParserXML.Parser();
            List<NPC> npcs = cenas.npcs;

            _facts = new string[4];
           

            _facts[0] = npcs[5].dialogues[0].Options[0];
            _facts[1] = npcs[5].dialogues[0].Options[1];
            _facts[2] = npcs[5].dialogues[0].Options[2];
            _facts[3] = npcs[5].dialogues[0].Options[3];
            // Initialize Variables
            _rand = new System.Random();
            _textObject = GameObject.Find("Canvas/Content/Random Fact");
            _text = _textObject.GetComponent<Text>();

            // Generate Random Number
            int r = _rand.Next(0, _facts.Length);
            _fact = _facts[r];

            // Set random fact text
            _text.text = _fact;
        }

    }
}
