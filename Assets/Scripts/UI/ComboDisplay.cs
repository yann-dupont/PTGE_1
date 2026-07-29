using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisplay : MonoBehaviour {

    [SerializeField] private GameObject cellPrefab;

    private List<GameObject> cells;


    public void DisplayCombo(NinjaSignCombination combo) {
        cells = new List<GameObject>();
        for (int i = 0; i < combo.SignsToActivate.Count; i++) {
            GameObject newCell = Instantiate(cellPrefab, transform);
            Image newImage = newCell.GetComponent<Image>();
            newImage.sprite = combo.SignsToActivate[i].Icon;

            cells.Add(newCell);
        }
    }
}
