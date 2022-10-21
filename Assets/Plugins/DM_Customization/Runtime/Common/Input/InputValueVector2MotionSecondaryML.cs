using System;
using UnityEngine;
using UnityEngine.InputSystem;
using GameCreator.Runtime.Common;

namespace DM_Customization.Runtime.Common
{
    [Title("Secondary Motion With Options")]
    [Category("Usage/Secondary Motion With Options")]

    [Version(0, 1, 0    )]

    [Description("Secondary motion commonly used to orbit the camera around the main character: Move the Mouse while pressing a key (or not) and Left Stick on Gamepads")]
    [Image(typeof(IconRotation), ColorTheme.Type.Blue)]
    
    [Keywords("Orbit", "Joystick")]
    
    [Serializable]
    public class InputValueVector2MotionSecondaryML : TInputValueVector2
    {
        
        private enum MouseButton
        {
            Always,
            PressingLeftButton,
            PressingMiddleButton,
            PressingRightButton
        }
        // MEMBERS: -------------------------------------------------------------------------------
            
        [NonSerialized] private InputAction m_InputAction;
        [SerializeField] private MouseButton m_WhilePressing = MouseButton.Always;


        // PROPERTIES: ----------------------------------------------------------------------------

        public InputAction InputAction
        {
            get
            {
                if (this.m_InputAction == null)
                {
                    this.m_InputAction = new InputAction(
                        "Secondary Motion",
                        InputActionType.Value
                    );
                
                    this.m_InputAction.AddBinding(
                        "<Mouse>/delta",
                        processors: @"
                        invertVector2(invertX=false,invertY=true),
                        scaleVector2(x=3,y=3),
                        divideScreenSize,
                        divideDeltaTime"
                    );
                
                    this.m_InputAction.AddBinding(
                        "<Gamepad>/rightStick",
                        processors: @"
                        invertVector2(invertX=false,invertY=true)"
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

        public static InputPropertyValueVector2 Create()
        {
            return new InputPropertyValueVector2(
                new InputValueVector2MotionSecondary()
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

        public override Vector2 Read()
        {
            string isMouse = "";
            Vector2 movement = this.InputAction?.ReadValue<Vector2>() ?? Vector2.zero;

            if (this.m_InputAction.activeControl != null)
            { 
                isMouse = this.m_InputAction.activeControl.displayName; 
            }

            if (isMouse == "Delta")
            {
                return this.m_WhilePressing switch
                {
                    MouseButton.PressingLeftButton => Mouse.current.leftButton.isPressed
                        ? movement
                        : Vector2.zero,

                    MouseButton.PressingMiddleButton => Mouse.current.middleButton.isPressed
                        ? movement
                        : Vector2.zero, 

                    MouseButton.PressingRightButton => Mouse.current.rightButton.isPressed
                        ? movement
                        : Vector2.zero,

                    _ => movement

                };
            }
            else
            {
                return this.InputAction?.ReadValue<Vector2>() ?? Vector2.zero;
            }
            
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