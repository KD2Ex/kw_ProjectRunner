//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/6_Scripts/Player/Input/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""5e2fdec2-d22a-49b4-a620-722ea000fcaa"",
            ""actions"": [
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""f7418d73-8331-401b-adce-6c29346b8f29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Stop"",
                    ""type"": ""Button"",
                    ""id"": ""305d8316-979a-4db7-83e0-a22ee1d43324"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""ba212413-8700-41e9-b33e-493eb91b372e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Slide"",
                    ""type"": ""Button"",
                    ""id"": ""559bdd88-ce83-40c8-961c-0ff20ccc37cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AirDash"",
                    ""type"": ""Button"",
                    ""id"": ""4655f2c5-fc12-47cf-b004-7fb7df781926"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AirDashMovement"",
                    ""type"": ""Button"",
                    ""id"": ""f8499fdc-b068-4bb9-a517-021a65a79337"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""36212c12-a711-4b27-a9f0-742b99b1f831"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MnK"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a509cd32-5f34-4645-a858-41c7bb44d025"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03b178f9-28b5-4559-805e-5942cf69c4bd"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MnK"",
                    ""action"": ""Stop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""efcead6b-7458-4dd8-9503-7ed14757093a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MnK"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d95d8031-6cf5-4701-a111-ef8f0ecd2527"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1018e498-6b85-4dee-81b2-41f39bc99815"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MnK"",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""090e36bb-86a9-4fb1-bdf4-cd130e4ff8aa"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AirDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""320ca018-cfbc-479a-8dd7-f92b4151abe7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AirDashMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5c75cc47-81af-4bb4-a161-e5428f8f54a1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MnK"",
                    ""action"": ""AirDashMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6016415a-aa8a-4575-ab7d-6e0e73352510"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MnK"",
                    ""action"": ""AirDashMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MnK"",
            ""bindingGroup"": ""MnK"",
            ""devices"": []
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": []
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Run = m_Gameplay.FindAction("Run", throwIfNotFound: true);
        m_Gameplay_Stop = m_Gameplay.FindAction("Stop", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Slide = m_Gameplay.FindAction("Slide", throwIfNotFound: true);
        m_Gameplay_AirDash = m_Gameplay.FindAction("AirDash", throwIfNotFound: true);
        m_Gameplay_AirDashMovement = m_Gameplay.FindAction("AirDashMovement", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
    private readonly InputAction m_Gameplay_Run;
    private readonly InputAction m_Gameplay_Stop;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Slide;
    private readonly InputAction m_Gameplay_AirDash;
    private readonly InputAction m_Gameplay_AirDashMovement;
    public struct GameplayActions
    {
        private @PlayerInput m_Wrapper;
        public GameplayActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Run => m_Wrapper.m_Gameplay_Run;
        public InputAction @Stop => m_Wrapper.m_Gameplay_Stop;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Slide => m_Wrapper.m_Gameplay_Slide;
        public InputAction @AirDash => m_Wrapper.m_Gameplay_AirDash;
        public InputAction @AirDashMovement => m_Wrapper.m_Gameplay_AirDashMovement;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @Stop.started += instance.OnStop;
            @Stop.performed += instance.OnStop;
            @Stop.canceled += instance.OnStop;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Slide.started += instance.OnSlide;
            @Slide.performed += instance.OnSlide;
            @Slide.canceled += instance.OnSlide;
            @AirDash.started += instance.OnAirDash;
            @AirDash.performed += instance.OnAirDash;
            @AirDash.canceled += instance.OnAirDash;
            @AirDashMovement.started += instance.OnAirDashMovement;
            @AirDashMovement.performed += instance.OnAirDashMovement;
            @AirDashMovement.canceled += instance.OnAirDashMovement;
        }

        private void UnregisterCallbacks(IGameplayActions instance)
        {
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @Stop.started -= instance.OnStop;
            @Stop.performed -= instance.OnStop;
            @Stop.canceled -= instance.OnStop;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Slide.started -= instance.OnSlide;
            @Slide.performed -= instance.OnSlide;
            @Slide.canceled -= instance.OnSlide;
            @AirDash.started -= instance.OnAirDash;
            @AirDash.performed -= instance.OnAirDash;
            @AirDash.canceled -= instance.OnAirDash;
            @AirDashMovement.started -= instance.OnAirDashMovement;
            @AirDashMovement.performed -= instance.OnAirDashMovement;
            @AirDashMovement.canceled -= instance.OnAirDashMovement;
        }

        public void RemoveCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_MnKSchemeIndex = -1;
    public InputControlScheme MnKScheme
    {
        get
        {
            if (m_MnKSchemeIndex == -1) m_MnKSchemeIndex = asset.FindControlSchemeIndex("MnK");
            return asset.controlSchemes[m_MnKSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnRun(InputAction.CallbackContext context);
        void OnStop(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSlide(InputAction.CallbackContext context);
        void OnAirDash(InputAction.CallbackContext context);
        void OnAirDashMovement(InputAction.CallbackContext context);
    }
}
