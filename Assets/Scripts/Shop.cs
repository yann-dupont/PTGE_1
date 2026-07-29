using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] GameObject[] upgradePrefabs;
    [SerializeField] Transform[] upgradeSlots;

    List<GameObject> upgradePool;
    Upgrade[] upgradePoolInfo;
    GameObject[] currentAvailableUpgrades;


    void Start() {
        upgradePool = new List<GameObject>();
        foreach (GameObject upgradePrefab in upgradePrefabs) {
            GameObject upgradeGO = Instantiate(upgradePrefab, transform);
            upgradeGO.SetActive(false);
            upgradePool.Add(upgradeGO);
        }

        upgradePoolInfo = new Upgrade[upgradePool.Count];
        for (int i = 0; i < upgradePool.Count; i++) {
            upgradePoolInfo[i] = upgradePool[i].GetComponent<Upgrade>();
        }

        currentAvailableUpgrades = new GameObject[upgradeSlots.Length];

        StartCoroutine(TestSpawnUpgrade());
    }

    IEnumerator TestSpawnUpgrade() {
        yield return new WaitForSeconds(0.3f);
        SpawnUpgrades();
    }

    public void SpawnUpgrades() {
        StartCoroutine(SpawnUpgradesCoroutine());
    }

    IEnumerator SpawnUpgradesCoroutine() {
        foreach (GameObject upgradeGO in currentAvailableUpgrades) {
            if(upgradeGO == null) {
                continue;
            }
            upgradeGO.SetActive(false);
            Upgrade upgrade = upgradeGO.GetComponent<Upgrade>();
            upgrade.upgradeInfo.count += 1;
            yield return new WaitForSeconds(0.3f);
        }

        currentAvailableUpgrades = new GameObject[upgradeSlots.Length];

        for (int i = 0; i < upgradeSlots.Length; i++) {
            yield return new WaitForSeconds(0.3f);

            GameObject newUpgrade = GetRandomUpgrade();
            if (newUpgrade == null) {
                continue;
            }
            newUpgrade.SetActive(true);
            newUpgrade.transform.position = upgradeSlots[i].position;
            newUpgrade.transform.rotation = upgradeSlots[i].rotation;

            Upgrade upgrade = newUpgrade.GetComponent<Upgrade>();
            upgrade.upgradeInfo.count -= 1;
            upgrade.isCollectible = true;
            currentAvailableUpgrades[i] = newUpgrade;
        }
    }

    GameObject GetRandomUpgrade() {
        int totalWeight = 0;
        for (int i = 0; i < upgradePoolInfo.Length; i++) {
            if (upgradePoolInfo[i].upgradeInfo.count > 0 && !currentAvailableUpgrades.Contains(upgradePool[i])) {
                totalWeight += upgradePoolInfo[i].upgradeInfo.weight;
            }
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight + 1);

        for (int i = 0; i < upgradePoolInfo.Length; i++) {
            if (upgradePoolInfo[i].upgradeInfo.count > 0 && !currentAvailableUpgrades.Contains(upgradePool[i])) {
                randomValue -= upgradePoolInfo[i].upgradeInfo.weight;
                if (randomValue <= 0) {
                    return upgradePool[i];
                }
            }
        }

        return null;
    }

    public void CollectUpgrade(GameObject upgradeGO) {
        foreach (GameObject upgradeGO1 in currentAvailableUpgrades) {
            if (upgradeGO1 == null) {
                continue;
            }
            Upgrade upgrade = upgradeGO1.GetComponent<Upgrade>();
            upgrade.isCollectible = false;
        }

        SpawnUpgrades();
    }
}
