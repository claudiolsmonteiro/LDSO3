using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform LeftBorder;
        public Transform RightBorder;
        public Transform BottomBorder;
        public Transform UpperBorder;
        private Transform _character;

        public void Start()
        {
            _character = GameObject.Find("Character").transform;
        }

        public void Update()
        {
            Vector3 newPosition = transform.position;
            newPosition.x = _character.position.x;
            newPosition.x = Mathf.Clamp(newPosition.x, LeftBorder.position.x, RightBorder.position.x);
            newPosition.y = _character.position.y;
            newPosition.y = Mathf.Clamp(newPosition.y, BottomBorder.position.y, UpperBorder.position.y);
    
            transform.position = newPosition;
        }
    }
}
