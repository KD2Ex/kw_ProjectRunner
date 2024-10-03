using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Scriptable Objects/Player/Input")]
public class InputReader : ScriptableObject, PlayerInput.IGameplayActions
{
    private PlayerInput _input;

    public UnityAction<bool> RunEvent;
    public UnityAction<bool> StopEvent;
    public UnityAction<bool> JumpEvent;
    public UnityAction<bool> SlideEvent;
    public UnityAction<bool> AirDashEvent;
    public UnityAction<float> AirDashMovementEvent;
    public UnityAction DashAbilityTest;
    
    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new PlayerInput();
            _input.Gameplay.SetCallbacks(this);
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
}
