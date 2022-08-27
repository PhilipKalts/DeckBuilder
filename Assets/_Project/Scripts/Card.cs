using UnityEngine;
using UnityEngine.UI;

/* The purpose of this script is: hold the data of the GameObjects' Card
*/

public class Card : MonoBehaviour
{
    public CardData CardData;

    RawImage rawImage;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        rawImage.texture = CardData.Texture;
    }
}