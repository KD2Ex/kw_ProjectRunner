using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Scriptable Objects/Player/Input")]
public class InputReader : ScriptableObject, PlayerInput.IGameplayActions
{
    private PlayerInput _input;

    public UnityAction<bool> RunEvent;
    public UnityAction StopEvent;
    public UnityAction<bool> JumpEvent;
    
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
        RunEvent?.Invoke(context.performed);
    }

    public void OnStop(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        StopEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke(context.performed || context.started);
    }
}
