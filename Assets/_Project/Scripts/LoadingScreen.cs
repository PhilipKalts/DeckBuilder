using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

/* The purpose of this script is:
*/

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtTips;
    [SerializeField, Tooltip("Give the player something to read while waiting for the " +
        "cards to initialized")]
    string[] tips;

    [SerializeField] RectTransform loadingImage;



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

        sequence.Append(loadingImage.DORotate(new Vector3(0,0, -50), 0.5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear));
        sequence.Append(loadingImage.DORotate(new Vector3(0,0, 360), 0.75f, RotateMode.FastBeyond360).SetRelative(true));

        sequence.Append(loadingImage.DORotate(new Vector3(0,0, 50), 0.5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear));
        sequence.Append(loadingImage.DORotate(new Vector3(0,0, -360), 0.75f, RotateMode.FastBeyond360).SetRelative(true));

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