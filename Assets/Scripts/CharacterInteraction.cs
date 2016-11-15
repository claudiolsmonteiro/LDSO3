using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterInteraction : MonoBehaviour
{
    public string NextScene;
    public bool InRange;

    public void Start()
    {
        InRange = false;
    }

    public void OnTriggerEnter2D()
    {
        InRange = true;
    }

    public void OnTriggerExit2D()
    {
        InRange = false;
    }

    public void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.keyCode == KeyCode.Return && InRange)
            SceneManager.LoadScene(NextScene);
    }

}
