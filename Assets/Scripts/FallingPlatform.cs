using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float fallDelay = 0.75f;
    private float destroyDelay = 3.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();       
    }
    private void Start()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(StartFallingSequence());
        }
    }

    IEnumerator StartFallingSequence()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;
        rb.useGravity = true;
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }


}
