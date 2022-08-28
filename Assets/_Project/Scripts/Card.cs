using UnityEngine;
using UnityEngine.UI;

/* The purpose of this script is: hold the data of the GameObjects' Card
*/

public class Card : MonoBehaviour
{
    public CardData CardData;

    RawImage rawImage;
    Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        rawImage = GetComponent<RawImage>();
        rawImage.texture = CardData.Texture;
    }

    public void ToggleCard()
    {
        // If we want to add a card to our deck but we have reached the maximum number
        if (toggle.isOn && GameManager.Instance.CardsManager.MyCards.Count >= GameManager.Instance.CardsManager.MaxDeckCards)
        {
            toggle.isOn = false;
            GameManager.Instance.UIEventsManager.OnMaxDeck?.Invoke();
            return;
        }

        if (toggle.isOn) GameManager.Instance.CardsManager.MyCards.Add(CardData.ID);
        else GameManager.Instance.CardsManager.MyCards.Remove(CardData.ID);
    }
}