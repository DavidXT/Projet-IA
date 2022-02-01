using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class TextButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    // add callbacks in the inspector like for buttons
    public UnityEvent onClick;
    public TMP_Text text;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // invoke your event
        onClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        text.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        text.color = Color.white;
    }

}