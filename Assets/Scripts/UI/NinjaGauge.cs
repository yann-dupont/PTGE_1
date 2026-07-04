using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NinjaGauge : MonoBehaviour {
	private const int MAX_NUMBER_OF_ITEMS = 4;
	
	[SerializeField]
	private Image mainNinjaSignImage;
	
	[SerializeField]
	private HorizontalLayoutGroup ninaGaugeItemLayout;
	
	[SerializeField]
	private GameObject ninjaGaugeItem;
	
	private List<GameObject> ninjaGaugeItems = new List<GameObject>();
	private NinjaSignDescriptor previousNinjaSign = null;

	private void Start() {
		if (NinjaSignVessel.HasIsntance(gameObject.scene)) {
			NinjaSignVessel.Instance(gameObject.scene).OnNinjaSignAdded += OnNinjaSignAdded;
			NinjaSignVessel.Instance(gameObject.scene).OnNinjaCombinationExecuted += OnNinjaCombinationExecuted;
		}
		
		if (mainNinjaSignImage) {
			mainNinjaSignImage.sprite = null;
		}

		ClearItems();
	}

	private void OnDestroy() {
		if (NinjaSignVessel.HasIsntance(gameObject.scene)) {
			NinjaSignVessel.Instance(gameObject.scene).OnNinjaCombinationExecuted -= OnNinjaCombinationExecuted;
			NinjaSignVessel.Instance(gameObject.scene).OnNinjaSignAdded -= OnNinjaSignAdded;
		}
	}

	private void OnNinjaSignAdded(NinjaSignDescriptor addedNinjaSign) {
		if (mainNinjaSignImage) {
			mainNinjaSignImage.sprite = addedNinjaSign.Icon;
		}

		if (previousNinjaSign != null) {
			AddItemFor(previousNinjaSign);
		}
		previousNinjaSign = addedNinjaSign;
	}

	private void OnNinjaCombinationExecuted(NinjaSignCombination executedNinjaCombination) {
		if (mainNinjaSignImage) {
			mainNinjaSignImage.sprite = null;
		}
		
		ClearItems();
	}

	private void AddItemFor(NinjaSignDescriptor ninjaSign) {
		if (ninjaSign == null) {
			return;
		}

		if (!ninjaGaugeItem) {
			return;
		}

		if (!ninaGaugeItemLayout) {
			return;
		}

		if (ninjaGaugeItems.Count == MAX_NUMBER_OF_ITEMS) {
			DestroyImmediate(ninjaGaugeItems[0]);
			ninjaGaugeItems.RemoveAt(0);
		}

		GameObject newNinjaGaugeItem = Instantiate(ninjaGaugeItem, ninaGaugeItemLayout.transform);
		Image imageComponent = newNinjaGaugeItem.GetComponent<Image>();
		if (imageComponent) {
			imageComponent.sprite = ninjaSign.Icon;
		}
		
		ninjaGaugeItems.Add(newNinjaGaugeItem);
	}

	private void ClearItems() {
		if (ninaGaugeItemLayout) {
			foreach (Transform child in ninaGaugeItemLayout.transform) 
			{
				Destroy(child.gameObject);
			}
		}
		
		ninjaGaugeItems.Clear();
		previousNinjaSign = null;
	}
}
