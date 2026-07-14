using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject stockContainer;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private int maxHealth;
    
    private List<Heart> healthPoints = new List <Heart>();
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = 1;
        for (int i = 0; i < maxHealth; i++)
        {
            healthPoints.Add(Instantiate(heartPrefab, stockContainer.transform).GetComponent<Heart>());
        }
        UpdateHealthUI();
    }
    
    void TakeDamaged(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            //TBD : add Game Over
        }
        UpdateHealthUI();
    }

    public void Healed(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
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
