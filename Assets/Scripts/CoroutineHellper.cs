using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper instance;

    public static CoroutineHelper Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject coroutineHelperObject = new GameObject("CoroutineHelper");
                instance = coroutineHelperObject.AddComponent<CoroutineHelper>();
            }
            return instance;
        }
    }
}
