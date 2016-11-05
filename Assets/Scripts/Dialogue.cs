using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Dialogue : MonoBehaviour {

    private string line1, line2;
    private GUIStyle style, style2;

    void Start ()
    {
        // Initialize Variables
        line1 = "Hi!\nDo you want to play football with us?";
        line2 = "Cool!\n We're missing another player.\nDo you want to help us find one?";
        style = new GUIStyle();
        style2 = new GUIStyle();
        // Format GUI
        formatGUI();
        formatGUI2();

    }

    // For Style 1
    void formatGUI()
    {
        style.fontSize = 14;
        style.font = (Font)Resources.Load("Fonts/Jekyll"); ;
    }

    // For Style 2
    void formatGUI2()
    {
      style2.fontSize = 14;
      style2.font = (Font)Resources.Load("Fonts/Jekyll");
      //style2.color = Color.white;
    }

    IEnumerator waitSeconds(int secs) {
      yield return new WaitForSeconds(secs);
    }


}
