using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Scriptable Objects/Player/Input")]
public class InputReader : ScriptableObject, PlayerInput.IGameplayActions, PlayerInput.IDevActions, PlayerInput.IUIActions
{
    private PlayerInput _input;

    public UnityAction<bool> RunEvent;
    public UnityAction<bool> StopEvent;
    public UnityAction<bool> JumpEvent;
    public UnityAction<bool> SlideEvent;
    public UnityAction<bool> AirDashEvent;
    public UnityAction<float> AirDashMovementEvent;
    public UnityAction DashAbilityTest;
    public UnityAction RestartScenesEvent;
    public UnityAction EscEvent;
    public UnityAction InteractEvent;
    
    public bool RunTriggered =>_input.Gameplay.Run.triggered;

    
    public void DisableGameplayInput()
    {
        _input.Gameplay.Disable();
    }

    public void EnableGameplayInput()
    {
        _input.Gameplay.Enable();
    }
    
    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new PlayerInput();
            _input.Dev.SetCallbacks(this);
            _input.Gameplay.SetCallbacks(this);
            _input.UI.SetCallbacks(this);
        }
        
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        RunEvent?.Invoke(context.started || context.performed);
    }

    public void OnStop(InputAction.CallbackContext context)
    {
        StopEvent?.Invoke(context.started || context.performed);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke(context.performed || context.started);
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        SlideEvent?.Invoke(context.performed || context.started);
    }

    public void OnAirDash(InputAction.CallbackContext context)
    {
        AirDashEvent?.Invoke(context.performed || context.started);
    }

    public void OnAirDashMovement(InputAction.CallbackContext context)
    {
        if (context.performed) return;
        
        AirDashMovementEvent?.Invoke(context.ReadValue<float>());
    }

    public void OnDashAbilityTest(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        DashAbilityTest?.Invoke();
    }

    public void OnRestartScenes(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        RestartScenesEvent?.Invoke();
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        EscEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        InteractEvent?.Invoke();
    }
}
