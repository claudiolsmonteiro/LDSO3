using UnityEngine;
using System.Collections;

public class RandomFact : MonoBehaviour {

   // string[] facts = ["um", "dois", "tres"];

    public string getRandomFact()
    {
        Random r = new Random();
        //Console.Write(r);

        string fact = "fact fact fact";
        return fact;
    }

    void OnGUI()
    {
        System.Console.Write("hello");
        GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");
    }

}
