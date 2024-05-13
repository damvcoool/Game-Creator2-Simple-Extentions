using UnityEngine;
using System;

namespace SimpleExtentions.Runtime.SimpleDoors
{
    public abstract class TSimpleDoor : MonoBehaviour
    {
        // Events
        public event Action OnOpenStart;
        public event Action OnOpenComplete;
        public event Action OnCloseStart;
        public event Action OnCloseComplete;

        // Members
        protected bool isOpening = false;
        protected bool isClosing = false;
        public bool isOpen { get; private set; } = false;
        public bool isBusy
        {
            get
            {
                if (isClosing || isOpening) 
                {
                return true;
                }
                return false;
            }
        }

        public abstract void Open();
        public abstract void Close();

        protected void InvokeOpenStartEvent()
        {
            if (OnOpenStart != null)
            {
                OnOpenStart.Invoke();
            }
        }

        protected void InvokeOpenCompleteEvent()
        {
            if (OnOpenComplete != null)
            {
                OnOpenComplete.Invoke();
                isOpen = true;
            }
        }

        protected void InvokeCloseStartEvent()
        {
            if (OnCloseStart != null)
            {
                OnCloseStart.Invoke();
            }
        }

        protected void InvokeCloseCompleteEvent()
        {
            if (OnCloseComplete != null)
            {
                OnCloseComplete.Invoke();
                isOpen = false;
            }
        }
    }
}