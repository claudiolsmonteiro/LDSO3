using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SwapCharacterSprite : MonoBehaviour
    {

        protected Image CharacterImage;
        // Use this for initialization
        public void Start ()
        {
            CharacterImage = gameObject.GetComponent<Image>();

            if (PlayerPrefs.HasKey("character"))
            {
                string characterName = PlayerPrefs.GetString("character");

                if (characterName == "Jimmy")
                {
                    CharacterImage.sprite = Resources.Load("Sprites/Jimmy_front", typeof(Sprite)) as Sprite;
                }
                else
                {
                    CharacterImage.sprite = Resources.Load("Sprites/Ashley_front", typeof(Sprite)) as Sprite;
                }
            }
        }
	
        // Update is called once per frame
        public void Update () {
        }
    }
}
