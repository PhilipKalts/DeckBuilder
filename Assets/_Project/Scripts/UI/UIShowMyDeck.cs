using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* The purpose of this script is:
*/

public class UIShowMyDeck : MonoBehaviour
{
    List<GameObject> myCardsObj = new List<GameObject>();

    [SerializeField] TMP_Dropdown dropdown;


    #region Dropdown

    public void DropdownChanged()
    {
        // All Cards
        if (dropdown.value == 0)
        {
            ActivateAllCards();
        }
        else ShowMyDeck(dropdown.value - 1);
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

        //for (int i = 0; i < cards.Count; i++)
        //{
        //    for (int j = 0; j < GameManager.Instance.CardsManager.Cards.Count; j++)
        //    {
        //        if (cards[i] == GameManager.Instance.CardsManager.Cards[j].CardData.ID)
        //        {
        //            myCardsObj.Add(GameManager.Instance.CardsManager.Cards[j].gameObject);
        //        }
        //    }
        //}

        for (int i = 0; i < GameManager.Instance.CardsManager.Cards.Count; i++) GameManager.Instance.CardsManager.Cards[i].gameObject.SetActive(false);

        foreach (GameObject listObject in myCardsObj)
        {
            listObject.SetActive(true);
        }
    }

    public void ActivateAllCards()
    {
        for (int i = 0; i < GameManager.Instance.CardsManager.Cards.Count; i++) GameManager.Instance.CardsManager.Cards[i].gameObject.SetActive(true);
    }
}