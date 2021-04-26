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

    public Image Blocker;
    
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
        StartCoroutine(AnimateColor(blankingTime, menuShowCallback, showedColor));
    }
    public void HideAnimated(Action menuHideCallback)
    {
        StartCoroutine(AnimateColor(blankingTime, menuHideCallback, hidedColor));
    }
    public void HideAnimated(float duration, Action menuHideCallback)
    {
        StartCoroutine(AnimateColor(duration, menuHideCallback, hidedColor));
    }

    IEnumerator AnimateColor(float duration, Action animationEndCallback, Color destinationColor)
    {
        if (Blocker != null) Blocker.raycastTarget = true;
        graphic = GetGraphic();
        yield return UIAnimations.AnimateGraphicsColor(
            graphic,
            graphic.color,
            destinationColor,
            AnimationCurve.Linear(0, 0, duration, 1),
            0,
            () => animationEndCallback?.Invoke());
        graphic.color = destinationColor;
        if (Blocker != null) Blocker.raycastTarget = false;
    }

    Graphic GetGraphic()
    {
        if (graphic == null)
            return GetComponent<Graphic>();
        return graphic;
    }
}
