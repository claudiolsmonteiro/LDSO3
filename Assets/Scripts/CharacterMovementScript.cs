using UnityEngine;

public class CharacterMovementScript : MonoBehaviour {

    public float Speed = 1.5f;
    private Animator _animator;

    // Use this for initialization
    void Start ()
    {
        _animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void FixedUpdate () {
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

    }
}
