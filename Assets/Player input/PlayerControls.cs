//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Player input/PlayerControls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Defender"",
            ""id"": ""889d1738-0c7a-4b3c-b0f3-55be116fa874"",
            ""actions"": [
                {
                    ""name"": ""AcceptSelection"",
                    ""type"": ""Button"",
                    ""id"": ""6ec28143-d1a1-437c-94e3-b6894d000df2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""50806422-168e-46ef-b636-d5252a19894f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0c8e5c33-eeed-4a14-a82c-efb1173e22bc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AcceptSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4567f8e9-22ab-430b-9b04-7ad799703629"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Defender
        m_Defender = asset.FindActionMap("Defender", throwIfNotFound: true);
        m_Defender_AcceptSelection = m_Defender.FindAction("AcceptSelection", throwIfNotFound: true);
        m_Defender_MousePosition = m_Defender.FindAction("MousePosition", throwIfNotFound: true);
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

    // Defender
    private readonly InputActionMap m_Defender;
    private List<IDefenderActions> m_DefenderActionsCallbackInterfaces = new List<IDefenderActions>();
    private readonly InputAction m_Defender_AcceptSelection;
    private readonly InputAction m_Defender_MousePosition;
    public struct DefenderActions
    {
        private @PlayerControls m_Wrapper;
        public DefenderActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @AcceptSelection => m_Wrapper.m_Defender_AcceptSelection;
        public InputAction @MousePosition => m_Wrapper.m_Defender_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Defender; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefenderActions set) { return set.Get(); }
        public void AddCallbacks(IDefenderActions instance)
        {
            if (instance == null || m_Wrapper.m_DefenderActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DefenderActionsCallbackInterfaces.Add(instance);
            @AcceptSelection.started += instance.OnAcceptSelection;
            @AcceptSelection.performed += instance.OnAcceptSelection;
            @AcceptSelection.canceled += instance.OnAcceptSelection;
            @MousePosition.started += instance.OnMousePosition;
            @MousePosition.performed += instance.OnMousePosition;
            @MousePosition.canceled += instance.OnMousePosition;
        }

        private void UnregisterCallbacks(IDefenderActions instance)
        {
            @AcceptSelection.started -= instance.OnAcceptSelection;
            @AcceptSelection.performed -= instance.OnAcceptSelection;
            @AcceptSelection.canceled -= instance.OnAcceptSelection;
            @MousePosition.started -= instance.OnMousePosition;
            @MousePosition.performed -= instance.OnMousePosition;
            @MousePosition.canceled -= instance.OnMousePosition;
        }

        public void RemoveCallbacks(IDefenderActions instance)
        {
            if (m_Wrapper.m_DefenderActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDefenderActions instance)
        {
            foreach (var item in m_Wrapper.m_DefenderActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DefenderActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DefenderActions @Defender => new DefenderActions(this);
    public interface IDefenderActions
    {
        void OnAcceptSelection(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}