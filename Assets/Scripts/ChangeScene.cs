using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ChangeScene : MonoBehaviour {

        // Change Scene
        public void ChangeToScene (string scene) {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }

        public void SetCharacter(string character)
        {
            PlayerPrefs.SetString("character", character);
        }

        public void SetLanguage(string language)
        {
            string lang = "Assets/Scripts/ParserXML/"+language+".xml";
            PlayerPrefs.SetString("language", lang);
        }

    }
}
