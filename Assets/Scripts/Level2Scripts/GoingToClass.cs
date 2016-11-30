using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class GoingToClass : MonoBehaviour
{

    private string _directions;
    private Text _textObject;
    private float _letterPause = 0.5f;
    // Use this for initialization
    void Start()
    {
        _directions =  "It's almost time for your first class of the day. You should go to your classroom";
        _textObject = GameObject.Find("DirectionsText").GetComponent<Text>();
        StartCoroutine(TypeDirections(_directions, _textObject));
    }

    // Write text to screen
    IEnumerator TypeDirections(string message, Text TextComp)
    {
        for (int i = 0; i < message.Length; i++)
        {
            TextComp.text += message[i];
            yield return new WaitForSeconds(_letterPause);
        }
    }
    

}
