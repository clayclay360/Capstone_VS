//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Player/ControlsVS.inputactions
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

public partial class @ControlsVS : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControlsVS()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControlsVS"",
    ""maps"": [
        {
            ""name"": ""Player Controls VS"",
            ""id"": ""1ee4d4ad-8ec2-495f-ac4b-db14a4c6d5d2"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""81475f36-d89c-4bc0-8e50-8be069727d44"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""b3ada644-5c66-4e90-a457-4d0d2343d534"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""GamePad"",
                    ""id"": ""9b3b8244-281c-43fc-b5a2-ba3fb9b47b61"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""766e466a-42fc-492a-aee5-9b4bbfa141f4"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Controls VS"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fff1f534-5760-4a05-adcd-e31d457a88df"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Controls VS"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b33ef9c3-258f-442f-9246-cd95f670dace"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Controls VS"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9dee68ec-35c6-4a98-97cd-f9621838bcaf"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Controls VS"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""GamePad"",
                    ""id"": ""51b36f47-e654-404a-bf49-9e3c883b6c14"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""18afc6bc-5a4b-41f2-8831-d712289f7b5c"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Controls VS"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a63b35ba-3e9e-4180-b43a-e3de80ab2eee"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Controls VS"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f6f06836-1a57-4133-87ee-ffbbbadcfe96"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Controls VS"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""48ff7795-89f5-4223-909d-0b82f57de12e"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Controls VS"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""All Controls VS"",
            ""bindingGroup"": ""All Controls VS"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player Controls VS
        m_PlayerControlsVS = asset.FindActionMap("Player Controls VS", throwIfNotFound: true);
        m_PlayerControlsVS_Move = m_PlayerControlsVS.FindAction("Move", throwIfNotFound: true);
        m_PlayerControlsVS_Look = m_PlayerControlsVS.FindAction("Look", throwIfNotFound: true);
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

    // Player Controls VS
    private readonly InputActionMap m_PlayerControlsVS;
    private IPlayerControlsVSActions m_PlayerControlsVSActionsCallbackInterface;
    private readonly InputAction m_PlayerControlsVS_Move;
    private readonly InputAction m_PlayerControlsVS_Look;
    public struct PlayerControlsVSActions
    {
        private @ControlsVS m_Wrapper;
        public PlayerControlsVSActions(@ControlsVS wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerControlsVS_Move;
        public InputAction @Look => m_Wrapper.m_PlayerControlsVS_Look;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControlsVS; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsVSActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsVSActions instance)
        {
            if (m_Wrapper.m_PlayerControlsVSActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerControlsVSActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControlsVSActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControlsVSActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_PlayerControlsVSActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerControlsVSActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerControlsVSActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_PlayerControlsVSActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public PlayerControlsVSActions @PlayerControlsVS => new PlayerControlsVSActions(this);
    private int m_AllControlsVSSchemeIndex = -1;
    public InputControlScheme AllControlsVSScheme
    {
        get
        {
            if (m_AllControlsVSSchemeIndex == -1) m_AllControlsVSSchemeIndex = asset.FindControlSchemeIndex("All Controls VS");
            return asset.controlSchemes[m_AllControlsVSSchemeIndex];
        }
    }
    public interface IPlayerControlsVSActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
}
