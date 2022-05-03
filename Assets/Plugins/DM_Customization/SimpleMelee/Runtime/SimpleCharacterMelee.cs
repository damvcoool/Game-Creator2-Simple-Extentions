using UnityEngine;
using GameCreator.Runtime.Characters;

namespace DM_Customization.Runtime
{
    [AddComponentMenu("Game Creator/Custom/Simple Character Melee")]

    [Icon(DMPaths.DMPATH + "SimpleMelee/Editor/MeleeGizmo.png")]

    public class SimpleCharacterMelee : MonoBehaviour
    {
        [SerializeField] SimpleWeapon m_Weapon;
        private Character m_Character;
        private GameObject p_Weapon;
        private void Start()
        {
            m_Character = this.GetComponent<Character>();
            p_Weapon = m_Weapon.Equip(m_Character);
        }

        public void CharacterAttack()
        {
            m_Weapon.Attack(this.m_Character, p_Weapon);
        }
    }
}