using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class RandomFact : MonoBehaviour {

    private string[] facts = { "um", "CMT is very dangerous", "tres" };
    private string fact;
    private System.Random rand;
    private GUIStyle style;

    void Start ()
    {
        // Initialize Variables
        rand = new System.Random();
        style = new GUIStyle();

        // Generate Random Number
        int r = rand.Next(0, facts.Length);
        fact = r + ": " + facts[r];

        // Format GUI
        formatGUI();
    
    }

    void formatGUI()
    {
        style.fontSize = 14;
        style.font = (Font)Resources.Load("Fonts/Jekyll"); ;
    }


    void OnGUI()
    {
                
        GUI.Box(new Rect(395, 180, 268, 75), fact, style);
    }

}
