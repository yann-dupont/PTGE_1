using System;
using UnityEngine;

public class TutoManager : MonoBehaviour {

    [SerializeField] GameObject collectResourcesTuto;
    [SerializeField] GameObject convertToMoneyTuto;
    [SerializeField] GameObject combosTuto;

    int currentTutoProgression;


    void Start() {
        collectResourcesTuto.SetActive(true);
        convertToMoneyTuto.SetActive(false);
        combosTuto.SetActive(false);
    }

    public void RegisterResourceCollected() {
        if (currentTutoProgression != 0) {
            return;
        }
        currentTutoProgression += 1;
        collectResourcesTuto.SetActive(false);
        convertToMoneyTuto.SetActive(true);
    }

    public void RegisterMoneyConverted() {
        if (currentTutoProgression != 1) {
            return;
        }
        currentTutoProgression += 1;
        convertToMoneyTuto.SetActive(false);
        combosTuto.SetActive(true);
        FindAnyObjectByType<Shop>().SpawnUpgrades();
    }

    public void RegisterComboUsed() {
        if (currentTutoProgression != 2) {
            return;
        }
        currentTutoProgression += 1;
        combosTuto.SetActive(false);
    }
}
