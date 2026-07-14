using System;
using UnityEngine;

public class HealSpell : Spell
{
    [SerializeField] private int healAmount;
    private PlayerHealth playerHealth;
    private float timer;
    
    public void Init(PlayerHealth playerHealth)
    {
        timer = 0f;
        this.playerHealth = playerHealth;
        playerHealth.Healed(healAmount);
    }
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void HandleSpell()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
}
