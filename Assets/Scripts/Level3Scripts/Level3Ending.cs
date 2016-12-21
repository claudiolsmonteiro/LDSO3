using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Level3Scripts
{
    public class Level3Ending : MonoBehaviour
    {
        public void OnTriggerEnter2D()
        {
            Time.timeScale = 0;
            Debug.Log("ouchie");
            SceneManager.LoadScene("Finished");
        }


    }
}
