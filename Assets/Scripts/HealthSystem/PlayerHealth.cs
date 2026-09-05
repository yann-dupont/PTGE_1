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
    

    //Stun
    [Header("Stun")]
    [SerializeField] private GameObject stunEffectPrefab;
    [SerializeField] private float headHeight = 2f;
    private PlayerController playerController;
    private bool isStunned = false;
    public bool IsStunned => isStunned;
    private Coroutine stunCoroutine;
    private GameObject currentStunEffect;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = GameplayManager.instance.playerMaxHealth; 
        currentHealth = GameplayManager.instance.playerHealth;
        playerController = GetComponent<PlayerController>();
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

    public void Stun(float duration)
    {
        Debug.Log("Stun() appelé, duration = " + duration);
        if (stunCoroutine != null)
        {
            StopCoroutine(stunCoroutine);
            playerController.EnablePlayerMovement(); // reset propre avant de relancer
        }
        stunCoroutine = StartCoroutine(StunRoutine(duration));
    }

    private IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        if (stunEffectPrefab != null && headHeight != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * headHeight;
            currentStunEffect = Instantiate(stunEffectPrefab, spawnPosition, Quaternion.identity, transform);
        }

        playerController.DisablePlayerMovement();
        Debug.Log("DisablePlayerMovement appelé");
        yield return new WaitForSeconds(duration);

        playerController.EnablePlayerMovement();
        Debug.Log("EnablePlayerMovement appelé");
        isStunned = false;
        stunCoroutine = null;
    }


// Update is called once per frame
    void Update()
    {
        
    }
}
