using System.Collections;
using qASIC;
using UnityEngine;
using UnityEngine.SceneManagement;
using qASIC.AudioManagment;

public class EndTrigger : MonoBehaviour
{
    public float EndingTolerance;

    public Transform endPosition;
    public Transform endRotationDir;

    public AudioData announcerGood;
    public AudioData announcerBad;

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

        bool isGood = PointCounter.GetMaxPoints() == 0 || PointCounter.GetPoints() / (float)PointCounter.GetMaxPoints() >= EndingTolerance;
        qDebug.Log($"Ending triggered. Collected {PointCounter.GetPoints()}/{PointCounter.GetMaxPoints()}. Playing ending {(isGood ? "good" : "bad")}", "trigger");
        AudioData clip = isGood ? announcerGood : announcerBad;

        AudioManager.Play("announcer", clip);
        
        for(float t = 0; t < animDuration; t += Time.deltaTime)
        {
            player.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(t));    
            player.rotation = Quaternion.Lerp(startRotation, endRotation, curve.Evaluate(t));
            yield return null;
        }

        yield return new WaitForSecondsRealtime(clip.clip.length - animDuration);
        ScreenBlanker.BlackOutScreen(() => SceneManager.LoadScene("Menu"));
    }

}
