using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {

    private CharacterMovementScript player;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<CharacterMovementScript>();

	}

    public void UpArrow()
    {
        player.Move(2);
    }

    public void LeftArrow()
    {
        player.Move(1);
    }

    public void RightArrow()
    {
        player.Move(0);
    }

    public void DownArrow()
    {
        player.Move(3);
    }

    public void UnpressedArrow()
    {
        player.Stop();
    }

    public void Action()
    {
        player.Action();
    }


}
