using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Accelerometer : MonoBehaviour {
    private Text info;
    public int counter;
    public float Speed = 0.2f;
    private Animator _animator;
    public int Moving = -2;

    // Use this for initialization
    void Start () { 
        _animator = GetComponent<Animator>();
        Time.timeScale = 1;
        counter = 0;
    }
	
	// Update is called once per frame
	void Update () {
       
        float input_x = Input.acceleration.x;
        float input_y = Input.acceleration.y;
        float input_z = Input.acceleration.z;
        _animator.SetFloat("Speed", 1.0f);
        //   MoveRight();
        transform.Translate(input_x * 1 * Speed * Time.deltaTime, Speed * Time.deltaTime, 0);

    }

    public void MoveRight()
    {
        _animator.SetFloat("Speed", 1.0f);
        _animator.SetInteger("Direction", 1);
        transform.position += Vector3.up * Speed * Time.deltaTime;
        Moving = 0;
    }
}
