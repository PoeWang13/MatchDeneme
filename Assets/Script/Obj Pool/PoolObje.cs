using UnityEngine;

public class PoolObje : MonoBehaviour
{
    [Header("Bu obje icin kullanılacak havuz")]
    public Pooler havuzum;

    public virtual void ObjeHavuzEnter()
    {
        gameObject.SetActive(false);
    }
    public virtual void ObjeHavuzExit()
    {
        gameObject.SetActive(true);
    }
}