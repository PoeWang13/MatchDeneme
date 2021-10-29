using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Vector2")]
public class Vector2Variables : ScriptableObject
{
    public Vector2 value;

    public void RemoveValue(Vector2Variables deger)
    {
        value -= deger.value;
    }
    public void RemoveValue(Vector2 deger)
    {
        value -= deger;
    }
    public void AddValue(Vector2Variables deger)
    {
        value += deger.value;
    }
    public void AddValue(Vector2 deger)
    {
        value += deger;
    }
    public void SetValue(Vector2Variables deger)
    {
        value = deger.value;
    }
    public void SetValue(Vector2 deger)
    {
        value = deger;
    }
}