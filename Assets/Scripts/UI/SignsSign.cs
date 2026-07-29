using System.Collections.Generic;
using UnityEngine;

public class SignsSign : MonoBehaviour {

    [SerializeField] private GameObject rowPrefab;

    private List<GameObject> rows;

    private void Start() {
        rows = new List<GameObject>();

        NinjaSignVessel vessel = FindAnyObjectByType<NinjaSignVessel>();
        foreach (NinjaSignCombination combo in vessel.NinjaSignCombinations) {
            DisplayCombo(combo);
        }
    }

    public void DisplayCombo(NinjaSignCombination combo) {
        GameObject newRow = Instantiate(rowPrefab, transform);
        ComboDisplay comboDisplay = newRow.GetComponent<ComboDisplay>();
        comboDisplay.DisplayCombo(combo);
        rows.Add(newRow);
    }
}
