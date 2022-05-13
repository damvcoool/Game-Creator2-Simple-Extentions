using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
namespace DM_Customization
{
    [Title("Custom Character Controller")]
    [Image(typeof(IconCharacter), ColorTheme.Type.Purple)]

    [Category("Custom Character Controller")]
    [Description("Configures the default 3D character controller")]

    [Serializable]
    public class CustomCharacterPreset : IKernelPreset
    {
        public IUnitPlayer MakePlayer => new UnitPlayerDirectionalAlternative();
        public IUnitMotion MakeMotion => new UnitMotionController();
        public IUnitDriver MakeDriver => new UnitDriverController();
        public IUnitFacing MakeFacing => new UnitFacingPivot();
        public IUnitAnimim MakeAnimim => new UnitAnimimKinematic();
    }
}