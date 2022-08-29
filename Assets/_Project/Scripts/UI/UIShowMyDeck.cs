using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* The purpose of this script is: to show the player's deck accordingly with the
 * dropdown index
*/

public class UIShowMyDeck : MonoBehaviour
{
    List<GameObject> myCardsObj = new List<GameObject>();

    [SerializeField] TMP_Dropdown dropdownDecks, dropdownSorting;

    [SerializeField] Transform deactivatedParent, activatedParent;


    UISorting uiSorting;


    private void Awake()
    {
        uiSorting = GetComponent<UISorting>();
    }



    public void DropdownChanged()
    {
        // Value 0 means show all cards
        if (dropdownDecks.value == 0) ActivateAllCards();
        // The other values represent the number of the list -1
        else ShowMyDeck(dropdownDecks.value - 1);

        if (dropdownSorting.value == 1) uiSorting.SortByHP();
    }



    public void ShowMyDeck(int deckIndex)
    {
        myCardsObj.Clear();
        List<string> cards = new List<string>();
        cards = GameManager.Instance.CardsManager.AllDecks.MyCards[deckIndex];


        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < GameManager.Instance.CardsManager.Cards.Count; j++)
            {
                if (cards[i] == GameManager.Instance.CardsManager.Cards[j].CardData.ID)
                    myCardsObj.Add(GameManager.Instance.CardsManager.Cards[j].gameObject);
            }
        }


        for (int i = 0; i < GameManager.Instance.CardsManager.Cards.Count; i++) GameManager.Instance.CardsManager.Cards[i].transform.SetParent(deactivatedParent);

        foreach (GameObject listObject in myCardsObj)
        {
            listObject.transform.SetParent(activatedParent);
        }
    }

    public void ActivateAllCards()
    {
        for (int i = 0; i < GameManager.Instance.CardsManager.Cards.Count; i++) GameManager.Instance.CardsManager.Cards[i].transform.SetParent(activatedParent);
    }
}