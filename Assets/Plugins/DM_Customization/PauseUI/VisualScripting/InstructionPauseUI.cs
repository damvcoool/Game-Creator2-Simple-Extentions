using System;
using System.Threading.Tasks;
using UnityEngine;
using GameCreator.Runtime.Common;
using UnityEngine.UI;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime
{
    [Version(1, 0, 0)]

    [Title("Pause UI")]
    [Description("Opens a trading window for a specific Merchant")]

    [Category("UI/Pause UI")]

    [Parameter("Pause UI", "The currency type to modify")]
    [Parameter("State", "Whether to open or close the Pause UI")]

    [Keywords("Pause", "Resume", "Time", "Stop", "Menu")]

    [Image(typeof(IconPause), ColorTheme.Type.Red)]
    [Serializable]
    public class InstructionPauseUI : Instruction
    {
        public override string Title => $"{this.m_State} Pause UI";

        public enum EnumState
        {
            Open,
            Close
        }
        [SerializeField] private PropertyGetGameObject m_PauseUI;
        [SerializeField] private EnumState m_State = EnumState.Open;
        private PauseUI pauseUI;

        protected override Task Run(Args args)
        {
            pauseUI = m_PauseUI.Get(args).Get<PauseUI>();


            if (m_State == EnumState.Open)
            {
                if (pauseUI == null) { Debug.Log($"Pause UI has not been specified"); return DefaultResult; }
                pauseUI.OpenUI(m_PauseUI.Get(args).gameObject);
            }
            if (m_State == EnumState.Close)
            {
                PauseUI m_ClosingUI = GameObject.FindObjectOfType<PauseUI>();
                if (m_ClosingUI != null) m_ClosingUI.gameObject.SetActive(false);
            }



            return DefaultResult;
        }
    }
}