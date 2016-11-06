using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CMTcharacter : MonoBehaviour {


	void OnTriggerEnter2D()
	{
			SceneManager.LoadScene("CMTcharacterInteraction");
	}


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
