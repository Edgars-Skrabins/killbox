using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private bool m_dontDestroyOnLoad;
    
    private static T i;

    public static T I {get => i;}

    protected virtual void Awake()
    {
        if(i == null)
        {
            i = this as T;
            if(m_dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }
        else if(i != null && i != this)
        {
            Debug.LogError("There's more than one singleton " + gameObject.name + " of type " + GetType());
            Destroy(gameObject);
        }
    }
}

