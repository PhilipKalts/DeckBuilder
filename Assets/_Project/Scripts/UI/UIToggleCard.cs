using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


/* The purpose of this script is: to add/remove the card from the
 * according deck when the toggle is pressed
*/


public class UIToggleCard : MonoBehaviour
{
    [SerializeField] UICardDetails cardDetails;
    Toggle toggle;

    // This list is to make things easier to read list from the GameManager.Instance.CardsManager.AllDecks
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