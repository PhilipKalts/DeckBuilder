using System.Collections.Generic;
using UnityEngine;

/* The purpose of this script is:
*/

public class UIShowMyDeck : MonoBehaviour
{
    List<GameObject> myCardsObj = new List<GameObject>();

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