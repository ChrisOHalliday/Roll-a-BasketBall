using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float player_speed = 750.0f;
    [SerializeField] private int player_jumpHeight = 5;
    [SerializeField] private int player_jumps = 2;


    public void SetPlayerSpeed(float speed)
    {
        player_speed = speed;   
    }

    public void SetPlayerJumpHeight(int jumpHeight)
    {
        player_jumpHeight = jumpHeight;
    }

    public void SetNumberOfJumps(int numberOfJumps)
    {
        player_jumps = numberOfJumps;
    }

    public float GetPlayerSpeed()
    {
        return player_speed;
    }

    public int GetPlayerJumpHeight()
    {
        return player_jumpHeight;
    }

    public int GetNumberOfJumps()
    {
        return player_jumps;
    }


}
