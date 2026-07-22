using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    private float currentCollectingTime = 0f;
    private Collectable currentCollectable = null;

    private float collected = 0;

    [SerializeField] private ScoreUpdater scoreUpdater;

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

        if (input.Player.Interact.IsPressed())
        {
            Collect();
        }
        else
        {
            currentCollectingTime = 0f;
            EnablePlayerMovement();
        }

        currentCollectable?.CollectEffect(currentCollectingTime);
        
    }


    private void Collect()
    {
        currentCollectingTime += Time.deltaTime;
        if (currentCollectingTime < currentCollectable.GetCollectionDuration())
        {
            DisablePlayerMovement();
        }
        else
        {
            // complete collecting
            collected += currentCollectable.GetCollectionDuration();
            if (scoreUpdater)
                scoreUpdater.SetPreScore(collected);

            currentCollectable.gameObject.SetActive(false);
            currentCollectable = null;
            currentCollectingTime = 0f;
            EnablePlayerMovement();
        }
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

    /// <summary>
    /// Drop collected to tent
    /// </summary>
    private void HandleCollectableTentDrop()
    {
        if (scoreUpdater)
        {
            scoreUpdater.IncrementScore(collected);
            scoreUpdater.SetPreScore(0);
        }
        collected = 0;
    }
}