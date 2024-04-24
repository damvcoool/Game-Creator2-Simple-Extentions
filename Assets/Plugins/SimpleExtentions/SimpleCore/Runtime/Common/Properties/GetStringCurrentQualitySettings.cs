using System;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace SimpleExtentions.Runtime.Common
{
    [Title("Current Graphical Quality")]
    [Category("Settings/Current Graphical Quality")]

    [Image(typeof(IconUnity), ColorTheme.Type.White)]
    [Description("Returns the current Grpahical Quality Setting Name")]

    [Serializable]
    public class GetStringCurrentQualitySettings : PropertyTypeGetString
    {
        public override string Get(Args args) => QualitySettings.names[QualitySettings.GetQualityLevel()];

        public override string Get(GameObject gameObject) => QualitySettings.names[QualitySettings.GetQualityLevel()];

        public static PropertyGetString Create => new PropertyGetString(
            new GetStringCurrentQualitySettings()
        );

        public override string String => "Current Graphical Quality";

        public override string EditorValue => QualitySettings.names[QualitySettings.GetQualityLevel()];
    }
}
