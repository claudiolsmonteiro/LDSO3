using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level3CollisionScript : MonoBehaviour
{
    public void OnTriggerEnter2D()
    {
        Time.timeScale = 0;
        Debug.Log("ouchie");
        SceneManager.LoadScene("GameOver");


    }


}
