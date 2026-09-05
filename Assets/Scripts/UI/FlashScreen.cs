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
	private float holdTime = 1f; // temps où le message reste pleinement visible

	[SerializeField]
	private float fadeTime = 1f; // temps du fondu de disparition

	private float flashTimer = 0f;
	
	private static Dictionary<Scene, FlashScreen> instances = new Dictionary<Scene, FlashScreen>();

	private Coroutine displayCoroutine = null;
	
	public static bool HasInstance(Scene forScene) {
		return instances.ContainsKey(forScene);
	}

	public static FlashScreen Instance(Scene forScene) {
		return instances[forScene];
	}
	
	private void Awake() {
		if (HasInstance(gameObject.scene)) {
			Debug.Log("Ok buddy.");
			DestroyImmediate(gameObject);
		} else {
			instances.Add(gameObject.scene, this);
		}
	}

	private void OnDestroy() {
		if (HasInstance(gameObject.scene) && Instance(gameObject.scene) == this) {
			instances.Remove(gameObject.scene);
		}
	}

	public void Display(string withMessage, float? customHoldTime = null, float? customFadeTime = null) {
		if (textBlock) {
			textBlock.text = withMessage;
		}

		if (displayCoroutine != null) {
			StopCoroutine(displayCoroutine);
			displayCoroutine = null;
		}
		
		float hold = customHoldTime ?? holdTime;
		float fade = customFadeTime ?? fadeTime;
		
		displayCoroutine = StartCoroutine(DisplayRoutine(hold, fade));
	}

	public IEnumerator DisplayRoutine(float hold, float fade) {
		flashTimer = 0f;
		if (canvasGroup) {
			canvasGroup.alpha = 1f;
		}

		// Phase 1 : reste pleinement visible
		yield return new WaitForSeconds(hold);

		// Phase 2 : fondu de disparition
		while (flashTimer < fade) {
			yield return null;
			flashTimer += Time.deltaTime;
			canvasGroup.alpha = 1f - (flashTimer / fade);
		}

		if (canvasGroup) {
			canvasGroup.alpha = 0f;
		}
		displayCoroutine = null;
	}
}