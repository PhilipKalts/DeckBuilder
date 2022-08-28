using UnityEngine;

/* The purpose of this script is:
*/

public class UIEventsManager : MonoBehaviour
{
    public delegate void UIChange();
    public UIChange OnMaxDeck;

    public delegate void UIPressCard(CardData carddata);
    public UIPressCard OnPressedCard;
}