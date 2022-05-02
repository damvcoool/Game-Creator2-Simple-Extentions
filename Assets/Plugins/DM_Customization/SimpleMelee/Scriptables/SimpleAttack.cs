using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace DM_Customization.Runtime
{
    [CreateAssetMenu(
        fileName = "Simple Weapon",
        menuName = "Custom/Simple Attack"
    )]

    [Icon("")]

    [Serializable]
    public class SimpleAttack : ScriptableObject
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private AnimationClip m_Attack;
        
        private Args m_Args;

        // PROPERTIES: ----------------------------------------------------------------------------

        public AnimationClip Attack => this.m_Attack;
        
    }
}