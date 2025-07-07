using UnityEngine;

public class SpeedPlatform : MonoBehaviour
{

    [SerializeField] private int playerIncreasedSpeed;
    private float originalSpeed;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerStats>(out PlayerStats player))
        {
            originalSpeed = player.GetPlayerSpeed();
            player.SetPlayerSpeed(playerIncreasedSpeed);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerStats>(out PlayerStats player))
        {
            player.SetPlayerSpeed(originalSpeed);
        }
    }
}
