using System;
using UnityEngine.InputSystem;
using GameCreator.Runtime.Common;

namespace DM_Customization.Runtime.Common
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
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public static InputPropertyButton Create()
        {
            return new InputPropertyButton(
                new InputButtonPause()
            );
        }
    }
}