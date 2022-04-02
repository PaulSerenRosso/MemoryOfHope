//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Player/Input/Player.inputactions
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

public partial class @InputMaster : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""2ba51e39-8f39-4695-8d99-c56cf57369e8"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b4bc967c-b188-4d1e-a590-7a4f7dc46524"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""cbb6a423-44f6-4b10-8437-001b71891669"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""75bd5528-4e79-4dea-ab6a-7aba45b3089f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenCloseMap"",
                    ""type"": ""Button"",
                    ""id"": ""df62dc6e-43dc-4594-83ff-7804a1705148"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MovingOnMap"",
                    ""type"": ""Value"",
                    ""id"": ""84429ab2-2968-4199-b8b1-c58219159c38"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Prism"",
                    ""type"": ""Button"",
                    ""id"": ""fe029cfb-9718-4b07-a02a-ea75a2c27689"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""InteractionModule"",
                    ""type"": ""Button"",
                    ""id"": ""d59e9e21-3b38-4794-9627-f5e53fdee26f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""InteractionSelect"",
                    ""type"": ""Button"",
                    ""id"": ""4ddd3f0d-af99-47ab-8e03-59b3cbf1ba66"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""InteractionMove"",
                    ""type"": ""Value"",
                    ""id"": ""57dfab5c-cb93-497b-b7c0-185bd2334ee8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotateCamera"",
                    ""type"": ""Value"",
                    ""id"": ""d9723e69-aa1c-447e-9a6c-de4a755069da"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""39873438-dc12-4275-b24a-704fffb8ae7a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""a32ebeac-0599-46f7-aa34-cc3704e01d5a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8b633f8c-c5f7-49bd-a38b-de5becec14da"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b75cb467-474d-4c68-8287-915b8e147414"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""49f0bc8e-44c4-4dd8-91b5-cc79c2fbf9f0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6f9ce7ab-85d6-43bd-9be9-30481d249e07"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d183bb9b-855a-40c1-9cef-ebb3a59a575e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d23cc520-506f-4d91-9a0d-3e0e6de44375"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""db862bd7-7c52-4616-a14d-a1e954c7de21"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ec76111c-eade-4157-acd5-5a0aa93f53c3"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6a8f7c0-63f1-404e-97d0-b2bfc9f01181"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseMap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""726261f8-f03b-46b1-b40f-669ab6cc3a38"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovingOnMap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3bc31474-8971-4c04-ae9a-7e94894757fa"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prism"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0018cb57-2a93-4ced-9070-c7a579cff19f"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractionModule"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0029b021-8a23-4b43-a666-56ae17ddd458"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractionSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""793f95ae-f673-46e0-b65b-bd5507a32a64"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""InteractionMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b42a814-2a76-4dfe-a5a3-d26e7b8947b8"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a918800-e10b-45e2-b521-b1ba3e12224e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a955e18a-b7aa-4043-8085-32977e5c1bc7"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_OpenCloseMap = m_Player.FindAction("OpenCloseMap", throwIfNotFound: true);
        m_Player_MovingOnMap = m_Player.FindAction("MovingOnMap", throwIfNotFound: true);
        m_Player_Prism = m_Player.FindAction("Prism", throwIfNotFound: true);
        m_Player_InteractionModule = m_Player.FindAction("InteractionModule", throwIfNotFound: true);
        m_Player_InteractionSelect = m_Player.FindAction("InteractionSelect", throwIfNotFound: true);
        m_Player_InteractionMove = m_Player.FindAction("InteractionMove", throwIfNotFound: true);
        m_Player_RotateCamera = m_Player.FindAction("RotateCamera", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_OpenCloseMap;
    private readonly InputAction m_Player_MovingOnMap;
    private readonly InputAction m_Player_Prism;
    private readonly InputAction m_Player_InteractionModule;
    private readonly InputAction m_Player_InteractionSelect;
    private readonly InputAction m_Player_InteractionMove;
    private readonly InputAction m_Player_RotateCamera;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Dash;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @OpenCloseMap => m_Wrapper.m_Player_OpenCloseMap;
        public InputAction @MovingOnMap => m_Wrapper.m_Player_MovingOnMap;
        public InputAction @Prism => m_Wrapper.m_Player_Prism;
        public InputAction @InteractionModule => m_Wrapper.m_Player_InteractionModule;
        public InputAction @InteractionSelect => m_Wrapper.m_Player_InteractionSelect;
        public InputAction @InteractionMove => m_Wrapper.m_Player_InteractionMove;
        public InputAction @RotateCamera => m_Wrapper.m_Player_RotateCamera;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @OpenCloseMap.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenCloseMap;
                @OpenCloseMap.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenCloseMap;
                @OpenCloseMap.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenCloseMap;
                @MovingOnMap.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovingOnMap;
                @MovingOnMap.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovingOnMap;
                @MovingOnMap.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovingOnMap;
                @Prism.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrism;
                @Prism.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrism;
                @Prism.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrism;
                @InteractionModule.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractionModule;
                @InteractionModule.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractionModule;
                @InteractionModule.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractionModule;
                @InteractionSelect.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractionSelect;
                @InteractionSelect.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractionSelect;
                @InteractionSelect.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractionSelect;
                @InteractionMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractionMove;
                @InteractionMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractionMove;
                @InteractionMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractionMove;
                @RotateCamera.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCamera;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @OpenCloseMap.started += instance.OnOpenCloseMap;
                @OpenCloseMap.performed += instance.OnOpenCloseMap;
                @OpenCloseMap.canceled += instance.OnOpenCloseMap;
                @MovingOnMap.started += instance.OnMovingOnMap;
                @MovingOnMap.performed += instance.OnMovingOnMap;
                @MovingOnMap.canceled += instance.OnMovingOnMap;
                @Prism.started += instance.OnPrism;
                @Prism.performed += instance.OnPrism;
                @Prism.canceled += instance.OnPrism;
                @InteractionModule.started += instance.OnInteractionModule;
                @InteractionModule.performed += instance.OnInteractionModule;
                @InteractionModule.canceled += instance.OnInteractionModule;
                @InteractionSelect.started += instance.OnInteractionSelect;
                @InteractionSelect.performed += instance.OnInteractionSelect;
                @InteractionSelect.canceled += instance.OnInteractionSelect;
                @InteractionMove.started += instance.OnInteractionMove;
                @InteractionMove.performed += instance.OnInteractionMove;
                @InteractionMove.canceled += instance.OnInteractionMove;
                @RotateCamera.started += instance.OnRotateCamera;
                @RotateCamera.performed += instance.OnRotateCamera;
                @RotateCamera.canceled += instance.OnRotateCamera;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnOpenCloseMap(InputAction.CallbackContext context);
        void OnMovingOnMap(InputAction.CallbackContext context);
        void OnPrism(InputAction.CallbackContext context);
        void OnInteractionModule(InputAction.CallbackContext context);
        void OnInteractionSelect(InputAction.CallbackContext context);
        void OnInteractionMove(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
}
