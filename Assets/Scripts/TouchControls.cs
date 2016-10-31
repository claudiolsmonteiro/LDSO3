using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {

    private CharacterMovementScript player;

    // Use this for initialization
    void Start ()
    {
        player = FindObjectOfType<CharacterMovementScript>();
	  }


    public void UpArrow()
    {
        player.MoveUp();
    }

    public void LeftArrow()
    {
        player.MoveLeft();
    }

    public void RightArrow()
    {
        player.MoveRight();
    }

    public void DownArrow()
    {
        player.MoveDown();
    }

    public void UnpressedArrow()
    {
        player.Stop();
        player.moving = -2;
    }

    public void Action()
    {
        player.Action();
    }


}
