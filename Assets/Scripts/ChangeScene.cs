using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	// Change Scene
	public void ChangeToScene (string scene) {
        SceneManager.LoadScene(scene);
	}
}
