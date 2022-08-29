using UnityEngine;

/* The purpose of this script is: to hold the Card Date
*/

[System.Serializable]
public class CardData
{
    public string ID;
    public string Name;
    public int HP;
    public string ImageURL;
    public Texture Texture;

    public string AttackName;
    public int Damage;
    public string Effect;
    public string Weakness;

    public string Rarity;
    public string Type;
}