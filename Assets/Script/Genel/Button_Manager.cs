using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button_Manager : MonoBehaviour, IPointerClickHandler
{
    [Header("Do something with Other Object")]
    public UnityEvent otherEvents;
    [Header("Play Music - Close Object")]
    public UnityEvent objectEvents;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            otherEvents.Invoke();
            objectEvents.Invoke();
        }
    }
}