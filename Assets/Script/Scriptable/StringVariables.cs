using UnityEngine;

[CreateAssetMenu(menuName = "Variables/String")]
public class StringVariables : ScriptableObject
{
    public string value;

    public void SetValue(StringVariables deger)
    {
        value = deger.value;
    }
    public void SetValue(string deger)
    {
        value = deger;
    }
}