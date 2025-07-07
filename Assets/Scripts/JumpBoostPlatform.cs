using UnityEngine;

public class JumpBoostPlatform : MonoBehaviour
{
    [SerializeField] private int playerIncreasedJumpheight;
    private int originalJumpHeight;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerStats>(out PlayerStats player))
        {
            originalJumpHeight = player.GetPlayerJumpHeight();
            player.SetPlayerJumpHeight(playerIncreasedJumpheight);
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerStats>(out PlayerStats player))
        {
            player.SetPlayerJumpHeight(originalJumpHeight);
        }
    }
}
