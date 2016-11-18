using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovementScript : MonoBehaviour {

    public float Speed = 1.5f;
    private Animator _animator;
    public int moving = -2;

    // Use this for initialization
    public void Start ()
    {
        _animator = GetComponent<Animator>();
    }



	// Update is called once per frame
	public void FixedUpdate () {

  #if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetKey(KeyCode.D) )
        {
            Move(0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Move(1);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            Move(2);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Move(3);
        }
        else
        {
            Stop();
        }

      #else

        if (moving == 0){
          MoveRight();
        }
        if (moving == 1){
          MoveLeft();
        }
        if (moving == 2){
          MoveUp();
        }
        if (moving == 3){
          MoveDown();
        }
        if (moving == -2) {
          Stop();
        }

      #endif
    }

    public void MoveDown(){
      _animator.SetFloat("Speed", 1.0f);
      _animator.SetInteger("Direction", 3);
      transform.position += Vector3.down * Speed * Time.deltaTime;
      moving = 3;
    }

    public void MoveUp() {
      _animator.SetFloat("Speed", 1.0f);
      _animator.SetInteger("Direction", 2);
      transform.position += Vector3.up * Speed * Time.deltaTime;
      moving = 2;
    }

    public void MoveLeft() {
      _animator.SetFloat("Speed", 1.0f);
      _animator.SetInteger("Direction", 1);
      transform.position += Vector3.left * Speed * Time.deltaTime;
      moving = 1;
    }

    public void MoveRight() {
      _animator.SetFloat("Speed", 1.0f);
      _animator.SetInteger("Direction", 0);
      transform.position += Vector3.right * Speed * Time.deltaTime;
      moving = 0;
    }

    public void Move(int direction)
    {
        _animator.SetFloat("Speed", 1.0f);
        _animator.SetInteger("Direction", direction);
        if (direction == 0)
        {
          transform.position += Vector3.right * Speed * Time.deltaTime;
        }
        else if (direction == 1)
        {
          transform.position += Vector3.left * Speed * Time.deltaTime;
        }
        else if (direction == 2)
        {
          transform.position += Vector3.up * Speed * Time.deltaTime;
        }
        else if (direction == 3)
        {
          transform.position += Vector3.down * Speed * Time.deltaTime;
        }
    }

    public void Stop()
    {
        _animator.SetFloat("Speed", 0.0f);
    }

    public void Action()
    {
        var interactiveCharacters = GameObject.Find("InteractiveCharacters");

        List<InteractionNotificationScript> interactionNotification = new List<InteractionNotificationScript>();
        List<GameObject> interactiveCharactersList = new List<GameObject>();

        foreach (Transform character in interactiveCharacters.transform)
        {
            interactionNotification.Add(character.GetComponent(typeof(InteractionNotificationScript)) as InteractionNotificationScript);
            interactiveCharactersList.Add(character.gameObject);
        }

        for (int i = 0; i < interactiveCharactersList.Count; i++)
        {
            Debug.Log(interactiveCharactersList[i]);
            CharacterInteraction characterInteractionScript = interactiveCharactersList[i].GetComponentInChildren(typeof(CharacterInteraction)) as CharacterInteraction;
            Debug.Log(interactionNotification[i].Interactable);

            if (characterInteractionScript != null && characterInteractionScript.InRange && interactionNotification[i].Interactable)
            {
                SceneManager.LoadScene(characterInteractionScript.NextScene);
            }
        }
    }
}
