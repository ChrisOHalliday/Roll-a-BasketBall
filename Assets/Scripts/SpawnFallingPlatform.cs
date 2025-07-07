using UnityEngine;

public class SpawnFallingPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject platformPrefab;
    private GameObject childPlatform; 

    private void Update()
    {
        if (!HasChildPlatform()) 
        {
            Instantiate(platformPrefab,gameObject.transform);
        }
    }

    bool HasChildPlatform()
    {
        if (transform.childCount == 0)
        {
            return false;
        }
        return true;
    }

}
