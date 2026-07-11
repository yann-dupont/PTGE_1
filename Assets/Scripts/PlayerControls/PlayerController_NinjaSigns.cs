using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NinjaChakraManager))]
public partial class PlayerController
{
	private const float ACTION_BUFFER_TIMER = 0.25f;

	private List<InputAction> boundActions = new List<InputAction>();
	private List<NinjaSignDescriptor> ninjaSigns = new List<NinjaSignDescriptor>();

	private List<InputAction> actionBuffer = new List<InputAction>();
	private float actionBufferTime = 0f;
	
	private NinjaChakraManager chakraManager;
	private void OnEnable_NinjaSigns()
	{
		chakraManager = GetComponent<NinjaChakraManager>();
		BindToNinjaSigns();
	}
	
	
	private void OnDisable_NinjaSigns()
	{
		UnbindFromNinjaSigns();
	}

	private void Update_NinjaSigns()
	{
		if (actionBuffer.Count <= 0)
		{
			return;
		}

		actionBufferTime += Time.deltaTime;
		if (actionBufferTime >= ACTION_BUFFER_TIMER)
		{
			actionBufferTime = 0f;
			ExecuteSignFromActionBuffer();
			actionBuffer.Clear();
		}
	}

	private void BindToNinjaSigns()
	{
		if (!NinjaSignVessel.HasIsntance(gameObject.scene))
		{
			return;
		}

		ninjaSigns = NinjaSignVessel.Instance(gameObject.scene).NinjaSigns.ToList();
		ninjaSigns.Sort((a, b) => b.Actions.Count - a.Actions.Count);
		foreach (NinjaSignDescriptor ninjaSign in ninjaSigns)
		{
			foreach (InputActionReference actionReference in ninjaSign.Actions)
			{
				InputAction action = actionReference.action;
				if (action != null && !boundActions.Contains(action))
				{
					boundActions.Add(action);
					action.performed += OnNinjaActionPerformed;
				}
			}
		}
	}

	private void UnbindFromNinjaSigns()
	{
		foreach (InputAction boundAction in boundActions)
		{
			if (boundAction != null)
			{
				boundAction.performed -= OnNinjaActionPerformed;
			}
		}
	}

	private void OnNinjaActionPerformed(InputAction.CallbackContext context)
	{
		InputAction performedAction = context.action;
		actionBuffer.Add(performedAction);
	}

	private void ExecuteSignFromActionBuffer()
	{
		if (!NinjaSignVessel.HasIsntance(gameObject.scene))
		{
			return;
		}

		NinjaSignDescriptor executedSign = GetSignFromActionBuffer();

		if (executedSign == null)
			return;
	
		chakraManager.ConsumeChakra(executedSign);
		PlayNinjaSignAnimation(executedSign);

		NinjaSignVessel.Instance(gameObject.scene)
			.AddNinjaSign(executedSign);
	}

	private NinjaSignDescriptor GetSignFromActionBuffer()
	{
		if (actionBuffer.Count <= 0)
		{
			return null;
		}

		foreach (NinjaSignDescriptor ninjaSign in ninjaSigns)
		{
			bool canExecute = true;
			foreach (InputActionReference ninjaSignActionReference in ninjaSign.Actions)
			{
				InputAction ninjaSignAction = ninjaSignActionReference.action;
				if (!actionBuffer.Contains(ninjaSignAction) || !HasEnoughChakra(ninjaSign))
				{
					canExecute = false;
					break;
				}
			}

			if (canExecute)
			{
				return ninjaSign;
			}
		}

		return null;
	}
	private bool HasEnoughChakra(NinjaSignDescriptor ninjaSign)
	{
		return chakraManager.CurrentChakraAmount >= ninjaSign.chakraCost;
	}
}