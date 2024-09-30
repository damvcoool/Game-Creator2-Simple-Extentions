using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace SimpleExtentions.Runtime.Pause
{
    [Version(1, 0, 0)]

    [Title("Pause UI is Open")]
    [Description("Checks if the Pause UI is currently open")]

    [Category("UI/Pause UI is Open")]

    [Keywords("Pause", "Resume", "Time", "Stop", "Menu")]

    [Image(typeof(IconPause), ColorTheme.Type.Red)]

    [Serializable]
    public class ConditionPauseUI : Condition
    {
        [SerializeField] private PropertyGetGameObject m_PauseUI;
        protected override string Summary => $"is Pause UI Open";
        protected override bool Run(Args args)
        {
            PauseUI pauseUI = m_PauseUI.Get<PauseUI>(args);

            return PauseUI.IsOpen;
        }
    }
}