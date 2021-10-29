using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Genel/Value")]
public class ValueVariables : ScriptableObject
{
    // Oyundaki altın can gibi değişkenleri tutar.
    public int value;

    public bool ValueCheck(int amount)
    {
        if (value >= amount)
        {
            return true;
        }
        return false;
    }
    public void ValueIncrease(TextMeshProUGUI text, int increaseAmount)
    {
        value += increaseAmount;
        text.text = value.ToString("0");
    }
    public void ValueDecrease(TextMeshProUGUI text, int decreaseAmount)
    {
        value -= decreaseAmount;
        text.text = value.ToString("0");
    }
    public void ValueSet(TextMeshProUGUI text, int setAmount)
    {
        value = setAmount;
        text.text = value.ToString("0");
    }
}