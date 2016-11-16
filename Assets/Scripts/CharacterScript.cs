using System;
using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour
{
    public String Character;
    protected Animator Animator;
	// Use this for initialization
	void Start () {
	    if (PlayerPrefs.HasKey("character"))
	    {
	        Character = PlayerPrefs.GetString("character");
	        Animator = GetComponent<Animator>();
            Animator.runtimeAnimatorController = Resources.Load("Animators/" + Character + "OverrideAnimator") as RuntimeAnimatorController;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
