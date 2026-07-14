using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float collectionDuration = 3.0f;

    void Start()
    {
        gameObject.tag = "Collectable";
    }
    
    float  getCollectionDuration()
    {
        return collectionDuration;
    }
    
}
