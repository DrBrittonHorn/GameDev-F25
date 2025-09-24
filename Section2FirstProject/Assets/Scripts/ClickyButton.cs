using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite defaultImg, pressedImg;
    public Image button;
    public void OnPointerDown(PointerEventData eventData)
    {
        button.sprite = pressedImg;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        button.sprite = defaultImg;
    }
}
