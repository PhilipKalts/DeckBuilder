using SimpleJSON;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;


/* The purpose of this script is: to Instantiate all the cards we want the player to have access to
 * This script is attached to the GameManager GO so it can be called from anywhere at any time
*/


/// <summary>
/// This class has a 2D list. The first index represents the deck and the second the id of the cards
/// This class is meant to be saved
/// </summary>
[System.Serializable]
public class AllDecks
{
    public List<List<string>> MyCards = new List<List<string>>();
}


public class CardsManager : MonoBehaviour
{
    #region Variables

    //*****Events*****//
    public delegate void CardChanges();
    public CardChanges OnFinishedInitializing;



    //*****Public*****//
    [HideInInspector]
    public AllDecks AllDecks = new AllDecks();

    [HideInInspector]
    public GameObject CardsParent;
    [HideInInspector]
    public List<Card> Cards = new List<Card>();

    [HideInInspector]
    public List<string> TotalRarities = new List<string>();
    public Dictionary<string, List<Card>> DictRarity = new Dictionary<string, List<Card>>();
    
    [HideInInspector]
    public List<string> TotalTypes = new List<string>();
    public Dictionary<string, List<Card>> DictType = new Dictionary<string, List<Card>>();

    [Tooltip("The Maximum number of cards each deck can take")]
    public int MaxDeckCards;



    //*****Serialized*****//
    [SerializeField] GameObject pokemonCardPrefab;
    [SerializeField, Tooltip("The total number of cards which will be loaded")] 
    int numberOfCards;



    //*****Private*****//
    int cardsCreated;

    #endregion

    

    private void Start()
    {
        AllDecks = SaveSystem.Load();
        CreateCards();
    }


    void CreateCards()
    {
        StartCoroutine(GetRequest("https://api.pokemontcg.io/v2/cards?q=hp%3A%5B150%20TO%20%2A%5D"));
        
        IEnumerator GetRequest(string url)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(url);
            yield return uwr.SendWebRequest();


            // Turn the JSON text into an array, I'll only take the first pokemon data of the array
            string jsonFile = uwr.downloadHandler.text;
            JSONNode fileInfo = JSON.Parse(jsonFile);
            var allCards = fileInfo["data"];

            for (int i = 0; i < numberOfCards; i++)
            {
                // Get Sprite Texture
                string spriteUrl = allCards[i]["images"]["small"];
                UnityWebRequest uwrTexture = UnityWebRequestTexture.GetTexture(spriteUrl);
                yield return uwrTexture.SendWebRequest();


                // Instantiate new Card GameObject
                RectTransform newCardObj = Instantiate(pokemonCardPrefab).GetComponent<RectTransform>();
                newCardObj.SetParent(CardsParent.transform, false);
                newCardObj.name += ": " + allCards[i]["name"];


                // Assign the values
                Card card = newCardObj.GetComponent<Card>();
                card.CardData.ImageURL = spriteUrl;
                card.CardData.ID = allCards[i]["id"];
                card.CardData.HP = allCards[i]["hp"];
                card.CardData.Name = allCards[i]["name"];
                card.CardData.Rarity = allCards[i]["rarity"];
                card.CardData.Type = allCards[i]["types"].ToString();
                card.CardData.Texture = DownloadHandlerTexture.GetContent(uwrTexture);

                card.CardData.AttackName = allCards[i]["attacks"][0]["name"];
                card.CardData.Damage = allCards[i]["attacks"][0]["damage"];
                card.CardData.Effect = allCards[i]["attacks"][0]["text"];
                card.CardData.Weakness = allCards[i]["weaknesses"][0]["type"];



                Cards.Add(card);
                ChecKDictionary(ref DictRarity, ref TotalRarities,ref  card, card.CardData.Rarity, true);
                ChecKDictionary(ref DictType, ref TotalTypes, ref card, card.CardData.Type, false);


                // If all of the cards are created Invoke the event
                cardsCreated++;
                if (cardsCreated >= numberOfCards) OnFinishedInitializing?.Invoke();
            }
        }
    }



    void ChecKDictionary(ref Dictionary<string, List<Card>> dictionary, ref List<string> list, ref Card card, string cardData, bool isRarity)
    {
        if (cardData == null)
        {
            if (isRarity) card.CardData.Rarity = "Unknown";
            else card.CardData.Type = "[\"Uknown\"]";
            
            cardData = "Unknown";
        }

        if (!dictionary.ContainsKey(cardData))
        {
            list.Add(cardData);
            dictionary[cardData] = new List<Card>();
        }

        dictionary[cardData].Add(card);
    }
}