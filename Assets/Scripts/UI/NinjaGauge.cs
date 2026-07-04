using System;
using UnityEngine;
using UnityEngine.UI;

public class NinjaGauge : MonoBehaviour {
	[SerializeField]
	private Image mainNinjaSignImage;

	private void Start() {
		if (NinjaSignVessel.HasIsntance(gameObject.scene)) {
			NinjaSignVessel.Instance(gameObject.scene).OnNinjaSignAdded += OnNinjaSignAdded;
			NinjaSignVessel.Instance(gameObject.scene).OnNinjaCombinationExecuted += OnNinjaCombinationExecuted;
		}
		
		if (mainNinjaSignImage) {
			mainNinjaSignImage.sprite = null;
		}
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
	}

	private void OnNinjaCombinationExecuted(NinjaSignCombination executedNinjaCombination) {
		if (mainNinjaSignImage) {
			mainNinjaSignImage.sprite = null;
		}
	}
}
