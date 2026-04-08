using System;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace SimpleExtentions.Runtime.Common
{
    [Title("Frames Per Second")]
    [Category("Settings/Current FPS")]

    [Image(typeof(IconUnity), ColorTheme.Type.TextLight)]
    [Description("Returns the current Frames Per Second, FPS, smoothed over multiple frames")]

    [Serializable]
    public class GetStringCurrentFPS : PropertyTypeGetString
    {
        private const float SMOOTH_FACTOR = 0.1f;

        [NonSerialized] private static int s_LastFrame = -1;
        [NonSerialized] private static float s_SmoothedDeltaTime;

        public override string Get(Args args) => GetFPS();

        public override string Get(GameObject gameObject) => GetFPS();

        public static PropertyGetString Create => new PropertyGetString(
            new GetStringCurrentFPS()
        );

        public override string String => "Show Game FPS";

        private static string GetFPS()
        {
            float delta = Time.unscaledDeltaTime;
            if (delta <= 0f) return "0";

            if (Time.frameCount != s_LastFrame)
            {
                s_LastFrame = Time.frameCount;
                s_SmoothedDeltaTime = s_SmoothedDeltaTime > 0f
                    ? Mathf.Lerp(s_SmoothedDeltaTime, delta, SMOOTH_FACTOR)
                    : delta;
            }

            return s_SmoothedDeltaTime > 0f ? ((int)(1f / s_SmoothedDeltaTime)).ToString() : "0";
        }
    }
}