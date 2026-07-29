using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutoManager : MonoBehaviour {

    [SerializeField] GameObject collectResourcesTuto;
    [SerializeField] GameObject convertToMoneyTuto;
    [SerializeField] GameObject combosTuto;
    [SerializeField] GameObject directionalComboTuto;

    [SerializeField] CanvasGroup ninjaArea;

    int currentTutoProgression;


    void Start() {
        collectResourcesTuto.SetActive(true);
        convertToMoneyTuto.SetActive(false);
        combosTuto.SetActive(false);
        directionalComboTuto.SetActive(false);
        ninjaArea.alpha = 0f;
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
        ninjaArea.alpha = 1f;
    }

    public void RegisterComboUsed() {
        if (currentTutoProgression != 2) {
            return;
        }
        currentTutoProgression += 1;
        combosTuto.SetActive(false);
        directionalComboTuto.SetActive(true);
        StartCoroutine(HideTutoWithDelay(directionalComboTuto, 30f));
    }

    IEnumerator HideTutoWithDelay(GameObject tuto, float delay) {
        yield return new WaitForSeconds(delay);
        tuto.SetActive(false);
    }

}
