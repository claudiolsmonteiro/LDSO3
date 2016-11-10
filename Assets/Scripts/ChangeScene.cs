using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Assets.Scripts.ParserXML;

public class ChangeScene : MonoBehaviour {

	// Change Scene
	public void ChangeToScene (string scene) {
        SceneManager.LoadScene(scene);
        Parser cenas = new Parser();
    }

}
