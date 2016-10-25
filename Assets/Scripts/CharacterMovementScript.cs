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
        if (Input.GetKey(KeyCode.D))
        {
            _animator.SetFloat("Speed", 1.0f);
            _animator.SetInteger("Direction", 0);
            transform.position += Vector3.right * Speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _animator.SetFloat("Speed", 1.0f);
            _animator.SetInteger("Direction", 1);
            transform.position += Vector3.left * Speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            _animator.SetFloat("Speed", 1.0f);
            _animator.SetInteger("Direction", 2);
            transform.position += Vector3.up * Speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _animator.SetFloat("Speed", 1.0f);
            _animator.SetInteger("Direction", 3);
            transform.position += Vector3.down*Speed*Time.deltaTime;
        }
        else
        {
            _animator.SetFloat("Speed", 0.0f);
        }
    }
}
