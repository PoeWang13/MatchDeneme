using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Bool")]
public class BoolVariables : ScriptableObject
{
    public bool value;

    public void SetValue(BoolVariables deger)
    {
        value = deger.value;
    }
    public void SetValue(bool deger)
    {
        value = deger;
    }
}