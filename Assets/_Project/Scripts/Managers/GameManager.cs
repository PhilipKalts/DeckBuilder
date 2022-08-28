using UnityEngine;

/* The purpose of this script is: to create a Master Singleton
 * the instance of this class is static so we can access it from anywhere
 * 
 * The goal is to have this class very simple. Just geting all of the Manager Components
 * 
 * All its' components are public so they be called as well so we can access them easily
 * i.e. GameManager.Instance.Component
*/

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //*****Components*****//
    [HideInInspector]
    public CardsManager CardsManager;
    [HideInInspector]
    public UIEventsManager UIEventsManager;



    private void Awake()
    {
        ///***Singleton***///
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);

        CardsManager = GetComponent<CardsManager>();
        UIEventsManager = GetComponent<UIEventsManager>();
    }
}