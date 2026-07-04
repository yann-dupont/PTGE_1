using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashScreen : MonoBehaviour {
	[SerializeField]
	private TextMeshProUGUI textBlock;
	
	[SerializeField]
	private CanvasGroup canvasGroup;

	[SerializeField]
	private float timeToFlash = 1f;

	private float flashTimer = 0f;
	
	private static Dictionary<Scene, FlashScreen> instances = new Dictionary<Scene, FlashScreen>();

	private Coroutine displayCoroutine = null;
	
	public static bool HasIsntance(Scene forScene) {
		return instances.ContainsKey(forScene);
	}

	public static FlashScreen Instance(Scene forScene) {
		return instances[forScene];
	}
	
	private void Awake() {
		if (HasIsntance(gameObject.scene)) {
			Debug.Log("Ok buddy.");
			DestroyImmediate(gameObject);
		} else {
			instances.Add(gameObject.scene, this);
		}
	}

	private void OnDestroy() {
		if (HasIsntance(gameObject.scene) && Instance(gameObject.scene) == this) {
			instances.Remove(gameObject.scene);
		}
	}

	public void Display(string withMessage) {
		if (textBlock) {
			textBlock.text = withMessage;
		}

		if (displayCoroutine != null) {
			StopCoroutine(displayCoroutine);
			displayCoroutine = null;
		}
		
		displayCoroutine = StartCoroutine(DisplayRoutine());
	}

	public IEnumerator DisplayRoutine() {
		flashTimer = 0f;
		if (canvasGroup) {
			canvasGroup.alpha = 1f;
		}

		while (flashTimer < timeToFlash) {
			yield return null;
			flashTimer += Time.deltaTime;
			canvasGroup.alpha = 1f - (flashTimer / timeToFlash);
		}

		if (canvasGroup) {
			canvasGroup.alpha = 0f;
		}
		displayCoroutine = null;
	}
}
