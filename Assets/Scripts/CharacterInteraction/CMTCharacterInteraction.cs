using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CMTCharacterInteraction : MonoBehaviour {

    bool inRange = false;

    void OnTriggerEnter2D()
    {
        inRange = true;
    }

    void OnTriggerExit2D()
    {
        inRange = false;
    }


    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.keyCode == KeyCode.Return && inRange)
            SceneManager.LoadScene("Level1b-CMTcharacterInteraction");

    }
}
