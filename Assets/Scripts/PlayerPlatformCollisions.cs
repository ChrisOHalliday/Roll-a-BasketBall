using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformCollisions : MonoBehaviour
{

    public Transform parentPlatform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            other.gameObject.transform.SetParent(parentPlatform,true);          
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null,true);
        }
     
    }

}
