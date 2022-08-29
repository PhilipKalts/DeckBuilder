using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

/* The purpose of this script is: to create a loading screen
 * while the cards are being loaded
 * 
 * At the same time we want to give the player an interesting loading screen
 * That's why we'll show tips and a card animation
*/

public class LoadingScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] TextMeshProUGUI txtTips;
    [SerializeField, Tooltip("Give the player something to read while waiting for the " +
        "cards to initialized")]
    string[] tips;

    [SerializeField] RectTransform loadingImage;

    #endregion



    #region Start

    private void Start()
    {
        StartCoroutine(ShowTips());

        AnimateLoadingCard();
    }

    WaitForSeconds waitTip = new WaitForSeconds(2);
    IEnumerator ShowTips()
    {
        int index = 0;

        if (tips.Length == 0) yield break;

        while (gameObject.activeSelf)
        {
            txtTips.text = "Tip: " + tips[index];
            yield return waitTip;

            if (index + 1 < tips.Length) index++;
            else index = 0;
        }
    }

    Sequence sequence;
    void AnimateLoadingCard()
    {
        sequence = DOTween.Sequence();

        sequence.Append(loadingImage.DORotate(new Vector3(0,0, -50), 0.3f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear));
        sequence.Append(loadingImage.DORotate(new Vector3(0,0, 360), 0.5f, RotateMode.FastBeyond360).SetRelative(true));

        sequence.Append(loadingImage.DORotate(new Vector3(0,0, 50), 0.3f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear));
        sequence.Append(loadingImage.DORotate(new Vector3(0,0, -360), 0.5f, RotateMode.FastBeyond360).SetRelative(true));

        sequence.SetLoops(-1);
    }

    #endregion



    #region OnEnable/Disable

    private void OnEnable()
    {
        GameManager.Instance.CardsManager.OnFinishedInitializing += DisableLoadingScreen;
    }

    private void OnDisable()
    {
        GameManager.Instance.CardsManager.OnFinishedInitializing -= DisableLoadingScreen;
    }

    void DisableLoadingScreen()
    {
        sequence.Kill();
        gameObject.SetActive(false);
    }

    #endregion
}