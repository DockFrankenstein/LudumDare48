using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public static class UIAnimations
{
    public static IEnumerator AnimateGraphicsColor(
        Graphic graphic,
        Color startColor,
        Color destinationColor,
        AnimationCurve animationCurve,
        float delay = 0,
        Action animationEndDelegate = null)
    {
        graphic.color = Color.Lerp(startColor, destinationColor, animationCurve.Evaluate(0));
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        float animationLength = GetAnimationLength(animationCurve);
        for(float time = 0; time <= animationLength; time += Time.deltaTime)
        {
            graphic.color = Color.Lerp(startColor, destinationColor, animationCurve.Evaluate(time));
            yield return new WaitForEndOfFrame();
        }
        animationEndDelegate?.Invoke();
    }
    public static float GetAnimationLength(AnimationCurve curve)
    {
        return curve.keys[curve.length - 1].time;
    }
}
