using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int")]
public class IntVariables : ScriptableObject
{
    public int value;

    public void RemoveValue(IntVariables deger)
    {
        value -= deger.value;
    }
    public void RemoveValue(int deger)
    {
        value -= deger;
    }
    public void AddValue(IntVariables deger)
    {
        value += deger.value;
    }
    public void AddValue(int deger)
    {
        value += deger;
    }
    public void SetValue(IntVariables deger)
    {
        value = deger.value;
    }
    public void SetValue(int deger)
    {
        value = deger;
    }
}