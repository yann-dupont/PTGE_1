using UnityEngine;
using UnityEngine.InputSystem;

public class MenuParallax : MonoBehaviour
{

    public float offsetMultiplier = 1f;
    public float smoothTime = .3f;
    private Vector2 startPosition;
    private Vector3 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 offset =Camera.main.ScreenToViewportPoint(mousePosition);
        transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
    }
}
