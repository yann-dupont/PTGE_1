using System;
using UnityEngine;
using UnityEngine.UI;


public class NinjaChakraManager : MonoBehaviour
{
    
    [SerializeField] Image chakraBar;
    
    [Header("ChakraJaugedata")]
    [SerializeField] private float maxChakraAmount;
    [SerializeField] private int chakraGainRate = 1;
    private float currentChakraAmount;
    public float CurrentChakraAmount => currentChakraAmount;
    

    private void Start()
    {
        currentChakraAmount = maxChakraAmount;
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
            UpdateChakraBar();
        }
    }
}

