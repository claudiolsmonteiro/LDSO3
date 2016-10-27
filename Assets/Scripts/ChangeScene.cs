using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Change Scene
	public void ChangeToScene (string scene) {
        Application.LoadLevel(scene);
	}
}
