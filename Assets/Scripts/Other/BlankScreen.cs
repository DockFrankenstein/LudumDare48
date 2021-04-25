using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlankScreen : MonoBehaviour
{
    Graphic graphic;

    public Color showedColor;
    public Color hidedColor;

    public float blankingTime = 1;
    public bool hideOnSceneLoad = false;
    
    public void Show()
    {
        GetGraphic().color = showedColor;
    }
    public void Hide()
    {
        GetGraphic().color = hidedColor;
    }

    public void ShowAnimated(Action menuShowCallback)
    {
        StartCoroutine(AnimateColor(menuShowCallback, showedColor));
    }
    public void HideAnimated(Action menuHideCallback)
    {
        StartCoroutine(AnimateColor(menuHideCallback, hidedColor));
    }
    
    IEnumerator AnimateColor(Action animationEndCallback, Color destinationColor)
    {
        graphic = GetGraphic();
        yield return UIAnimations.AnimateGraphicsColor(
            graphic,
            graphic.color,
            destinationColor,
            AnimationCurve.Linear(0, 0, blankingTime, 1),
            0,
            () => animationEndCallback?.Invoke());
        graphic.color = destinationColor;
    }

    Graphic GetGraphic()
    {
        if (graphic == null)
            return GetComponent<Graphic>();
        return graphic;
    }
}
