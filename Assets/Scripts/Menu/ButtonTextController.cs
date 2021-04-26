using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu
{
    public class ButtonTextController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TextMeshProUGUI text;

        public Vector4 Margins;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (text == null) return;
            text.margin = Margins;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (text == null) return;
            text.margin = Vector4.zero;
        }
    }
}