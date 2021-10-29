using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Vector3")]
public class Vector3Variables : ScriptableObject
{
    public Vector3 value;

    public void RemoveValue(Vector3Variables deger)
    {
        value -= deger.value;
    }
    public void RemoveValue(Vector3 deger)
    {
        value -= deger;
    }
    public void AddValue(Vector3Variables deger)
    {
        value += deger.value;
    }
    public void AddValue(Vector3 deger)
    {
        value += deger;
    }
    public void SetValue(Vector3Variables deger)
    {
        value = deger.value;
    }
    public void SetValue(Vector3 deger)
    {
        value = deger;
    }
}