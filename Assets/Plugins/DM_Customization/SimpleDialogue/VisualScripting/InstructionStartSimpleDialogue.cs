using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization
{
    [Version(1, 0, 0)]

    [Title("Simple Dialogue UI")]
    [Description("Opens a trading window for a specific Merchant")]

    [Category("UI/Simple Dialogue UI")]

    [Parameter("Pause UI", "The currency type to modify")]
    [Parameter("State", "Whether to open or close the Pause UI")]

    [Keywords("Pause", "Resume", "Time", "Stop", "Menu")]

    [Image(typeof(IconUIText), ColorTheme.Type.Yellow)]

    [Serializable]
    public class InstructionStartSimpleDialogue : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectTransform.Create();
        [SerializeField] private SimpleDialogueUI m_DisplayMe = new SimpleDialogueUI();
        
        
        protected override Task Run(Args args)
        {
            var bubble = m_Character.Get(args).gameObject.GetComponentInChildren<SimpleDialogueUI>();
            string charcterName = m_Character.Get(args).name;

            Debug.Log(m_Character.Get(args).transform);
            if (bubble == null)
                bubble = SimpleDialogueUI.Instantiate(m_DisplayMe, m_Character.Get(args).transform, false);
            
            bubble.StartDialogue(charcterName);
            return DefaultResult;
        }
    }
}
