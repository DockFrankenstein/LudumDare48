using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public Transform endPosition;
    public Transform endRotationDir;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            PlayerReference.singleton.move.enabled = false;
            PlayerReference.singleton.look.enabled = false;
            PlayerReference.singleton.sounds.enabled = false;

            Vector3 directionVector = (endRotationDir.position - endPosition.position).normalized;
            Quaternion endRotation = Quaternion.LookRotation(directionVector);
            StartCoroutine(AnimateEnding(endPosition.position, endRotation));
        }
    }

    IEnumerator AnimateEnding(Vector3 endPosition, Quaternion endRotation)
    {
        Transform player = PlayerReference.singleton.transform;
        Vector3 startPosition = player.position;
        Quaternion startRotation = player.rotation;
        
        float animDuration = 3.0f;
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, animDuration, 1);

        for(float t = 0; t < animDuration; t += 0.01f)
        {
            player.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(t));    
            player.rotation = Quaternion.Lerp(startRotation, endRotation, curve.Evaluate(t));
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2.0f);
        ScreenBlanker.BlackOutScreen(() => SceneManager.LoadScene("EndScene"));
    }

}
