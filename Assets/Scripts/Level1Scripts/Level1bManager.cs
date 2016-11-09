using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class Level1bManager : MonoBehaviour
{

    // TextTyper variables
    public float letterPause = 0.05f;
    string message;
    Text textComp;

    //Character's GameObjects
    private Image mainCharacterSpeechBalloon;
    private Text mainCharacterSpeech;
    private string mainCharacterSpeechText;

    private Image cmtCharacterSpeechBalloon;
    private Text cmtCharacterSpeech;
    private string cmtCharacterSpeechText;

    // Character Response Game Objects
    private Button answerA;
    private Button answerB;
    private Button answerC;
    private Button answerD;

    private Button continueButton;
    private Button returnButton;

    // Dialogue Variables
    private string[] mainCharacterLines = { "Hey, new kid! Wanna join us playing football? We need one more player." };
    private string[] cmtCharacterLines = { "Hey there, thanks. I would love to play with you, but I cannot play football." };

    private string[] questionChoices1 = {
        "Why? Is there something wrong?",
        "Why not? You don't look sick or anything."
    };

    private string[] answers1 = {
        "Yes, I have a disease called Charcot-Marie-Tooth. It makes it really hard for me to maintain balance and control my body like you.",
         "You shouldn't assume things just by looking at people. I'm not sick, but I actually have this disease called Charcot-Marie-Tooth. It makes it really hard for me to maintain balance and control my body like you."
    };

    private string[] questionChoices2 = {
         "Oh, I'm sorry to hear that. If you want, I can ask the guys to play something other than football so you can join us." ,
         "Oh, I'm sorry to hear that. I guess we'll have to play with an odd number of players, then. See you later!",
         "You can be the goalkeeper, then. You won't have to move too much and we get to play with an even number of players.",
         "I guess no one will want to play with you then. I'll try to find someone else."
    };

    private string[] answers2 = {
        "Thanks. I really appreciate it.",
        "Thanks. See you later then.",
        "I think I'll pass.",
        "That was not very nice from you. You seemed like a respectful guy at first."
    };

    private string[] questionChoices3 = {
        "I'm sorry I said that. That was rude. If you want, I can ask the guys to play something other than football so you can join us.",
        "I'm sorry I said that. That was rude. I guess we'll have to play with an odd number of players, then. See you later!",
        "You can be the goalkeeper, then. You won't have to move too much and we get to play with an even number of players.",
        "Who cares? No one will want to play with you, then. I'll try to find someone else."
    };

    private string[] answers3 = {
        "No problem, I don't think you meant to be rude. I'd really appreciate it if you guys played something else just so I can play too.",
         "Sure, no problem. See you later, then.",
        "No thanks. I'd rather do something else.",
        "You are really rude."
    };

    private List<string[]> questionsList;
    private List<string[]> cmtCharacterSpeechList;

    // Control Variables
    private bool mainCharacterTurn;
    private bool displayChoices = true;
    private bool displayNextButton = false;
    private bool interactionOver;
    private int mainCharacterDialogueCounter;
    private int cmtCharacterDialogueCounter;
    private int questionsIterator;

    // GUI styling
    private GUIStyle style, continueStyle;

    // Start interaction
    void Start()
    {

        // initialize control variables
        mainCharacterDialogueCounter = 0;
        cmtCharacterDialogueCounter = 0;
        mainCharacterTurn = true;
        interactionOver = false;
        questionsIterator = 0;

        // Get Character's Lines
        //  XMLParser("level1, mainCharacterLines ,cmtCharacterLines");

        // Get main character's speech objects
        mainCharacterSpeechBalloon = GameObject.Find("mainCharacterSpeech").GetComponent<Image>();
        mainCharacterSpeech = GameObject.Find("mainCharacterSpeechText").GetComponent<Text>();
        mainCharacterSpeechText = mainCharacterSpeech.text;

        // Get cmt character's speech objects
        cmtCharacterSpeechBalloon = GameObject.Find("CMTcharacterSpeech").GetComponent<Image>();
        cmtCharacterSpeech = GameObject.Find("CMTcharacterSpeechText").GetComponent<Text>();
        cmtCharacterSpeechText = mainCharacterSpeech.text;

        // Get buttons
        answerA = GameObject.Find("answerA").GetComponent<Button>();
        answerB = GameObject.Find("answerB").GetComponent<Button>();
        answerC = GameObject.Find("answerC").GetComponent<Button>();
        answerD = GameObject.Find("answerD").GetComponent<Button>();
        continueButton = GameObject.Find("continue").GetComponent<Button>();
        returnButton = GameObject.Find("return").GetComponent<Button>();

        // Hide Speech balloons
        mainCharacterSpeechBalloon.enabled = false;
        cmtCharacterSpeechBalloon.enabled = false;

        // initialize questions and answers
        // TODO: Should be done with the xmlParser
        questionsList = new List<string[]>();
        questionsList.Add(questionChoices1);
        questionsList.Add(questionChoices2);
        questionsList.Add(questionChoices3);

        cmtCharacterSpeechList = new List<string[]>();
        cmtCharacterSpeechList.Add(answers1);
        cmtCharacterSpeechList.Add(answers2);
        cmtCharacterSpeechList.Add(answers3);
        Debug.Log("speechList: " + cmtCharacterSpeechList.Count);


        // Hide response buttons
        hideButtons();

        // start actions
        continueDialogue();
    }

    // Dialogue cycle
    void continueDialogue()
    {
        if (mainCharacterTurn)
        {
            // clear everything
            cmtCharacterSpeechBalloon.enabled = false;
            clearText(cmtCharacterSpeech);
            clearText(mainCharacterSpeech);
            // Write Text
            message = mainCharacterLines[mainCharacterDialogueCounter];
            StartCoroutine(TypeText(message));
            mainCharacterDialogueCounter++;
        }
        else
        {
            // Clean Everything
            mainCharacterSpeechBalloon.enabled = false;
            clearText(cmtCharacterSpeech);
            clearText(mainCharacterSpeech);
            // Write Text
            message = cmtCharacterLines[cmtCharacterDialogueCounter];
            StartCoroutine(TypeText(message));
            cmtCharacterDialogueCounter++;
        }
        mainCharacterTurn = !mainCharacterTurn;
    }


    void askQuestions()
    {
        loadQuestions(questionsList[questionsIterator]);
    }

    bool responseControl = false;

    void getCMTresponse(int number)
    {
        Debug.Log("questionsIterator: " + questionsIterator);
        // Clean Everything
        clearText(cmtCharacterSpeech);
        // Write Text
        message = cmtCharacterSpeechList[questionsIterator][number];
        StartCoroutine(writeCMTspeech(message));
    }




    void loadQuestions(string[] arr)
    {

        answerA.GetComponentInChildren<Text>().text = arr[0];
        answerA.onClick.AddListener(() => buttonClicked(0));
        answerA.gameObject.SetActive(true);

        answerB.GetComponentInChildren<Text>().text = arr[1];
        answerB.onClick.AddListener(() => buttonClicked(1));
        answerB.gameObject.SetActive(true);

        if (arr.Length > 2)
        {
            answerC.GetComponentInChildren<Text>().text = arr[2];
            answerC.onClick.AddListener(() => buttonClicked(2));
            answerC.gameObject.SetActive(true);

        }
        if (arr.Length == 4)
        {
            answerD.GetComponentInChildren<Text>().text = arr[3];
            answerD.onClick.AddListener(() => buttonClicked(3));
            answerD.gameObject.SetActive(true);
        }
    }

    // Hide buttons
    void hideButtons()
    {
        answerA.gameObject.SetActive(false);
        answerB.gameObject.SetActive(false);
        answerC.gameObject.SetActive(false);
        answerD.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
    }

    // Clear Speech Balloon
    void clearText(Text textObject)
    {
        textObject.text = "";
    }


    void buttonClicked(int buttonNumber)
    {
        if (questionsIterator == cmtCharacterSpeechList.Count - 1)
        {

        }
        hideButtons();
        getCMTresponse(buttonNumber);
    }


    void OnGUI()
    {
        if (displayNextButton)
        {
            if (GUI.Button(new Rect(375, 225, 268, 25), "Continue"))
            {
                continueDialogue();
                displayNextButton = false;
            }
        }
        if (interactionOver)
        {
            if (GUI.Button(new Rect(375, 225, 268, 25), "Return"))
            {
                SceneManager.LoadScene("CMTandExercise");
            }

        }
    }


    // Write text to screen
    IEnumerator TypeText(string message)
    {
        if (mainCharacterTurn)
        {
            mainCharacterSpeechBalloon.enabled = true;
            for (int i = 0; i < message.Length; i++)
            {
                mainCharacterSpeech.text += message[i];
                yield return new WaitForSeconds(letterPause);
            }
        }
        else
        {
            cmtCharacterSpeechBalloon.enabled = true;
            for (int i = 0; i < message.Length; i++)
            {
                cmtCharacterSpeech.text += message[i];
                yield return new WaitForSeconds(letterPause);
            }
        }
        if (mainCharacterDialogueCounter == mainCharacterLines.Length && cmtCharacterDialogueCounter == cmtCharacterLines.Length)
        {
            askQuestions();
        }
        else
            displayNextButton = true;
    }



    IEnumerator writeCMTspeech(string message)
    {
        if (interactionOver)
        {
            return true;
        }
        cmtCharacterSpeechBalloon.enabled = true;
        clearText(cmtCharacterSpeech);
        for (int i = 0; i < message.Length; i++)
        {
            cmtCharacterSpeech.text += message[i];
            yield return new WaitForSeconds(letterPause);
        }
        if (questionsIterator < (questionsList.Count - 1))
        {
            questionsIterator++;
            askQuestions();
        }
        else
        {
            hideButtons();
            interactionOver = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
