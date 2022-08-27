using UnityEngine;
using UnityEngine.UI;

/* The purpose of this script is:
*/

public class CardButton : MonoBehaviour
{
    public Card Card;

    RawImage rawImage;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        rawImage.texture = Card.Texture;
    }
}