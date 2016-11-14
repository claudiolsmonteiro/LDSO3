using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterInteraction : MonoBehaviour
{
    public Object NextScene;
    public bool inRange = false;

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
            SceneManager.LoadScene(NextScene.name);

    }
}
