using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;

namespace DM_Customization
{
    [Title("Directional Alternative")]
    [Image(typeof(IconGamepadCross), ColorTheme.Type.Blue)]
    
    [Category("Directional Alternative")]
    [Description(
        "Moves the Player using a directional input from the Main Camera's perspective and provides built-in methods for Jump and Interact"
    )]

    [Serializable]
    public class UnitPlayerDirectionalAlternative : UnitPlayerDirectional
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private InputPropertyButton m_InputJump;
        [SerializeField] private InputPropertyButton m_InputInteract;

        // INITIALIZERS: --------------------------------------------------------------------------

        public UnitPlayerDirectionalAlternative()
        {
            this.m_InputJump = InputButtonJump.Create();
            this.m_InputInteract = InputButtonInteract.Create();
        }
        
        // OVERRIDERS: ----------------------------------------------------------------------------

        public override void OnStartup(Character character)
        {
            base.OnStartup(character);
            this.m_InputJump.OnStartup();
            this.m_InputInteract.OnStartup();
        }
        
        public override void OnDispose(Character character)
        {
            base.OnDispose(character);
            this.m_InputJump.OnDispose();
            this.m_InputInteract.OnDispose();
        }
        
        public override void OnEnable()
        {
            base.OnEnable();
            this.m_InputJump.Enable();
            this.m_InputInteract.Enable();
            this.m_InputJump.RegisterPerform(this.GetJump);
            this.m_InputInteract.RegisterPerform(this.GetInteract);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            this.m_InputJump.Disable();
            this.m_InputInteract.Disable();
            this.m_InputJump.ForgetPerform(this.GetJump);
            this.m_InputInteract.ForgetPerform(this.GetInteract);
        }


        // UPDATE METHODS: ------------------------------------------------------------------------


        protected virtual void GetJump()
        {
            if (!this.Character.IsPlayer || !this.m_IsControllable) return;

            this.Character.Jump?.Do();
            return;
        }

        protected virtual void GetInteract()
        {
            if (!this.Character.IsPlayer || !this.m_IsControllable) return;

            this.Character.Interaction?.Interact();
            return;
        }

        // STRING: --------------------------------------------------------------------------------

        public override string ToString() => "Directional Alternative";
    }
}