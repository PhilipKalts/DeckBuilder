using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* The purpose of this script is:
*/

public class UIToggleCard : MonoBehaviour
{
    [SerializeField] UICardDetails cardDetails;
    Toggle toggle;

    List<string> cardsList = new List<string>();

    private void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    public void ToggleCard(int deckIndex)
    {
        // Make it easier to read
        cardsList = GameManager.Instance.CardsManager.AllDecks.MyCards[deckIndex];

        //If we want to add a card to our deck but we have reached the maximum number
        if (toggle.isOn && cardsList.Count >= GameManager.Instance.CardsManager.MaxDeckCards)
        {
            toggle.isOn = false;
            GameManager.Instance.UIEventsManager.OnMaxDeck?.Invoke();
            return;
        }

        if (toggle.isOn) cardsList.Add(cardDetails.CardData.ID);
        else cardsList.Remove(cardDetails.CardData.ID);


        GameManager.Instance.CardsManager.AllDecks.MyCards[deckIndex] = cardsList;
        SaveSystem.Save(GameManager.Instance.CardsManager.AllDecks);
    }
}