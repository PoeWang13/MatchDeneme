using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Guid")]
public class GuidVariables : ScriptableObject
{
    public Guid value;

    public void SetValue(GuidVariables deger)
    {
        value = deger.value;
    }
    public void SetValue(Guid deger)
    {
        value = deger;
    }
}