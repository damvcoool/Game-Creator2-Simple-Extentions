using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace SimpleExtentions.Runtime.SimpleDoors
{

    [Serializable]
    public class InstructionSimpleDoorsClose : Instruction
    {
        [SerializeField] private TSimpleDoor m_Door;
        protected override Task Run(Args args)
        {
            GameObject obj = m_Door.gameObject;
            foreach(TSimpleDoor door in obj.GetComponents<TSimpleDoor>())
            {
                door.Close();
            }
            return DefaultResult;
        }
    }
}