using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime
{
    [Serializable]
    public class InstructionSimpleAttack : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();

        protected override Task Run(Args args)
        {
            GameObject gameObject = this.m_Character.Get(args);

            SimpleCharacterMelee weapon = gameObject.GetComponent<SimpleCharacterMelee>();

            weapon.CharacterAttack();

            return DefaultResult;
        }
    }
}