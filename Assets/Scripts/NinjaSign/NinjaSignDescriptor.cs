using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Ninja Sign", menuName = "Ninja/Ninja Sign")]
public class NinjaSignDescriptor : ScriptableObject {
	[SerializeField]
	private List<InputActionReference> actions = new List<InputActionReference>();

	[SerializeField]
	private string displayName = "";

	[SerializeField]
	private Sprite icon = null;
	
	public List<InputActionReference> Actions => actions;
	public string DisplayName => displayName;
	public Sprite Icon => icon;
}
