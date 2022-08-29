using UnityEngine;

/* The purpose of this script is: to load the GameManager GameObject before eveything else
 * That way we won't have to care if we put it in any scene but still ensuring we can rely on it
*/

public static class ManagerInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute() => Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Game Manager")));
}