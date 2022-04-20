using System;
using UnityEngine.InputSystem;
using GameCreator.Runtime.Common;

namespace DM_Customization
{
    [Title("Acceleration")]
    [Category("Usage/Acceleration")]

    [Description("Get Acceleration from a Controller Trigger or axis input, like L2 and R2 in most modern controllers")]
    [Image(typeof(IconChevronUp), ColorTheme.Type.Purple)]

    [Keywords("Joystick", "Acceleration")]

    [Serializable]
    public class InputValueFloatAcceleration : TInputValueFloat
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private InputAction m_InputAction;

        // PROPERTIES: ----------------------------------------------------------------------------

        public InputAction InputAction
        {
            get
            {
                if (this.m_InputAction == null)
                {
                    this.m_InputAction = new InputAction(
                        "Acceleration",
                        InputActionType.Value
                    );
                    // R2 in most Gamepads, added Mouse Y axis for Testing if no gamepad is available.
                    this.m_InputAction.AddBinding(
                        "<Mouse>/delta/y"
                    );
                    // R2 in most Gamepads, 
                    this.m_InputAction.AddBinding(
                        "<Gamepad>/rightTrigger"
                    );
                }

                return this.m_InputAction;
            }
        }

        public override bool Active
        {
            get => this.InputAction?.enabled ?? false;
            set
            {
                switch (value)
                {
                    case true: this.Enable(); break;
                    case false: this.Disable(); break;
                }
            }
        }

        // INITIALIZERS: --------------------------------------------------------------------------

        public static InputPropertyValueFloat Create()
        {
            return new InputPropertyValueFloat(
                new InputValueFloatAcceleration()
            );
        }

        public override void OnStartup()
        {
            this.Enable();
        }

        public override void OnDispose()
        {
            this.Disable();
            this.InputAction?.Dispose();
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override float Read()
        {
            return this.InputAction?.ReadValue<float>() ?? 0f;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Enable()
        {
            this.InputAction?.Enable();
        }

        private void Disable()
        {
            this.InputAction?.Disable();
        }
    }
}