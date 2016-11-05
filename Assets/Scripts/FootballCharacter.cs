using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FootballCharacter : MonoBehaviour {

	void OnTriggerEnter2D()
	{
			SceneManager.LoadScene("Dialogue");
	}

}
