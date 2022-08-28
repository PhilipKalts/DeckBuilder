using System.Linq;
using TMPro;
using UnityEngine;

/* The purpose of this script is: to control the buttons which are in game
*/

public class UIButtons : MonoBehaviour
{
    #region Variables

    [SerializeField] TextMeshProUGUI txtRarity, txtType;

    UIResetScrollbar resetScrollbar;

    int rarityIndex, typeIndex;

    #endregion



    private void Start() => resetScrollbar = GetComponent<UIResetScrollbar>();



    #region Sorting by HP

    public void SortByHP()
    {
        GameManager.Instance.CardsManager.Cards = GameManager.Instance.CardsManager.Cards.OrderBy(x => x.CardData.HP).ToList<Card>();
        SortGameObjects();


        /// <summary>
        /// By setting the parent to null and then setting it again we sort
        /// the Hierarchy based on the List
        /// Because we use the Grid Layout the order of the Hierarchy is the order on the screen
        /// </summary>
        void SortGameObjects()
        {
            for (int i = 0; i < GameManager.Instance.CardsManager.Cards.Count; i++)
            {
                RectTransform rectTransform = GameManager.Instance.CardsManager.Cards[i].GetComponent<RectTransform>();

                rectTransform.SetParent(null);
                rectTransform.SetParent(GameManager.Instance.CardsManager.CardsParent.transform);
            }
            resetScrollbar.ResetBar();
        }
    }

    #endregion



    #region Sorting By Type

    public void TypeNextIndex()
    {
        NextIndex(ref typeIndex, GameManager.Instance.CardsManager.TotalTypes.Count);

        SortByType();
    }

    public void TypePreviousIndex()
    {
        PreviousIndex(ref typeIndex, GameManager.Instance.CardsManager.TotalTypes.Count);

        SortByType();
    }

    void SortByType()
    {
        // The Type is a string with 
        string correctTxt = GameManager.Instance.CardsManager.TotalTypes[typeIndex];
        correctTxt = correctTxt.Remove(0,2);
        correctTxt = correctTxt.Remove(correctTxt.Length - 2);

        txtType.text = correctTxt;

        DeactivateAllCards();

        // Activate only the cards we want with their rarity
        foreach (Card card in GameManager.Instance.CardsManager.DictType[GameManager.Instance.CardsManager.TotalTypes[typeIndex]])
            card.gameObject.SetActive(true);
    }

    public void TypeActivate()
    {
        typeIndex = 0;

        SortByType();
    }

    #endregion



    #region Sorting By Rarity

    public void RarityNextIndex()
    {
        NextIndex(ref rarityIndex, GameManager.Instance.CardsManager.TotalRarities.Count);

        SortByRarity();
    }

    public void RarityPreviousIndex()
    {
        PreviousIndex(ref rarityIndex, GameManager.Instance.CardsManager.TotalRarities.Count);

        SortByRarity();
    }

    public void RarityActivate()
    {
        rarityIndex = 0;

        SortByRarity();
    }

    void SortByRarity()
    {
        txtRarity.text = GameManager.Instance.CardsManager.TotalRarities[rarityIndex];

        DeactivateAllCards();

        // Activate only the cards we want with their rarity
        foreach (Card card in GameManager.Instance.CardsManager.DictRarity[GameManager.Instance.CardsManager.TotalRarities[rarityIndex]])
            card.gameObject.SetActive(true);
    }

    #endregion



    #region Shared

    void DeactivateAllCards()
    {
        for (int i = 0; i < GameManager.Instance.CardsManager.Cards.Count; i++) GameManager.Instance.CardsManager.Cards[i].gameObject.SetActive(false);
    }

    void NextIndex(ref int index, int count)
    {
        if (index + 1 >= count) index = 0;
        else index++;
    }

    void PreviousIndex(ref int index, int count)
    {
        if (index - 1 < 0) index = count - 1;
        else index--;
    }

    #endregion
}