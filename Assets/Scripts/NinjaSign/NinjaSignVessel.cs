using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NinjaSignVessel : MonoBehaviour {
	private static Dictionary<Scene, NinjaSignVessel> instances;

	public static bool HasIsntance(Scene forScene) {
		return instances.ContainsKey(forScene);
	}

	public NinjaSignVessel Instance(Scene forScene) {
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
}
