using System;
using UnityEngine.InputSystem;
using GameCreator.Runtime.Common;

namespace SimpleExtentions.Runtime.Common
{
    [Title("Pause")]
    [Category("Usage/Pause")]
    
    [Description("Cross-device support for the 'Pause' option: Esc key on Keyboards and the pressing Options/Pause on Gamepads")]
    [Image(typeof(IconPause), ColorTheme.Type.Red)]

    [Serializable]
    public class InputButtonPause : TInputButtonInputAction
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private InputAction m_InputAction;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override InputAction InputAction
        {
            get
            {
                if (this.m_InputAction == null)
                {
                    this.m_InputAction = new InputAction(
                        "Pause",
                        InputActionType.Button
                    );

                    this.m_InputAction.AddBinding("<Gamepad>/start");
                    this.m_InputAction.AddBinding("<Keyboard>/escape");
                }

                return this.m_InputAction;
            }
        }

        // PROTECTED OVERRIDE METHODS: ------------------------------------------------------------

        protected override void ExecuteEventStart(InputAction.CallbackContext context)
        {
            this.ExecuteEventStart();
        }

        protected override void ExecuteEventCancel(InputAction.CallbackContext context)
        {
            this.ExecuteEventCancel();
        }

        protected override void ExecuteEventPerform(InputAction.CallbackContext context)
        {
            this.ExecuteEventPerform();
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static InputPropertyButton Create()
        {
            return new InputPropertyButton(
                new InputButtonPause()
            );
        }
    }
}