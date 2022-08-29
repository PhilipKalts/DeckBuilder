using UnityEngine;

/* The purpose of this script is: to create the events when certain actions are performered
 * via the UI
 * We use delegates cause we can invoke the events outside of this class
*/

public class UIEventsManager : MonoBehaviour
{
    public delegate void UIChange();
    public UIChange OnMaxDeck, OnChangedScroll;

    public delegate void UIPressCard(CardData carddata);
    public UIPressCard OnPressedCard;
}