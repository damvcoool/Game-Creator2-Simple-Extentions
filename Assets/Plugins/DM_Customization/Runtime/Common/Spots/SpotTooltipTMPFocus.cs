using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime.Common
{ 
    [Title("Show Text (TMP) on Focus")]
    [Image(typeof(IconString), ColorTheme.Type.Blue, typeof(OverlayDot))]
    
    [Category("Tooltips/Show Text (TMP) on Focus")]
    [Description(
        "Displays a text in a world-space canvas when the Hotspot is focused by the target and " +
        "hides it when it is not. If no Prefab is provided, a default UI is displayed"
    )]

    [Serializable]
    public class SpotTooltipTMPFocus : SpotTooltipTMP
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private IInteractive m_Interactive;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Show {this.m_Text} on Focus";

        // OVERRIDE METHODS: ----------------------------------------------------------------------

        public override void OnAwake(Hotspot hotspot)
        {
            base.OnAwake(hotspot);
            this.m_Interactive = InteractionTracker.Require(hotspot.gameObject);
        }

        protected override bool EnableInstance(Hotspot hotspot)
        {
            bool isActive = base.EnableInstance(hotspot);
            
            Character character = hotspot.Target.Get<Character>();
            bool hasFocus = character != null && 
                            character.Interaction.Target?.Instance == hotspot.gameObject;

            return isActive && hasFocus && !this.m_Interactive.IsInteracting;
        }
    }
}