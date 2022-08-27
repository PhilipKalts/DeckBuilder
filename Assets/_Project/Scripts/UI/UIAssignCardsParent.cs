using UnityEngine;

/* The purpose of this script is: to Assign to the CardsManager
 * in which object should the cards be parented
*/

public class UIAssignCardsParent : MonoBehaviour
{
    [SerializeField] GameObject cardsParent;

    private void Awake() => GameManager.Instance.CardsManager.CardsParent = cardsParent;
}