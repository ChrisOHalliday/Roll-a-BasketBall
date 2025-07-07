using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectables : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI RingCounterText;

    private int RingCounterValue = 0;

    private void Awake()
    {
        RingCounterText.text = "Rings Collected: " + RingCounterValue.ToString();
    }

    private void LateUpdate()
    {
        RingCounterText.text = "Rings Collected: " + RingCounterValue.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            RingCounterValue++;
            GameObject.Destroy(other.gameObject);
        }
    }

}
