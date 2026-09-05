using UnityEngine;

public class OrbitRotation : MonoBehaviour
{
    public float orbitSpeed = 90f; // degrés par seconde

    void Update()
    {
        transform.Rotate(Vector3.up, orbitSpeed * Time.deltaTime, Space.Self);
    }
}