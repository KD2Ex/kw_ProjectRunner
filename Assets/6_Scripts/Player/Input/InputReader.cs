using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Scriptable Objects/Player/Input")]
public class InputReader : ScriptableObject, 
    PlayerInput.IGameplayActions, 
    PlayerInput.IDevActions, 
    PlayerInput.IUIActions,
    PlayerInput.IPlayerBossActions,
    PlayerInput.ICutsceneActions
{
    private PlayerInput _input;

    public UnityAction<bool> RunEvent;
    public UnityAction<bool> StopEvent;
    public UnityAction<bool> JumpEvent;
    public UnityAction<Vector2> TouchJumpEvent;
    public UnityAction<bool> SlideEvent;
    public UnityAction<bool> AirDashEvent;
    public UnityAction<float> AirDashMovementEvent;
    public UnityAction DashAbilityTest;
    public UnityAction RestartScenesEvent;
    public UnityAction EscEvent;
    public UnityAction InteractEvent;
    public UnityAction SlowTimeEvent;

    public UnityAction<int> PlayerBossMoveEvent;
    
    public UnityAction<int> UIXMoveEvent;
    public UnityAction<int> UIYMoveEvent;

    public UnityAction<float> XYMoveEvent;
    
    public UnityAction CutsceneSkipEvent; 
    
    public bool RunTriggered =>_input.Gameplay.Run.triggered;
    public float XMoveValue => _input.UI.XMove.ReadValue<float>();

    public void DisableBossGameplayInput()
    {
        _input.PlayerBoss.Disable();
    }

    public void EnableBossGameplayInput()
    {
        _input.PlayerBoss.Enable();
    }
    
    public void DisableGameplayInput()
    {
        _input.Gameplay.Disable();
    }

    public void EnableGameplayInput()
    {
        _input.Gameplay.Enable();
    }

    public void DisableUIInput() => _input.UI.Disable();
    public void EnableUIInput() => _input.UI.Enable();
    
    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new PlayerInput();
            _input.Dev.SetCallbacks(this);
            _input.Gameplay.SetCallbacks(this);
            _input.UI.SetCallbacks(this);
            _input.PlayerBoss.SetCallbacks(this);
            _input.Cutscene.SetCallbacks(this);
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

    public void OnTouchJump(InputAction.CallbackContext context)
    {
        var pos = Touchscreen.current.primaryTouch.IsPressed() ? context.ReadValue<Vector2>() : Vector2.zero;
        TouchJumpEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnRestartScenes(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        RestartScenesEvent?.Invoke();
    }

    public void OnSlowTime(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        SlowTimeEvent?.Invoke();
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

    public void OnYMove(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        UIYMoveEvent?.Invoke((int)context.ReadValue<float>());
    }

    public void OnXMove(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        UIXMoveEvent?.Invoke((int)context.ReadValue<float>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        PlayerBossMoveEvent?.Invoke(Mathf.RoundToInt(context.ReadValue<float>()));
    }

    public void OnSkip(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        CutsceneSkipEvent?.Invoke();
        Debug.Log("Cut event invoked");
    }

    public void OnXYMove(InputAction.CallbackContext context)
    {
        //Debug.Log(context.performed);
        XYMoveEvent?.Invoke(context.ReadValue<float>());
    }
}
