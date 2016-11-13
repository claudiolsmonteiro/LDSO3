using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform leftBorder;
    public Transform rightBorder;
    public Transform bottomBorder;
    public Transform upperBorder;
    private Transform character;

    void Start()
    {
        character = GameObject.Find("Character").transform;
    }

    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = character.position.x;
        newPosition.x = Mathf.Clamp(newPosition.x, leftBorder.position.x, rightBorder.position.x);
        newPosition.y = character.position.y;
        newPosition.y = Mathf.Clamp(newPosition.y, bottomBorder.position.y, upperBorder.position.y);
    
        transform.position = newPosition;
    }
}
