using UnityEngine;
using UnityEngine.InputSystem;

public class LazyPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private InputSystem_Actions input;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.UI.Menu.started += OnMenuStarted;
        input.UI.Menu.canceled += OnMenuCanceled;

        input.Enable();
    }

    private void OnDisable()
    {
        input.UI.Menu.started -= OnMenuStarted;
        input.UI.Menu.canceled -= OnMenuCanceled;

        input.Disable();
    }

    private void OnMenuStarted(InputAction.CallbackContext ctx)
    {
        panel?.SetActive(true);
    }

    private void OnMenuCanceled(InputAction.CallbackContext ctx)
    {
        panel?.SetActive(false);
    }
}
