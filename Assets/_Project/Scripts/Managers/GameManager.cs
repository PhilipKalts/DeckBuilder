using UnityEngine;

/* The purpose of this script is:
*/

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector]
    public CardsManager CardsManager;

    private void Awake()
    {
        ///***Singleton***///
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);

        CardsManager = GetComponent<CardsManager>();
    }
}