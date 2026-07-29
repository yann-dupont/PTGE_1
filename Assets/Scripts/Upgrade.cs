using System;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour {

    [SerializeField] public UpgradeInfo upgradeInfo;
    [SerializeField] public TextMeshProUGUI descriptionText;
    [SerializeField] public GameObject confirmButton;
    [HideInInspector] public bool isCollectible;

    void Start() {
        descriptionText.text = upgradeInfo.description;
        confirmButton.SetActive(false);
    }

    void Update() {
        descriptionText.text = upgradeInfo.description + " (" + upgradeInfo.count + " left)";
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
            // TODO
        }

        upgradeInfo.count -= 1;
        FindAnyObjectByType<Shop>().CollectUpgrade(gameObject);
    }
}

[Serializable]
public struct UpgradeInfo {

    public string description;
    [Tooltip("The bigger, the more often this upgrade will appear")]
    public int weight;
    [Tooltip("How many times the upgrade can appear. -1 for infinite")]
    public int count;

    public int heartsRestored;
    public float chakraGained;
    public NinjaSignCombination comboUnlocked;

}
