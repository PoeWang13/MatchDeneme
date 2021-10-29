using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Genel/Havuz")]
public class Pooler : ScriptableObject
{
    #region GameObject
    [Header("Oluşturulacak Obje")]
    public PoolObje prefab;
    public Queue<PoolObje> havuz = new Queue<PoolObje>();
    public void ObjeyiHavuzaYerlestir(PoolObje pool)
    {
        // Kullanılabilir objeyi havuza eklerken düzenle
        pool.ObjeHavuzEnter();
        // Kullanılabilir objeyi havuza ekle
        havuz.Enqueue(pool);
    }
    public PoolObje HavuzdanObjeIste(Vector3 pos)
    {
        PoolObje havuzObjesi = null;
        // Havuzda kullanılabilir obje varsa onu kullan
        if (havuz.Count > 0)
        {
            havuzObjesi = havuz.Dequeue();
            if (havuzObjesi == null)
            {
                // Kullanılabilir obje havuza eklendikten sonra silinmisse
                havuzObjesi = Instantiate(prefab, pos, Quaternion.identity);
            }
            else
            {
                // Kullanılabilir objeyi düzenle
                havuzObjesi.transform.position = pos;
                havuzObjesi.ObjeHavuzExit();
            }
            return havuzObjesi;
        }
        // Kullanılabilir obje yoksa yeni obje ins et
        havuzObjesi = Instantiate(prefab, pos, Quaternion.identity);
        // Yeni objeyi gönder
        return havuzObjesi;
    }
    #endregion
}