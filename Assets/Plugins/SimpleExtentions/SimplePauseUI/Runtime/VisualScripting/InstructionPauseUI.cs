using System;
using System.Threading.Tasks;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace SimpleExtentions.Runtime.Pause
{
    [Version(1, 0, 0)]

    [Title("Pause UI")]
    [Description("Opens specified PauseUI")]

    [Category("UI/Pause UI")]

    [Parameter("Pause UI", "The currency type to modify")]
    [Parameter("State", "Whether to open or close the Pause UI")]

    [Keywords("Pause", "Resume", "Time", "Stop", "Menu")]

    [Image(typeof(IconPause), ColorTheme.Type.Red)]
    [Serializable]
    public class InstructionPauseUI : Instruction
    {
        public enum EnumState
        {
            Open,
            Close
        }

        [SerializeField] private PropertyGetGameObject m_PauseUI;
        [SerializeField] private EnumState m_State = EnumState.Open;

        public override string Title => $"{this.m_State} {this.m_PauseUI}";
        protected override Task Run(Args args)
        {
            GameObject pauseGO = m_PauseUI.Get(args);
            if (pauseGO == null)
            {
                Debug.LogWarning("Pause UI has not been specified");
                return DefaultResult;
            }

            PauseUI pauseUI = pauseGO.GetComponent<PauseUI>();
            if (pauseUI == null)
            {
                Debug.LogWarning("Pause UI component not found on the specified object");
                return DefaultResult;
            }

            if (m_State == EnumState.Open)
            {
                pauseUI.OpenUI(pauseGO);
            }
            else if (m_State == EnumState.Close)
            {
                pauseUI.CloseUI(pauseGO);
            }
            return DefaultResult;
        }
    }
}