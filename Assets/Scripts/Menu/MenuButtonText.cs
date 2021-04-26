using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;

    public string startText;
    public string endText;

    string mainText;

    private void Awake()
    {
        if (text == null) return;
        mainText = text.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (text == null) return;
        text.text = $"{startText}{mainText}{endText}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (text == null) return;
        text.text = mainText;
    }
}
