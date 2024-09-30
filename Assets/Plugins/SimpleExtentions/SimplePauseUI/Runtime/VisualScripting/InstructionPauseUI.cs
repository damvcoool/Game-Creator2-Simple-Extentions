using System;
using System.Threading.Tasks;
using UnityEngine;
using GameCreator.Runtime.Common;
using UnityEngine.UI;
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
        private PauseUI pauseUI;

        public override string Title => $"{this.m_State} {this.m_PauseUI}";
        protected override Task Run(Args args)
        {
            pauseUI = m_PauseUI.Get(args).Get<PauseUI>();

            if (pauseUI == null) 
            { 
                Debug.LogWarning($"Pause UI has not been specified"); 
                return DefaultResult; 
            }

            if (m_State == EnumState.Open)
            {
                pauseUI.OpenUI(m_PauseUI.Get(args));
            }
            if (m_State == EnumState.Close)
            {
                pauseUI.CloseUI(m_PauseUI.Get(args));
            }
            return DefaultResult;
        }
    }
}