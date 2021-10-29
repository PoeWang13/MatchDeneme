using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Float")]
public class FloatVariables : ScriptableObject
{
    public float value;

    public void RemoveValue(FloatVariables deger)
    {
        value -= deger.value;
    }
    public void RemoveValue(float deger)
    {
        value -= deger;
    }
    public void AddValue(FloatVariables deger)
    {
        value += deger.value;
    }
    public void AddValue(float deger)
    {
        value += deger;
    }
    public void SetValue(FloatVariables deger)
    {
        value = deger.value;
    }
    public void SetValue(float deger)
    {
        value = deger;
    }
}