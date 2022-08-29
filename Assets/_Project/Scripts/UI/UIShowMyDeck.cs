using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* The purpose of this script is:
*/

public class UIShowMyDeck : MonoBehaviour
{
    List<GameObject> myCardsObj = new List<GameObject>();

    [SerializeField] TMP_Dropdown dropdownDecks, dropdownSorting;

    [SerializeField] Transform deactivatedParent, activatedParent;


    UIButtons buttons;


    private void Awake()
    {
        buttons = GetComponent<UIButtons>();
    }


    #region Dropdown

    public void DropdownChanged()
    {
        // All Cards
        if (dropdownDecks.value == 0)
        {
            ActivateAllCards();
        }
        else ShowMyDeck(dropdownDecks.value - 1);

        if (dropdownSorting.value == 1) buttons.SortByHP();

        GameManager.Instance.UIEventsManager.OnChangedScroll?.Invoke();
    }

    #endregion


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