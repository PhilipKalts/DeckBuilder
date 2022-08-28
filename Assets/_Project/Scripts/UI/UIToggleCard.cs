using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* The purpose of this script is:
*/

public class UIToggleCard : MonoBehaviour
{
    [SerializeField] UICardDetails cardDetails;
    Toggle toggle;

    public List<string> list = new List<string>();

    private void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    public void ToggleCard(int deckIndex)
    {
        list = GameManager.Instance.CardsManager.AllDecks.MyCards[deckIndex];

        //If we want to add a card to our deck but we have reached the maximum number
        if (toggle.isOn && list.Count >= GameManager.Instance.CardsManager.MaxDeckCards)
        {
            toggle.isOn = false;
            GameManager.Instance.UIEventsManager.OnMaxDeck?.Invoke();
            return;
        }

        print(toggle.isOn);
        if (toggle.isOn) list.Add(cardDetails.CardData.ID);
        else list.Remove(cardDetails.CardData.ID);


        GameManager.Instance.CardsManager.AllDecks.MyCards[deckIndex] = list;
    }
}