using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ChangeScene : MonoBehaviour {

        // Change Scene
        public void ChangeToScene (string scene) {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
            //Parser cenas = new Parser();
        }

        public void SetCharacter(string character)
        {
            PlayerPrefs.SetString("character", character);
        }

    }
}
