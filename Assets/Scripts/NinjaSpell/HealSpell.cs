using System;
using UnityEngine;

public class HealSpell : Spell
{
    [SerializeField] private int healAmount;
    [SerializeField] private int healAmountBetter;
    private PlayerHealth playerHealth;
    private float timer;
    
    public void Init(PlayerHealth playerHealth, bool withPerfectDirection)
    {
        timer = 0f;
        this.playerHealth = playerHealth;
        if (withPerfectDirection)
        {
            playerHealth.Healed(healAmountBetter);
        }
        else
        {
            playerHealth.Healed(healAmount);
        }

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
