using System;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour {

    [SerializeField] public UpgradeInfo upgradeInfo;
    [SerializeField] public TextMeshProUGUI descriptionText;
    [SerializeField] public TextMeshProUGUI priceText;
    [SerializeField] public GameObject confirmButton;
    [HideInInspector] public bool isCollectible;

    PlayerController playerController;


    void Start() {
        descriptionText.text = upgradeInfo.description;
        priceText.text = "(" + upgradeInfo.price.ToString() + ")";
        confirmButton.SetActive(false);
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.TryGetComponent(out PlayerController player)) {
            return;
        }
        confirmButton.SetActive(true);
        player.currentUpgrade = this;
    }

    private void OnTriggerExit(Collider other) {
        if (!other.TryGetComponent(out PlayerController player)) {
            return;
        }
        confirmButton.SetActive(false);
        player.currentUpgrade = null;
    }

    public void Collect() {
        if(upgradeInfo.heartsRestored > 0) {
            playerController.GetComponent<PlayerHealth>().Healed(upgradeInfo.heartsRestored);
        }
        if (upgradeInfo.chakraGained > 0) {
            playerController.GetComponent<NinjaChakraManager>().RestoreChakra(upgradeInfo.chakraGained);
        }
        if(upgradeInfo.comboUnlocked != null) {
            FindAnyObjectByType<NinjaSignVessel>().AddNinjaSignCombination(upgradeInfo.comboUnlocked);
            FindAnyObjectByType<SignsSign>().DisplayCombo(upgradeInfo.comboUnlocked);
        }

        upgradeInfo.count -= 1;
        FindAnyObjectByType<Shop>().CollectUpgrade(gameObject);
    }
}

[Serializable]
public struct UpgradeInfo {

    public int price;
    public string description;
    [Tooltip("The bigger, the more often this upgrade will appear")]
    public int weight;
    [Tooltip("How many times the upgrade can appear")]
    public int count;

    public int heartsRestored;
    public float chakraGained;
    public NinjaSignCombination comboUnlocked; 

}
