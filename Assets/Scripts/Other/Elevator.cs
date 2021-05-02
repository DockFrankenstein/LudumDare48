using qASIC;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
	public TriggerController controller;
	public bool isEnd;
    public string sceneName;

    [Header("Animation")]
    private Animator anim;
    public string DoorCloseTriggerName = "close";
    public float SceneLoadDelay = 5f;
    public string DoorOpenTriggerName = "open";
    public float DoorOpenDelay = 3f;

    bool isUsed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller?.ColliderEnter?.AddListener(OnInElevator);
    }

    private void Start()
    {
        if (isEnd)
        {
            anim.SetTrigger(DoorOpenTriggerName);
            return;
        }

        StartCoroutine(InvokeActionAfterDelay(() => anim.SetTrigger(DoorOpenTriggerName), DoorOpenDelay));
    }

    public void OnInElevator(Collider collider)
    {
        if (!isEnd || isUsed) return;
        if (collider.gameObject.tag != "Player") return;
        isUsed = true;
        anim?.SetTrigger(DoorCloseTriggerName);
        StartCoroutine(InvokeActionAfterDelay(LoadScene, SceneLoadDelay));
        qDebug.Log($"Elevator triggered. Finishing level with {PointCounter.GetPoints()}/{PointCounter.GetMaxPoints()} collected", "trigger");
    }

    public void LoadScene()
    {
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            qDebug.LogError("Cannot load scene in elevator!");
            return;
        }
        ScreenBlanker.BlackOutScreen(() =>
            SceneManager.LoadScene(sceneName));
    }

    IEnumerator InvokeActionAfterDelay(UnityAction action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
}
