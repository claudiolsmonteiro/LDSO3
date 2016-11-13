using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class RandomFact : MonoBehaviour {

    private string[] _facts = { "um", "CMT is very dangerous", "tres" };
    private string _fact;
    private System.Random _rand;
    private GameObject _textObject;
    private Text _text;

    void Start ()
    {
        // Initialize Variables
        _rand = new System.Random();
        _textObject = GameObject.Find("Canvas/Content/Random Fact");
        _text = _textObject.GetComponent<Text>();

        // Generate Random Number
        int r = _rand.Next(0, _facts.Length);
        _fact = r + ": " + _facts[r];

        // Set random fact text
        _text.text = _fact;
    }

}
