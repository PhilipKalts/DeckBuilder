using SimpleJSON;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Xml;


/* The purpose of this script is: to Instantiate all the cards we want the player to have access to
 * This script is attached to the GameManager GO so it can be called from anywhere at any time
*/

/// <summary>
/// This class has a 2D list. The first index represents the deck and the second the id of the cards
/// This class is meant to be saved
/// </summary>
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


    //*****Hidden*****//
    [HideInInspector]
    public GameObject CardsParent;


    //*****Public*****//
    public List<Card> Cards = new List<Card>();

    public List<string> TotalRarities = new List<string>();
    public Dictionary<string, List<Card>> DictRarity = new Dictionary<string, List<Card>>();
    
    public List<string> TotalTypes = new List<string>();
    public Dictionary<string, List<Card>> DictType = new Dictionary<string, List<Card>>();

    public int MaxDeckCards;

    public AllDecks AllDecks = new AllDecks();


    //*****Serialized*****//
    [SerializeField] GameObject pokemonCardPrefab;
    [SerializeField] string[] pokemonNames;


    //*****Private*****//
    int cardsCreated;

    #endregion

    

    private void Start()
    {
        // Insert lists to the 2d List
        for (int i = 0; i < 3; i++)
        {
            List<string> s = new List<string>();
            AllDecks.MyCards.Add(s);
        }
        CreateCards();
    }

    void CreateCards()
    {
        string url = "https://api.pokemontcg.io/v2/cards?q=name:";
        for (int i = 0; i < pokemonNames.Length; i++) StartCoroutine(GetRequest(url + pokemonNames[i]));
        
        
        IEnumerator GetRequest(string url)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(url);
            yield return uwr.SendWebRequest();


            // Turn the JSON text into an array, I'll only take the first pokemon data of the array
            string jsonFile = uwr.downloadHandler.text;
            JSONNode fileInfo = JSON.Parse(jsonFile);
            var allCards = fileInfo["data"];


            // Get Sprite Texture
            string spriteUrl = allCards[0]["images"]["small"];
            UnityWebRequest uwrTexture = UnityWebRequestTexture.GetTexture(spriteUrl);
            yield return uwrTexture.SendWebRequest();


            // Instantiate new Card GameObject
            RectTransform newCardObj = Instantiate(pokemonCardPrefab).GetComponent<RectTransform>();
            newCardObj.SetParent(CardsParent.transform);
            newCardObj.name += ": " + allCards[0]["name"];


            // Assign the values
            Card card = newCardObj.GetComponent<Card>();
            card.CardData.ImageURL = spriteUrl;
            card.CardData.ID = allCards[0]["id"];
            card.CardData.HP = allCards[0]["hp"];
            card.CardData.Name = allCards[0]["name"];
            card.CardData.Rarity = allCards[0]["rarity"];
            card.CardData.Type = allCards[0]["types"].ToString();
            card.CardData.Texture = DownloadHandlerTexture.GetContent(uwrTexture);



            Cards.Add(card);
            ChecKDictionary(ref DictRarity, ref TotalRarities, card, card.CardData.Rarity);
            ChecKDictionary(ref DictType, ref TotalTypes, card, card.CardData.Type);


            // If all of the cards are created Invoke the event
            cardsCreated++;
            if (cardsCreated == pokemonNames.Length) OnFinishedInitializing?.Invoke();
        }
    }

    void ChecKDictionary(ref Dictionary<string, List<Card>> dictionary, ref List<string> list, Card card, string cardData)
    {
        if (!dictionary.ContainsKey(cardData))
        {
            list.Add(cardData);
            dictionary[cardData] = new List<Card>();
        }

        dictionary[cardData].Add(card);
    }
}