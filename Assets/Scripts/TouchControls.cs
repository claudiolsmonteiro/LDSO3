using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {

    private CharacterMovementScript _player;

    // Use this for initialization
    void Start ()
    {
        _player = FindObjectOfType<CharacterMovementScript>();
	}

    public void UpArrow()
    {
        _player.MoveUp();
    }

    public void LeftArrow()
    {
        _player.MoveLeft();
    }

    public void RightArrow()
    {
        _player.MoveRight();
    }

    public void DownArrow()
    {
        _player.MoveDown();
    }

    public void UnpressedArrow()
    {
        _player.Stop();
        _player.moving = -2;
    }

    public void Action()
    {
        _player.Action();
    }


}
