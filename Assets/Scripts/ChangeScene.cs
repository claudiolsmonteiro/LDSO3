using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Change Scene
	public void ChangeToScene (string scene) {
        SceneManager.LoadScene(scene);
	}

}
