using System;
using UnityEngine.InputSystem;

namespace GameCreator.Runtime.Common
{
    [Title("Attack")]
    [Category("Usage/Attack")]
    
    [Description("Cross-device support for the 'Attack' input: Left Mouse Click or and the pressing L1 on most Gamepads")]
    [Image(typeof(IconSkull), ColorTheme.Type.Purple)]

    [Serializable]
    public class InputButtonAttack : TInputButtonInputAction
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
                        "Attack",
                        InputActionType.Button
                    );

                    this.m_InputAction.AddBinding("<Mouse>/leftButton");
                    this.m_InputAction.AddBinding("<Gamepad>/rightShoulder");
                }

                return this.m_InputAction;
            }
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public static InputPropertyButton Create()
        {
            return new InputPropertyButton(
                new InputButtonAttack()
            );
        }
    }
}