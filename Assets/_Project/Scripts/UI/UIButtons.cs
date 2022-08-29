using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/* The purpose of this script is: to control the buttons which are in game
*/

public class UIButtons : MonoBehaviour
{
    #region Variables

    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] TextMeshProUGUI txtRarity, txtType;



    int rarityIndex, typeIndex;
    GameObject panelRarity, panelType; 

    UIResetScrollbar resetScrollbar;

    #endregion



    #region Start

    private void Start()
    {
        resetScrollbar = GetComponent<UIResetScrollbar>();
        panelRarity = txtRarity.gameObject.transform.parent.gameObject;
        panelType = txtType.gameObject.transform.parent.gameObject;
    }

    #endregion



    #region Dropdown

    public void ChangeDropDown()
    {
        // No sorting
        if (dropdown.value == 0)
        {
            ActivatePanel(null);
            ChangeActivationCards(true);
        }
        // Sort by HP
        else if (dropdown.value == 1)
        {
            ActivatePanel(null);
            SortByHP();
        }
        // Sort by Rarity
        else if (dropdown.value == 2)
        {
            ActivatePanel(panelRarity);
            FindValidRarity(ref GameManager.Instance.CardsManager.DictRarity, true);
            SortByRarity();
        }
        // Sort by Type
        else if (dropdown.value == 3)
        {
            ActivatePanel(panelType);
            FindValidType(ref GameManager.Instance.CardsManager.DictType, true);
            SortByType();
        }

        GameManager.Instance.UIEventsManager.OnChangedScroll?.Invoke();
    }


    void ActivatePanel(GameObject activatePanel)
    {
        panelType.gameObject.SetActive(false);
        panelRarity.gameObject.SetActive(false);

        if (activatePanel != null) activatePanel.gameObject.SetActive(true);
    }

    #endregion



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
                // If the parent is not already on the CardsParent it means the card is not on this deck and we skip it
                if (GameManager.Instance.CardsManager.Cards[i].transform.parent == GameManager.Instance.CardsManager.CardsParent.transform)
                {
                    Transform cardTransform = GameManager.Instance.CardsManager.Cards[i].transform;
                    cardTransform.SetParent(null);
                    cardTransform.SetParent(GameManager.Instance.CardsManager.CardsParent.transform);
                }
            }
        }
    }

    #endregion



    #region Sorting By Type

    public void TypeNextIndex()
    {
        NextIndex(ref typeIndex, GameManager.Instance.CardsManager.TotalTypes.Count);

        FindValidType(ref GameManager.Instance.CardsManager.DictType, true);

        SortByType();
    }

    public void TypePreviousIndex()
    {
        PreviousIndex(ref typeIndex, GameManager.Instance.CardsManager.TotalTypes.Count);

        FindValidType(ref GameManager.Instance.CardsManager.DictType, false);

        SortByType();
    }

    void FindValidType(ref Dictionary<string, List<Card>> dictionary, bool isNext)
    {
        int totalTimes = 0;
        bool haveFound = false;

        while (!haveFound && totalTimes < dictionary.Count)
        {
            foreach (Card card in dictionary[GameManager.Instance.CardsManager.TotalTypes[typeIndex]])
                if (card.transform.parent.name == GameManager.Instance.CardsManager.CardsParent.name) haveFound = true;

            if (!haveFound)
            {
                if (isNext)
                    NextIndex(ref typeIndex, GameManager.Instance.CardsManager.TotalTypes.Count);
                else 
                    PreviousIndex(ref typeIndex, GameManager.Instance.CardsManager.TotalTypes.Count);
            }

            totalTimes++;
        }
    }

    void SortByType()
    {
        // The Type is a string as a ["type"]. We want to remove the [" at the start and the "] in the end
        string correctTxt = GameManager.Instance.CardsManager.TotalTypes[typeIndex];
        correctTxt = correctTxt.Remove(0,2);
        correctTxt = correctTxt.Remove(correctTxt.Length - 2);

        txtType.text = correctTxt;

        ChangeActivationCards(false);

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
        
        FindValidRarity(ref GameManager.Instance.CardsManager.DictRarity, true);
        
        SortByRarity();
    }

    public void RarityPreviousIndex()
    {
        PreviousIndex(ref rarityIndex, GameManager.Instance.CardsManager.TotalRarities.Count);

        FindValidRarity(ref GameManager.Instance.CardsManager.DictRarity, false);
        
        SortByRarity();
    }

    public void RarityActivate()
    {
        rarityIndex = 0;

        SortByRarity();
    }

    void FindValidRarity(ref Dictionary<string, List<Card>> dictionary, bool isNext)
    {
        int totalTimes = 0;
        bool haveFound = false;

        while (!haveFound && totalTimes < dictionary.Count)
        {
            foreach (Card card in dictionary[GameManager.Instance.CardsManager.TotalRarities[rarityIndex]])
                if (card.transform.parent.name == GameManager.Instance.CardsManager.CardsParent.name) haveFound = true;

            if (!haveFound)
            {
                if (isNext)
                    NextIndex(ref rarityIndex, GameManager.Instance.CardsManager.TotalRarities.Count);
                else 
                    PreviousIndex(ref rarityIndex, GameManager.Instance.CardsManager.TotalRarities.Count);
            }

            totalTimes++;
        }
    }

    void SortByRarity()
    {
        txtRarity.text = GameManager.Instance.CardsManager.TotalRarities[rarityIndex];

        ChangeActivationCards(false);


        // Activate only the cards we want with their rarity
        foreach (Card card in GameManager.Instance.CardsManager.DictRarity[GameManager.Instance.CardsManager.TotalRarities[rarityIndex]])
            card.gameObject.SetActive(true);
    }

    #endregion



    #region Shared

    void ChangeActivationCards(bool isActive)
    {
        for (int i = 0; i < GameManager.Instance.CardsManager.Cards.Count; i++) 
            GameManager.Instance.CardsManager.Cards[i].gameObject.SetActive(isActive);
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