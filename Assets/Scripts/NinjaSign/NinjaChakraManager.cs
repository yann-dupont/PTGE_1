using System;
using UnityEngine;
using UnityEngine.UI;


public class NinjaChakraManager : MonoBehaviour
{
    
    [SerializeField] Image chakraBar;
    
    [Header("ChakraJaugedata")]
    [SerializeField] private int chakraGainRate = 1;
    private float currentChakraAmount;
    private float maxChakraAmount;
    public float CurrentChakraAmount => currentChakraAmount;
    

    private void Start()
    {
        maxChakraAmount = GameplayManager.instance.playerMaxShakra;
        currentChakraAmount = GameplayManager.instance.playerShakra;
        UpdateChakraBar();
        Debug.Log("NinjaChakraManager Start : " + CurrentChakraAmount);
    }

    public void ConsumeChakra(NinjaSignDescriptor ninjaSign)
    {
        currentChakraAmount -= ninjaSign.chakraCost;
        if (currentChakraAmount < 0)
        {
            currentChakraAmount = 0;
        }
        GameplayManager.instance.playerShakra = currentChakraAmount;
        Debug.Log("NinjaChakraManager ConsumeChakra : " + CurrentChakraAmount);
        UpdateChakraBar();
    }

    private void UpdateChakraBar()
    {
        chakraBar.fillAmount = currentChakraAmount / maxChakraAmount;
    }

    private void Update()
    {
        if (currentChakraAmount < maxChakraAmount)
        {
            currentChakraAmount += chakraGainRate * Time.deltaTime;
            if (currentChakraAmount > maxChakraAmount)
            {
                currentChakraAmount = maxChakraAmount;
            }
            GameplayManager.instance.playerShakra = currentChakraAmount;
            UpdateChakraBar();
        }
    }

    public void RestoreChakra(float amount) {
        currentChakraAmount = Mathf.Max(currentChakraAmount + amount, maxChakraAmount);
        GameplayManager.instance.playerShakra = currentChakraAmount;
        UpdateChakraBar();
    }
}

