using System;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace SimpleExtentions.Runtime.Common
{
    [Title("Frames Per Second")]
    [Category("Settings/Current FPS")]

    [Image(typeof(IconUnity), ColorTheme.Type.TextLight)]
    [Description("Returns the current Frames Per Second, FPS")]

    [Serializable]
    public class GetStringCurrentFPS : PropertyTypeGetString
    {
        public override string Get(Args args) => GetFPS();

        public override string Get(GameObject gameObject) => GetFPS();

        public static PropertyGetString Create => new PropertyGetString(
            new GetStringCurrentFPS()
        );

        public override string String => "Show Game FPS";

        private string GetFPS()
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            return fps.ToString();
        }
    }
}