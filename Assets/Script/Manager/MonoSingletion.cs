using UnityEngine;

public class MonoSingletion<T> : MonoBehaviour where T : MonoSingletion<T>
{
    // Diğer scriptlerden kullanılacak ve ekranda 1 tane olan scriptlere erişmek içindir.
    private static T instance = null;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        GetSomeTing();
    }
    // Component veya value yüklemelerini burda yap
    public virtual void GetSomeTing()
    {

    }
}