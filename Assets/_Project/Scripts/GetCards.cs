using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

/* The purpose of this script is:
*/

[System.Serializable]
public class Card
{
    public string Name;
    public int HP;
    public int Damage;
    public string ImageURL;
    public Texture Texture;
}


public class GetCards : MonoBehaviour
{
    [SerializeField] GameObject cardsParent;
    [SerializeField] GameObject pokemonCardPrefab;
    [SerializeField] string[] pokemonNames;


    //public List<Card> Cards;
    public List<CardButton> CardButtons;



    private void Awake()
    {
        GetCard();
    }

    void GetCard()
    {
        string url = "https://api.pokemontcg.io/v2/cards?q=name:";
        for (int i = 0; i < pokemonNames.Length; i++) StartCoroutine(GetRequest(url + pokemonNames[i]));
    }

    IEnumerator GetRequest(string url)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();
        //if (uwr.isNetworkError) Debug.Log("Error While Sending: " + uwr.error);
        //else Debug.Log("Received: " + uwr.downloadHandler.text);


        // Turn the JSON text into an array, I'll only take the first card of the array
        string jsonFile = uwr.downloadHandler.text;
        JSONNode fileInfo = JSON.Parse(jsonFile);
        var allCards = fileInfo["data"];

        // Get Sprite Texture
        string spriteUrl = allCards[0]["images"]["small"];
        UnityWebRequest uwrTexture = UnityWebRequestTexture.GetTexture(spriteUrl);
        yield return uwrTexture.SendWebRequest();


        // New Instance of the class
        //Card card = new Card()
        //{
        //    Name = allCards[0]["name"],
        //    HP = allCards[0]["hp"],
        //    ImageURL = spriteUrl,
        //    Texture = DownloadHandlerTexture.GetContent(uwrTexture)
        //};

        //Cards.Add(card);


        // Instantiate new Card
        RectTransform newCardObj = Instantiate(pokemonCardPrefab).GetComponent<RectTransform>();
        newCardObj.SetParent(cardsParent.transform);
        newCardObj.name += ": " + allCards[0]["name"];

        CardButton button = newCardObj.GetComponent<CardButton>();

        button.Card.Name = allCards[0]["name"];
        button.Card.HP = allCards[0]["hp"];
        button.Card.ImageURL = spriteUrl;
        button.Card.Texture = DownloadHandlerTexture.GetContent(uwrTexture);

        CardButtons.Add(button);
    }
}