using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    public static PlayerReference singleton;

    public PlayerMove move;
    public PlayerLook look;
    public Electricity.PlayerPower power;

    private void Awake()
    {
        if (singleton == null) singleton = this;
        if (singleton != this) Destroy(gameObject);
    }
}
