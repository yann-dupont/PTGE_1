using System;
using UnityEngine;

public class TomatoSpell : Spell
{
    [Header("Timing")]
    [SerializeField] private float timeBeforeExplosion = 2f;

    [Header("Explosion")]
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float stunDuration = 2f;

    [Header("VFX")]
    [SerializeField] private GameObject explosionVFXPrefab;

    private float timer = 0f;
    private bool hasExploded = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init(bool withPerfectDirection)
    {
        timer = 0f;
        hasExploded = false;
        if (withPerfectDirection)
        {
            // better tomato properties ??
        }
    }

    protected override void HandleSpell()
    {
        if (hasExploded) return;

        timer += Time.deltaTime;

        if (timer >= timeBeforeExplosion)
        {
            Explode();
        }
    }

    private void Explode()
    {
        hasExploded = true;

        if (explosionVFXPrefab != null)
        {
            Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            Debug.Log("Hit : " + hit.gameObject.name + " (tag: " + hit.gameObject.tag + ")");
            switch (hit.gameObject.tag)
            {
                case "Player":
                    PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.Stun(stunDuration);
                    }
                    break;

                case "Untagged":
                    Enemy enemy = hit.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.Stun(stunDuration);
                    }
                    break;
            }
        }

        Destroy(gameObject);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        // no
    }
}