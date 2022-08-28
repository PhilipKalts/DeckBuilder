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
        // Deck 1
        else if (dropdown.value == 1)
        {

        }
        // Deck 2
        else if (dropdown.value == 2)
        {

        }
        // Deck 3
        else if (dropdown.value == 3)
        {

        }
    }

    #endregion


    public void ShowMyDeck()
    {
        myCardsObj.Clear();
        for (int i = 0; i < GameManager.Instance.CardsManager.MyCards.Count; i++)
        {
            for (int j = 0; j < GameManager.Instance.CardsManager.Cards.Count; j++)
            {
                if (GameManager.Instance.CardsManager.MyCards[i] == GameManager.Instance.CardsManager.Cards[j].CardData.ID)
                {
                    myCardsObj.Add(GameManager.Instance.CardsManager.Cards[j].gameObject);
                }
            }
        }

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