using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace SimpleExtentions.Runtime.SimpleDialogue
{
    [Version(1, 0, 0)]

    [Title("Display Simple DialogueUI")]
    [Description("Opens a trading window for a specific Merchant")]

    [Category("Simple Dialogue/Display Simple DialogueUI")]

    [Parameter("Pause UI", "The currency type to modify")]
    [Parameter("State", "Whether to open or close the Pause UI")]

    [Keywords("Pause", "Resume", "Time", "Stop", "Menu")]

    [Image(typeof(IconUIText), ColorTheme.Type.Yellow)]

    [Serializable]
    public class InstructionStartSimpleDialogue : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectSelf.Create();
        [SerializeField] private SimpleDialogueUI m_SimpleDialogue;

        public override string Title => String.Format("Display {0}", this.m_SimpleDialogue == null ? "(none)" : this.m_SimpleDialogue.name);
        protected override Task Run(Args args)
        {
            SimpleDialogueUI bubble = m_Character.Get(args).gameObject.GetComponentInChildren<SimpleDialogueUI>();
            string charcterName = m_Character.Get(args).name;

            if (bubble == null)
                bubble = SimpleDialogueUI.Instantiate(m_SimpleDialogue, m_Character.Get(args).transform, false);
            
            bubble.StartDialogue(charcterName);
            return DefaultResult;
        }
    }
}
