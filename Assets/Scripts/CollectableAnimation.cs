using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAnimation : MonoBehaviour
{

    [SerializeField,Range(60.0f,120.0f)]
    private float rotationSpeed = 60.0f;

    [SerializeField] private float bobbleSpeed = 0.0003f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.localPosition += Vector3.up * Mathf.Cos(Time.time) * bobbleSpeed * Time.deltaTime;
        
   
    }
}
