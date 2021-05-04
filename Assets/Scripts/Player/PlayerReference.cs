using UnityEngine;
using qASIC;

public class PlayerReference : MonoBehaviour
{
    public static bool interactable = true;

    public static PlayerReference singleton;

    public PlayerMove move;
    public PlayerLook look;
    public Electricity.PlayerPower power;
    public PlayerDamage damage;
    public PlayerStepPlayer sounds;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            qDebug.Log("Assigned player", "Player");
        }
        if (singleton != this) Destroy(gameObject);
    }
}
