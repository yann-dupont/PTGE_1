using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject stockContainer;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject deathscreen;

    private int maxHealth;
    
    private List<Heart> healthPoints = new List <Heart>();
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = GameplayManager.instance.playerMaxHealth; 
        currentHealth = GameplayManager.instance.playerHealth;
        for (int i = 0; i < maxHealth; i++)
        {
            healthPoints.Add(Instantiate(heartPrefab, stockContainer.transform).GetComponent<Heart>());
        }
        UpdateHealthUI();
    }
    
    public void TakeDamaged(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            deathscreen.SetActive(true);
            //TBD : add Game Over
        }
        GameplayManager.instance.playerHealth = currentHealth;
        UpdateHealthUI();
    }

    public void Healed(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        GameplayManager.instance.playerHealth = currentHealth;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < healthPoints.Count; i++)
        {
            if (i < currentHealth)
            {
                healthPoints[i].ActivateHearthFill();
            }
            else
            {
                healthPoints[i].DeactivateHearthFill();
            }
        }
    }

// Update is called once per frame
    void Update()
    {
        
    }
}
