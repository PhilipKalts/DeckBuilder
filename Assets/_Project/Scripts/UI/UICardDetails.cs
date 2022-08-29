using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/* The purpose of this script is:
*/

public class UICardDetails : MonoBehaviour
{
    public CardData CardData { get; private set; }

    [SerializeField] GameObject basicInfo;
    [SerializeField] RawImage rawImage;
    [SerializeField] TextMeshProUGUI txtDetails;
    [SerializeField] Toggle[] toggles;

    List<string> list = new List<string>();

    CanvasGroup canvasGroup;



    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }



    private void OnEnable()
    {
        GameManager.Instance.UIEventsManager.OnPressedCard += CardPressed;
    }

    private void OnDisable()
    {
        GameManager.Instance.UIEventsManager.OnPressedCard -= CardPressed;
    }



    void CardPressed(CardData newData)
    {
        CardData = newData;
        TurnToggles();

        basicInfo.SetActive(false);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        rawImage.texture = CardData.Texture;
        txtDetails.text = $"Name: {CardData.Name} <br>HP: {CardData.HP}<br>Rarity: {CardData.Rarity}";
    }


    /// <summary>
    /// When the player chooses a Card, it may be already on a deck or not
    /// So we have to turn the toggles on and off depending on which decks
    /// exists the card
    /// </summary>
    void TurnToggles()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            
            list = GameManager.Instance.CardsManager.AllDecks.MyCards[i];

            for (int j = 0; j < list.Count; j++)
            {
                if (list[j] == CardData.ID)
                {
                    toggles[i].SetIsOnWithoutNotify(true);
                    break;
                }
                toggles[i].SetIsOnWithoutNotify(false);
            }
        }
    }


    public void ButtonBack()
    {
        basicInfo.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }


}