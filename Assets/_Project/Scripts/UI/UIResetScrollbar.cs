using UnityEngine;
using UnityEngine.UI;

/* The purpose of this script is: to reset the scrollbar
 * to the right position after changing the cards
 * 
 * The value 1 means the scrollbar is all the way to the top
 * 
 * The scrollbar GameObject is actually invisible and not interactable by the player
*/

public class UIResetScrollbar : MonoBehaviour
{
    [SerializeField] Scrollbar scrollbar;


    private void OnEnable() => GameManager.Instance.CardsManager.OnFinishedInitializing += ResetBar;


    private void OnDisable() => GameManager.Instance.CardsManager.OnFinishedInitializing -= ResetBar;


    public void ResetBar() => scrollbar.value = 1;
}