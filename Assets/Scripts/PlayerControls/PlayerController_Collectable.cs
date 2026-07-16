using UnityEngine;

public partial class PlayerController
{
    private float currentCollectingTime = 0f;
    private Collectable currentCollectable = null;
    
    private void HandleCollectableCollisionEnter(Collision collision)
    {
        if (!currentCollectable && collision.gameObject.CompareTag("Collectable"))
        {
            collision.gameObject.TryGetComponent<Collectable>(out currentCollectable);
            currentCollectable.EnableHighlight();
        }
    }

    private void HandleCollectableCollisionStay(Collision collision)
    {
        if (!currentCollectable || collision.gameObject != currentCollectable.gameObject)
            return;

        if (!input.Player.Interact.IsPressed())
        {
            currentCollectingTime = 0f;
            EnablePlayerMovement();
            return;
        }

        currentCollectingTime += Time.deltaTime;
        if (currentCollectingTime < currentCollectable.GetCollectionDuration())
        { 
            DisablePlayerMovement();
            return;
        }

        currentCollectable.gameObject.SetActive(false);
        currentCollectable = null;
        currentCollectingTime = 0f;
        EnablePlayerMovement();
    }

    private void HandleCollectableCollisionExit(Collision collision)
    {
        if (!currentCollectable || collision.gameObject != currentCollectable.gameObject)
            return;

        currentCollectable.DisableHighlight();
        currentCollectable = null;
        currentCollectingTime = 0f;
        EnablePlayerMovement();
    }
}