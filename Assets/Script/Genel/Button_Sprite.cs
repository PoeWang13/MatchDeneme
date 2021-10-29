using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Button_Sprite : MonoBehaviour
{
    // Ekrana eklenen sprite'ı button yapar

    public Action MouseClick = null;

    private void OnMouseDown()
    {
        MouseClick?.Invoke();
    }
}