using UnityEngine;
using DG.Tweening;

/* The purpose of this script is: to create an animation of a message
 * When the player tries to add a card to his/her deck but has reached the Max Number
 * this message should appear
*/

public class UIMaxDeckMessage : MonoBehaviour
{
    [SerializeField] RectTransform rectMessage;
    RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        GameManager.Instance.UIEventsManager.OnMaxDeck += ShowMessage;
    }

    private void OnDisable()
    {
        GameManager.Instance.UIEventsManager.OnMaxDeck -= ShowMessage;
    }

    void ShowMessage()
    {
        rectTransform.DOScale(1, 0.75f).OnComplete(EnableButtons);

        void EnableButtons()
        {
            rectMessage.DOScale(1, 0.3f);
        }
    }

    public void HideMessage()
    {
        rectMessage.DOScale(0, 0.5f);
        rectTransform.DOScale(1.3f, 0.4f).OnComplete(Hide);

        void Hide()
        {
            rectTransform.DOScale(0, 0.5f);
        }
    }
}