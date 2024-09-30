using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Scriptable Objects/Player/Input")]
public class InputReader : ScriptableObject, PlayerInput.IGameplayActions
{
    private PlayerInput _input;

    public UnityAction<bool> RunEvent;
    public UnityAction StopEvent;
    
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

        /*Debug.Log("Started: " + context.started);
        Debug.Log("Performed: " + context.performed);
        Debug.Log("Canceled: " + context.canceled);
        */
        
        RunEvent?.Invoke(context.performed);
    }

    public void OnStop(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        StopEvent?.Invoke();
    }
}
